using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src.unit
{
    class Range : Warrior
    {
        public Range(int team, Point position)
            : base(@"range.png", team, position, Type.range)
        {
            
        }

        public override void Update()
        {
            if (!isInitialized)
            {
                _armor = 0;
                _range = 90;
                _hp = 10;
                _damage = 7;
                _gun = new RangeWeapon(_damage, GameScene.Instance);
                _cooldownValue = 40;
                _speed = 2;
            }
            base.Update();
        }
    }
}