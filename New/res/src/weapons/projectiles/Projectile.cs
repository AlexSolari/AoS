using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using New.res.src.unit;

namespace New.res.src.weapons.projectiles
{
    abstract class Projectile : Entity
    {
        protected Graphic _sprite;
        protected Collider _collider;
        protected int _speed;
        protected Vector2 _direction;
        protected int _dmg;
        protected Unit _target;

        public Projectile(Unit target, int dmg, int x, int y)
        {
            _target = target;
            _dmg = dmg;
            X = x;
            Y = y;
            _direction = new Vector2(target.X - X, target.Y - Y);
            LifeSpan = 100;
        }

        public override void Update()
        {
            _direction.X = _target.X - X;
            _direction.Y = _target.Y - Y;

            Global.ReduceVector(ref _direction, _speed);

            X += _direction.X;
            Y += _direction.Y;
            var numberOfSplashes = 0;
            if (Math.Abs(X - _target.X) <= 10 && Math.Abs(Y - _target.Y) <= 10)
            {
                _target.Damage(_dmg);
                if (!_target.isBuilding)
                    for (var counter = _dmg; counter > 3; counter -= 3)
                    {
                        numberOfSplashes++;
                        if (numberOfSplashes > 20) break;
                        Game.Scene.Add(new Particle(X, Y, "blood.png", 6, 6)
                        {
                            LifeSpan = 10,
                            Angle = 10,
                            FinalAlpha = 0,
                            FinalX = X + GoodRnd.gen.Next(-20, 20) * (float)GoodRnd.gen.NextDouble(),
                            FinalY = Y + GoodRnd.gen.Next(-20, 20) * (float)GoodRnd.gen.NextDouble(),
                            FinalScaleX = 0.5f,
                            LockScaleRatio = true
                        });
                    }
                RemoveSelf();
            }
          base.Update();
        }
    }
}
