using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src.unit
{
    class Tower : Building
    {
        public Tower(int team, Point position)
            : base(@"tower.png", team, position, Type.tower)
        {
            
        }

        public override void Update()
        {
            if (!isInitialized)
            {
                _armor = 5;
                _range = 100;
                _hp = 300;
                _damage = 20;
                _gun = new TowerWeapon(_damage, GameScene.Instance);
                _cooldownValue = 40;
                _speed = 0;
            }
            base.Update();
        }
    }
}
