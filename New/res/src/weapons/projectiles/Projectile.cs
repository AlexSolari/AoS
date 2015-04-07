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
            //LifeSpan = 70;
        }

        public override void Update()
        {
            _direction.X = _target.X - X;
            _direction.Y = _target.Y - Y;

            Global.ReduceVector(ref _direction, _speed);

            X += _direction.X;
            Y += _direction.Y;

            if (X == _target.X && Y == _target.Y)
            {
                _target.Damage(_dmg);
                RemoveSelf();
            }
          base.Update();
        }
    }
}
