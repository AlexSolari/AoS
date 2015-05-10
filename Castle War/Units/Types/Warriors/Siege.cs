using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.Weapons;

namespace CastleWar.Units.Types.Warriors
{
    class Siege : Warrior
    {
        public Siege(int team, Point position)
            : base(Assets.UnitSiege, team, position, Type.siege)
        {
            
        }

        public override void Update()
        {
            if (!isInitialized)
            {
                _armor = 3;
                _range = 200;
                _hp = 15;
                _damage = 11;
                _gun = new SiegeWeapon(_damage, GameScene.Instance);
                _cooldownValue = 100;
                _speed = 1;
            }
            base.Update();
        }
    }
}