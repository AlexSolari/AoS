using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using New.res.src.player;

namespace New.res.src.unit
{
    abstract class Unit : Entity
    {
        protected const int COLLISION_RADIUS = 21;
        protected const int TARGETING_RADIUS = 150;

        protected int _handle;
        protected Text Bar;
        protected int _type;
        protected Image _sprite;
        protected Collider _collider;
        protected int _team;
        protected int _hp;
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

        public Unit(string spritePath, int team, Point position, int type)
        {
            _handle = GoodRnd.gen.Next(Int32.MinValue, Int32.MaxValue);
             if (team == Team.Red) spritePath = @"red/" + spritePath;
             else spritePath = @"blu/" + spritePath; ;
            _sprite = new Image(spritePath);
            Bar = new Text(16);

            _team = team;
            if (_team == Team.Red) Teams.redTeam.Add(this);
            else Teams.bluTeam.Add(this);
            X = position.X;
            Y = position.Y;

            _type = type;

            _direction = new Vector2(0);

            if (_team == Team.Red) _targetPoint = Global.bluAncientCoords;
            else _targetPoint = Global.redAncientCoords;
        }

        public void Damage(int damagePure)
        {
            var damageDealt = Convert.ToInt32(Math.Round(damagePure * Math.Pow(Global.damageReducingCoefficient, _armor), MidpointRounding.ToEven) - GoodRnd.gen.Next(-1,1));

            _hp = _hp - damageDealt;

#if DEBUG
            Console.WriteLine("Damage:");
            Console.WriteLine("\tPure: {0}", damagePure);
            Console.WriteLine("\tDealt: {0}", damageDealt);
            Console.WriteLine("\tArmor coeff: {0}", Math.Pow(Global.damageReducingCoefficient, _armor));
#endif
            if (_hp <= 0)
            {
                GameScene.Instance.RemoveGraphic(Bar);
                RemoveSelf();
                if (_team == Team.Red) Teams.redTeam.Remove(this);
                else Teams.bluTeam.Remove(this);
            }
        }

        public bool alive
        {
            get { return _hp > 0; }
        }

        protected void AITick()
        {
            var tmpCollider = _collider;
            _idleTimer--;

            List<Entity> enemyTeam;
            if (_team == Team.Red) enemyTeam = Teams.bluTeam;
            else enemyTeam = Teams.redTeam;

            foreach(Unit target in enemyTeam)
            {
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

            if (_idleTimer<=0 && !isBuilding)
            {
                if (_type == Type.siege) _idleTimer = 2;

                _direction.X = _targetPoint.X - X;
                _direction.Y = _targetPoint.Y - Y;

                Global.ReduceVector(ref _direction, _speed);

                X += Convert.ToInt32(_direction.X);
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
            while (X + COLLISION_RADIUS > Borders.Left) X--;
            while (X - COLLISION_RADIUS < Borders.Right) X++;
            while (Y + COLLISION_RADIUS > Borders.Bottom) Y--;
            while (X - COLLISION_RADIUS < Borders.Top) Y++;
        }
        protected void PreventCollisions()
        {
            if (isBuilding) return;

            PreventOutOfBorders();

            List<Entity> Units = new List<Entity>();
            
            foreach (Unit unit in Teams.bluTeam) Units.Add(unit);
            foreach (Unit unit in Teams.redTeam) Units.Add(unit);

            foreach (Unit target in Units)
            {
                if (_handle == target._handle) return;
                double Distance = (Math.Sqrt(Math.Pow(X - target.X, 2) + Math.Pow(Y - target.Y, 2)));
                if (Distance <= COLLISION_RADIUS)
                {
                    var randomDirection = new Vector2();
                    var randomShift = GoodRnd.gen.Next(-5, 5);
                    randomDirection.X = target.X - X + randomShift * (float)Math.Sin(Global.GetAngle(randomDirection, new Vector2(0, _team)) * 180 / 3.14);
                    randomDirection.Y = target.Y - Y + randomShift * (float)Math.Cos(Global.GetAngle(randomDirection, new Vector2(0, _team)) * 180 / 3.14);
                    randomDirection *= -1;

                    Global.ReduceVector(ref randomDirection, Convert.ToSingle(Distance)/4);

                    if (Distance < 0.01f)
                    {
                        randomDirection.X = 1;
                        randomDirection.Y = 1;
                    }
                    while (Distance <= COLLISION_RADIUS)
                    {
                        X += randomDirection.X;
                        Y += randomDirection.Y;
                        Distance = (Math.Sqrt(Math.Pow(X - target.X, 2) + Math.Pow(Y - target.Y, 2)));
                    }
                }
            }
        }

        public bool isBuilding { get { return (_type == Type.tower || _type == Type.ancient); } }
        public override void Update()
        {
            
            if (!isInitialized)
            {
                isInitialized = true;
                _collider = new CircleCollider(_range, Tags.unit);

                SetGraphic(_sprite);
                SetCollider(_collider);

                _sprite.CenterOrigin();
                _collider.CenterOrigin();

                Player owner;
                if (_team == Team.Blu)
                {
                    owner =  Teams.playerBlue;
                } else owner =  Teams.playerRed;

                _hp += owner._bonusHP;
                _armor += owner._bonusArmor;
                _damage += owner._bonusDamage;
                _gun.setDmg(_damage);
            }

            if (_cooldown > 0) _cooldown--;


            _gun.Move((int)X, (int)Y);

            AITick();

            PreventCollisions();
        }
    }
}
