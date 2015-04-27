using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using New.res.src.abilities;

namespace New.res.src.unit
{
    class Dragon : Hero
    {
        public Dragon(int team, Point position)
            : base(@"dragon.png", team, position, Type.dragonHero)
        {
        }

        public override void Update()
        {
            
            _heroPassive = new Ability(AbilityList.DragonBreath, false, 250, this);
            _idleTimer = 0;
            if (!isInitialized)
            {
                isFlying = true;
                _armor = 9999;
                _range = 0;
                _hp = 500;
                _damage = 35;
                _gun = new NoWeapon(GameScene.Instance);
                _cooldownValue = 10;
                _speed = 2;
            }
            if (_cooldown <= 0)
            {
                _heroPassive.Effect();
                _cooldown = _cooldownValue;
            }
            base.Update();
        }
    }
}
