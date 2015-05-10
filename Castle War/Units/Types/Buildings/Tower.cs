using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.Weapons;

namespace CastleWar.Units.Types.Buildings
{
    class Tower : Building
    {
        public Tower(int team, Point position)
            : base(Assets.BuildingTower, team, position, Type.tower)
        {
            
        }

        public override void Update()
        {
            if (!isInitialized)
            {
                _armor = 10;
                _range = 150;
                _hp = 600;
                _damage = 20;
                _gun = new TowerWeapon(_damage, GameScene.Instance);
                _cooldownValue = 10;
                _speed = 0;
            }
            base.Update();
        }

        public override void UpgradeDamage()
        {
            _damage += 5;
            _gun = new TowerWeapon(_damage, GameScene.Instance); 
        }
    }
}
