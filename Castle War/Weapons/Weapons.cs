﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace CastleWar.Weapons
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

    class DragonWeapon : Gun
    {
        public DragonWeapon(int dmg, Scene parent) : base(ProjectileType.dragon, dmg, parent) { }
    }

    class PriestWeapon : Gun
    {
        public PriestWeapon(int dmg, Scene parent) : base(ProjectileType.priest, dmg, parent) { }
    }

    class NoWeapon : Gun
    {
        public NoWeapon(Scene parent) : base(ProjectileType.none, 0, parent) { }
    }
}
