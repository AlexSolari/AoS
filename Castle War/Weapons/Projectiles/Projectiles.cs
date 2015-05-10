using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.Units;

namespace CastleWar.Weapons.Projectiles
{
    class NoProjectile : Projectile
    {
        public NoProjectile(Unit target, int dmg, int X, int Y)
            : base(target, dmg, X, Y)
        {

        }
    }
    class UnvisibleProjectile : Projectile
    {
        public UnvisibleProjectile(Unit target, int dmg, int X, int Y)
            :base(target, dmg, X, Y)
        {
            _sprite = Image.CreateCircle(0, Color.White);
            _speed = 4;
            SetGraphic(_sprite);
        }
    }
    class SimpleProjectile : Projectile
    {
        public SimpleProjectile(Unit target, int dmg, int X, int Y)
            : base(target, dmg, X, Y)
        {
            _sprite = Image.CreateCircle(2, Color.Green);
            _speed = 4;

            SetGraphic(_sprite);
        }
    }
    class LargeProjectile : Projectile
    {
        public LargeProjectile(Unit target, int dmg, int X, int Y)
            : base(target, dmg, X, Y)
        {
            _sprite = Image.CreateCircle(3, Color.Gray);
            _speed = 4;

            SetGraphic(_sprite);
        }
    }

    class BuildingProjectile : Projectile
    {
        private int _tickCounter = 0;
        public BuildingProjectile(Unit target, int dmg, int X, int Y)
            : base(target, dmg, X, Y)
        {

            _sprite = Image.CreateCircle(3, Color.Yellow);
            _speed = 4;

            SetGraphic(_sprite);
        }

        public override void Update()
        {
            _tickCounter += GoodRnd.NextBin();
            if (_tickCounter % 2 == 0 && _target.alive)
            {
                GameScene.Instance.Add(new Particle(X, Y, Assets.ParticleStar, 4, 4)
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


    class DragonProjectile : Projectile
    {
        private int _tickCounter = 0;
        public DragonProjectile(Unit target, int dmg, int X, int Y)
            : base(target, dmg, X, Y)
        {

            _sprite = Image.CreateCircle(0, Color.Red);
            _speed = 4;

            SetGraphic(_sprite);
        }

        public override void Update()
        {
            _tickCounter += GoodRnd.NextBin();
            if (_target.alive)
            {
                for (var counter = 0; counter < 3; counter++ )
                    GameScene.Instance.Add(new Particle(X, Y, Assets.ParticleFire, 4, 4)
                    {
                        LifeSpan = 10,
                        Angle = GoodRnd.gen.Next(-360, 360),
                        FinalAlpha = 0,
                        FinalX = X + GoodRnd.gen.Next(-20, 20) * (float)GoodRnd.gen.NextDouble(),
                        FinalY = Y + GoodRnd.gen.Next(-20, 20) * (float)GoodRnd.gen.NextDouble(),
                        FinalAngle = GoodRnd.gen.Next(-360, 360),
                        FinalScaleX = 0.5f,
                        LockScaleRatio = true
                    });
            }

            base.Update();
        }
    }

    class PriestProjectile : Projectile
    {
        private int _tickCounter = 0;
        public PriestProjectile(Unit target, int dmg, int X, int Y)
            : base(target, dmg, X, Y)
        {

            _sprite = Image.CreateCircle(0, Color.Red);
            _speed = 10;

            SetGraphic(_sprite);
        }

        public override void Update()
        {
            _tickCounter += GoodRnd.NextBin();
            if (_target.alive)
            {
                for (var counter = 0; counter <5; counter++)
                    GameScene.Instance.Add(new Particle(X, Y, Assets.ParticleStar, 4, 4)
                    {
                        LifeSpan = 10,
                        Angle = GoodRnd.gen.Next(-360, 360),
                        FinalAlpha = 0,
                        FinalX = X + GoodRnd.gen.Next(-20, 20) * (float)GoodRnd.gen.NextDouble(),
                        FinalY = Y + GoodRnd.gen.Next(-20, 20) * (float)GoodRnd.gen.NextDouble(),
                        FinalAngle = GoodRnd.gen.Next(-360, 360),
                        FinalScaleX = 0.5f,
                        LockScaleRatio = true
                    });
            }

            base.Update();
        }
    }
}
