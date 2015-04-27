using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using New.res.src.abilities;

namespace New.res.src.unit
{
    class Priest : Hero
    {
        public Priest(int team, Point position)
            : base(@"priest.png", team, position, Type.priestHero)
        {
        }

        public override void Update()
        {
            
            _heroPassive = new Ability(AbilityList.HealingAura, false, 110, this);
            if (!isInitialized)
            {
                _armor = 5;
                _range = 150;
                _hp = 100;
                _damage = 35;
                _gun = new PriestWeapon(_damage, GameScene.Instance);
                _cooldownValue = 60;
                _speed = 1;
            }
            
            base.Update();
            if (Global.tick % 40 == 0)
            {
                _heroPassive.Effect();
            }
            
        }
    }
}
