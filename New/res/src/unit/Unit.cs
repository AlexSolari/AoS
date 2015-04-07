using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src.unit
{
    abstract class Unit : Entity
    {
        protected int _handle;

        public int handle { get { return _handle; } }

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
            _sprite = new Image(spritePath);
            Bar = new Text(16);

            _team = team;
            if (_team == Team.Red) Teams.redTeam.Add(this);
            else Teams.bluTeam.Add(this);
            X = position.X;
            Y = position.Y;

            _type = type;

            _direction = new Vector2(0);
        }

        public void Damage(int value)
        {
            _hp = (int)(_hp - Math.Round(value * Math.Pow(Global.damageReducingCoefficient, _armor), MidpointRounding.ToEven));

#if DEBUG
            Console.WriteLine("Damage:");
            Console.WriteLine("\tPure: {0}", value);
            Console.WriteLine("\tDealt: {0}", Math.Round(value * Math.Pow(Global.damageReducingCoefficient, _armor), MidpointRounding.ToEven));
#endif
            if (_hp <= 0)
            {
                GameScene.Instance.RemoveGraphic(Bar);
                RemoveSelf();
                if (_team == Team.Red) Teams.redTeam.Remove(this);
                else Teams.bluTeam.Remove(this);
            }
        }

        public int team
        {
            get { return _team; }
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
                        if (_type != Type.ancient && _type != Type.tower) target._idleTimer = 20;
                    }

                }
            }

            if (_idleTimer<=0 && (_type != Type.ancient || _type != Type.tower))
            {
                _direction.X = _targetPoint.X - X;
                _direction.Y = _targetPoint.Y - Y;

                Global.ReduceVector(ref _direction, _speed);

                X += Convert.ToInt32(_direction.X);
                Y += Convert.ToInt32(_direction.Y);
            }
            {
                if (_team == Team.Red) _targetPoint = Global.bluAncientCoords;
                else _targetPoint = Global.redAncientCoords;
                return;
            }
        }

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

            }

            if (_cooldown > 0) _cooldown--;


            _gun.Move((int)X, (int)Y);

            AITick();
        }
    }
}
