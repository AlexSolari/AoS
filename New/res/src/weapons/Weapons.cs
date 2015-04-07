using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src
{
    class MeleeWeapon : Gun
    {
        public MeleeWeapon(int dmg, Scene parent) : base(ProjectileType.unvisible, dmg, parent) { }
    }

    class RangeWeapon : Gun
    {
        public RangeWeapon(int dmg, Scene parent) : base(ProjectileType.simple, dmg, parent) { }
    }

    class SiegeWeapon : Gun
    {
        public SiegeWeapon(int dmg, Scene parent) : base(ProjectileType.large, dmg, parent) { }
    }

    class TowerWeapon : Gun
    {
        public TowerWeapon(int dmg, Scene parent) : base(ProjectileType.building, dmg, parent) { }
    }

    class AncientWeapon : Gun
    {
        public AncientWeapon(int dmg, Scene parent) : base(ProjectileType.building, dmg, parent) { }
    }
}
