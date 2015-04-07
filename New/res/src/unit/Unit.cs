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
            X = position.X;
            Y = position.Y;


            _type = type;

            _direction = new Vector2(0);
        }

        public void Damage(int value)
        {
            _hp = (int)(_hp - Math.Round(value * Math.Pow(0.94, _armor), MidpointRounding.ToEven));
            if (_hp <= 0)
            {
                GameScene.Instance.RemoveGraphic(Bar);
                RemoveSelf();
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

            float prevX = X;
            float prevY = Y;

            _idleTimer--;
            
            float x;
            float y;

            for (var range = 0; range <= _range; range+=10 )
                for (int deg = 0; deg < 360; deg += 10)
                {
                    x = X + range * (float)Math.Sin((deg * Math.PI / 180));
                    y = Y + range * (float)Math.Cos((deg * Math.PI / 180));


                    //if (_type == Type.melee) Game.Scene.Add(new Particle(x, y, "star.png", 4, 4)
                    //{
                    //    LifeSpan = 2
                    //});

                    if (tmpCollider.Overlap(x, y, Tags.unit) && ((Unit)tmpCollider.CollideEntity(x, y, Tags.unit)).team != _team)
                    {
                        var target = (Unit)tmpCollider.CollideEntity(x, y, Tags.unit);
                        
                        bool isInRange = (Math.Sqrt(Math.Pow(X - target.X, 2) + Math.Pow(Y - target.Y, 2)) <= _range);
                        if (isInRange)
                        {
                            //X = prevX;
                            //Y = prevY;
                            if (_cooldown == 0)
                            {
                                _gun.Shoot(target);
                                _cooldown = _cooldownValue;
                                _idleTimer = _cooldownValue;
                                if (_type != Type.ancient && _type != Type.tower) target._idleTimer = 20;
                            }

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
                switch (_type)
                {
                    case Type.melee:
                        {
                            _armor = 1;
                            _range = 20;
                            _hp = 15;
                            _damage = 4;
                            _gun = new MeleeWeapon(_damage, Game.Scene);
                            _cooldownValue = 30;
                            _speed = 2;
                            break;
                        }
                    case Type.range:
                        {
                            _armor = 0;
                            _range = 90;
                            _hp = 10;
                            _damage = 7;
                            _gun = new RangeWeapon(_damage, Game.Scene);
                            _cooldownValue = 40;
                            _speed = 2;
                            break;
                        }
                    case Type.siege:
                        {
                            _armor = 1;
                            _range = 200;
                            _hp = 12;
                            _damage = 11;
                            _gun = new SiegeWeapon(_damage, Game.Scene);
                            _cooldownValue = 100;
                            _speed = 1;
                            break;
                        }
                    case Type.tower:
                        {
                            _armor = 5;
                            _range = 100;
                            _hp = 300;
                            _damage = 20;
                            _gun = new TowerWeapon(_damage, Game.Scene);
                            _cooldownValue = 40;
                            _speed = 0;
                            break;
                        }
                    case Type.ancient:
                        {
                            _armor = 10;
                            _range = 140;
                            _hp = 800;
                            _damage = 2;
                            _gun = new AncientWeapon(_damage, Game.Scene);
                            _cooldownValue = 2;
                            _speed = 0;
                            break;
                        }
                    default:
                        throw new Exception("Unknown unit type");
                }
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
