using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using New.res.src.unit;

namespace New.res.src.weapons.projectiles
{
    class UnvisibleProjectile : Projectile
    {
        public UnvisibleProjectile(Unit target, int dmg, int X, int Y)
            :base(target, dmg, X, Y)
        {
            _sprite = Image.CreateCircle(0, Color.White);
            _collider = new PointCollider(X, Y, Tags.projetile);
            _speed = 4;
            SetGraphic(_sprite);
            SetCollider(_collider);
        }
    }
    class SimpleProjectile : Projectile
    {
        public SimpleProjectile(Unit target, int dmg, int X, int Y)
            : base(target, dmg, X, Y)
        {
            _sprite = Image.CreateCircle(1, Color.White);
            _collider = new PointCollider(X, Y, Tags.projetile);
            _speed = 4;

            SetGraphic(_sprite);
            SetCollider(_collider);
        }
    }
    class LargeProjectile : Projectile
    {
        public LargeProjectile(Unit target, int dmg, int X, int Y)
            : base(target, dmg, X, Y)
        {
            _sprite = Image.CreateCircle(2, Color.White);
            _collider = new PointCollider(X, Y, Tags.projetile);
            _speed = 4;

            SetGraphic(_sprite);
            SetCollider(_collider);
        }
    }

    class BuildingProjectile : Projectile
    {
        private int _tickCounter = 0;
        public BuildingProjectile(Unit target, int dmg, int X, int Y)
            : base(target, dmg, X, Y)
        {

            _sprite = Image.CreateCircle(0, Color.White);
            _collider = new PointCollider(X, Y, Tags.projetile);
            _speed = 4;

            SetGraphic(_sprite);
            SetCollider(_collider);
        }

        public override void Update()
        {
            _tickCounter += GoodRnd.NextBin();
            if (_tickCounter % 2 == 0 && _target.alive)
            {
                Game.Scene.Add(new Particle(X, Y, "star.png", 4, 4)
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

            base.Update();
        }
    }
}
