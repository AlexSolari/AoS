using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.Weapons;
using CastleWar.Abilities;

namespace CastleWar.Units.Types.Heroes
{
    class Dragon : Hero
    {
        public Dragon(Team team, Point position)
            : base(Assets.HeroDragon, team, position, Type.dragonHero)
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
