using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src.unit
{
    class Melee : Warrior
    {
        public Melee(int team, Point position)
            : base(@"melee.png", team, position, Type.melee)
        {
            
        }
        public override void Update()
        {
            if (!isInitialized)
            {
                _armor = 1;
                _range = 30;
                _hp = 50;
                _damage = 4;
                _gun = new MeleeWeapon(_damage, GameScene.Instance);
                _cooldownValue = 40;
                _speed = 1;
            }
            base.Update();
        }
        
    }
}