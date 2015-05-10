using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.Players;
using CastleWar.Weapons;
using CastleWar.UI;

namespace CastleWar.Units
{
    abstract class Unit : Entity
    {
        

        public const int COLLISION_RADIUS = 25;
        public const int TARGETING_RADIUS = 150;

        protected int _handle;
        protected Text Bar;
        protected int _type;
        protected Image _sprite;
        protected int _team;
        protected int _hp;
        protected int _maxhp;
        protected int _armor;
        protected int _damage;
        protected int _idleTimer = 0;
        protected Vector2 _direction;
        protected int _range;
        protected Gun _gun;
        protected int _cooldown;
        protected int _cooldownValue;
        protected float _speed;

        protected Point _targetPoint;

        protected bool isInitialized = false;
        protected bool isAlive = true;
        protected bool isFlying = false;

        public Unit(string spritePath, int team, Point position, int type)
        {
            _handle = GoodRnd.gen.Next(Int32.MinValue, Int32.MaxValue);
             if (team == Team.Red) spritePath = @"red/" + spritePath;
             else spritePath = @"blu/" + spritePath; ;
            _sprite = new Image(spritePath);
            Bar = new Text(16);

            _team = team;
            X = position.X;
            Y = position.Y;

            _type = type;

            _direction = new Vector2(0);

            if (_team == Team.Red)
            {
                Teams.redTeam.Add(this);
                _targetPoint = new Point(Global.bluAncientCoords.X, 900);
            }
            else
            {
                StatisticWatcher.trackUnitsProduced(1);
                Teams.bluTeam.Add(this);
                _targetPoint = new Point(Global.redAncientCoords.X, -100);
            }            
        }

        public void UpgradeArmor()
        {
            _armor++;
        }

        public virtual void UpgradeDamage()
        {
            _damage+=2;
        }

        public void UpgradeHP()
        {
            _hp += 5;
        }

        public virtual void Damage(int damagePure)
        {
            var damageDealt = Convert.ToInt32(Math.Round(damagePure * Math.Pow(Global.damageReducingCoefficient, _armor * Global.armorMultifier), MidpointRounding.ToEven) - GoodRnd.gen.Next(-1, 1));
            if (_team == Team.Blu) StatisticWatcher.trackDamageDone(damageDealt);
            _hp -= damageDealt;
            if (_hp <= 0 && alive)
            {
                isAlive = false;
                GameScene.Instance.RemoveGraphic(Bar);
                
                for (var i = 0; i < 20; i++)
                    GameScene.Instance.Add(new Particle(X, Y, Assets.ParticleBlood, 4, 4)
                    {
                        LifeSpan = 50,
                        Angle = 10,
                        FinalAlpha = 0,
                        FinalX = X + GoodRnd.gen.Next(-20, 20) * (float)GoodRnd.gen.NextDouble(),
                        FinalY = Y + GoodRnd.gen.Next(-20, 20) * (float)GoodRnd.gen.NextDouble(),
                        FinalScaleX = 0.5f,
                        LockScaleRatio = true
                    });
                if (_team == Team.Red) 
                {
                    StatisticWatcher.trackUnitsKilled(1);
                    StatisticWatcher.trackCoins(1);
                    Teams.redTeam.Remove(this);
                    Teams.redTeam.TrimExcess();
                    Teams.playerBlue.AddCoin();
                    GameScene.Instance.Add(new Label(X, Y - Graphic.HalfHeight, "+1", Color.Gold, 40, true, 10));
                }
                else 
                {
                    Teams.bluTeam.Remove(this);
                    Teams.bluTeam.TrimExcess();
                    Teams.playerRed.AddCoin();
                }
                RemoveSelf();
            }
        }
        public int type
        {
            get { return _type; }
        }
        public bool alive
        {
            get { return isAlive; }
        }
        public int team
        {
            get { return _team; }
        }
        public float Xcoord
        {
            get { return X; }
        }
        public float Ycoord
        {
            get { return Y; }
        }

        public int damage
        {
            get { return _damage; }
        }

        protected void AITick()
        {
            _idleTimer--;

            List<Entity> enemyTeam;
            if (_team == Team.Red) enemyTeam = Teams.bluTeam;
            else enemyTeam = Teams.redTeam;

            if (_type != Type.dragonHero)
            {
                foreach (Unit target in enemyTeam)
                {
                    if (target._type == Type.dragonHero) continue;
                    double Distance = (Math.Sqrt(Math.Pow(X - target.X, 2) + Math.Pow(Y - target.Y, 2)));
                    if (Distance <= _range)
                    {
                        if (_cooldown == 0)
                        {
                            _gun.Shoot(target);

                            _cooldown = _cooldownValue;
                            _idleTimer = _cooldownValue;
                        }

                    }
                    else if (Distance <= TARGETING_RADIUS)
                    {
                        _targetPoint = new Point((int)target.X, (int)target.Y);
                    }
                }
            }
           

            if (_idleTimer<=0 && !isBuilding)
            {
                if (_type == Type.siege) _idleTimer = 2;

                _direction.X = _targetPoint.X - X;
                _direction.Y = _targetPoint.Y - Y;

                Global.ReduceVector(ref _direction, _speed);

                var randomShift = 2 * (float)(GoodRnd.gen.NextDouble() - 0.5f);

                X += Convert.ToInt32(_direction.X) + randomShift;
                Y += Convert.ToInt32(_direction.Y);
            }
            if (Math.Abs(X - _targetPoint.X) <= Math.Sqrt(_range) && Math.Abs(Y - _targetPoint.Y) <= Math.Sqrt(_range))
            {
                if (_team == Team.Red) _targetPoint = Global.bluAncientCoords;
                else _targetPoint = Global.redAncientCoords;
            }
        }

        protected void PreventOutOfBorders()
        {
            if( (X + COLLISION_RADIUS > Borders.Left) ||
                (X - COLLISION_RADIUS < Borders.Right) ||
                (Y + COLLISION_RADIUS > Borders.Bottom) ||
                (Y - COLLISION_RADIUS < Borders.Top) )
            {
                RemoveSelf();
                if (_team == Team.Red)
                {
                    Teams.redTeam.Remove(this);
                    Teams.redTeam.TrimExcess();
                }
                else
                {
                    Teams.bluTeam.Remove(this);
                    Teams.bluTeam.TrimExcess();
                }
            }
        }
        protected void PreventCollisions()
        {
            PreventOutOfBorders();

            if (isBuilding || isFlying) return;

            List<Entity> Units = new List<Entity>();
            
            foreach (Unit unit in Teams.bluTeam) Units.Add(unit);
            foreach (Unit unit in Teams.redTeam) Units.Add(unit);

            foreach (Unit target in Units)
            {
                if (_handle == target._handle) continue;
                double Distance = (Math.Sqrt(Math.Pow(X - target.X, 2) + Math.Pow(Y - target.Y, 2)));
                if (Distance <= COLLISION_RADIUS)
                {
                    var randomDirection = new Vector2();
                    var randomShift = GoodRnd.gen.Next(-5, 5);
                    randomDirection.X = target.X - X + randomShift * (float)Math.Sin(Global.GetAngle(randomDirection, new Vector2(0, _team)) * 180 / 3.14);
                    randomDirection.Y = target.Y - Y + randomShift * (float)Math.Cos(Global.GetAngle(randomDirection, new Vector2(0, _team)) * 180 / 3.14);
                    randomDirection *= -1;

                    Global.ReduceVector(ref randomDirection, Convert.ToSingle(Distance)/4);

                    
                    while (Distance <= COLLISION_RADIUS)
                    {
                        if (Distance < 1 || randomDirection.X == 0 || randomDirection.Y == 0)
                        {
                            randomDirection.X = 1;
                            randomDirection.Y = 1;
                        }
                        X += randomDirection.X;
                        Y += randomDirection.Y;
                        if (!target.isBuilding) 
                        { 
                            target.X -= randomDirection.X/2;
                            target.Y -= randomDirection.Y/2;
                        }
                        Distance = (Math.Sqrt(Math.Pow(X - target.X, 2) + Math.Pow(Y - target.Y, 2)));
                    }
                }
            }
        }

        public void Heal(int value)
        {
            if (_hp == _maxhp) return;
            var prevHP = _hp;
            _hp += _maxhp * value/100;
            if (_hp > _maxhp) _hp = _maxhp;
            if (_team == Team.Blu) StatisticWatcher.trackHealthHealed(_hp - prevHP);
        }
        public bool isBuilding { get { return (_type == Type.tower || _type == Type.ancient); } }
        public override void Update()
        {
            
            if (!isInitialized)
            {
                isInitialized = true;

                SetGraphic(_sprite);

                _sprite.CenterOrigin();

                Player owner;
                if (_team == Team.Blu)
                {
                    owner =  Teams.playerBlue;
                } else owner =  Teams.playerRed;

                _hp += owner._bonusHP;
                _armor += owner._bonusArmor;
                _damage += owner._bonusDamage;
                _gun.setDmg(_damage);

                _maxhp = _hp;
            }

            if (_cooldown > 0) _cooldown--;


            _gun.Move((int)X, (int)Y);

            AITick();

            PreventCollisions();
        }
    }
}
