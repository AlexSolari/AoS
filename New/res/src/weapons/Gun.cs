using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using New.res.src.unit;
using New.res.src.weapons.projectiles;

namespace New.res.src
{
    abstract class Gun
    {
        protected int _projectileType;
        protected int _dmg;
        protected int X;
        protected int Y;

        public Scene _parent;
        public Gun(int type, int dmg, Scene parent) { _projectileType = type; _dmg = dmg; X = 0; Y = 0; _parent = parent; }

        public void setDmg(int dmg) { _dmg = dmg; }
        public void Move(int x, int y) {X = x; Y = y;}

        public void Shoot(Unit target)
        {
            Projectile projectile;
            switch (_projectileType)
            {
                case ProjectileType.unvisible:
                    {
                        projectile = new UnvisibleProjectile(target, _dmg, X, Y);
                        break;
                    }
                case ProjectileType.simple:
                    {
                        projectile = new SimpleProjectile(target, _dmg, X, Y);
                        break;
                    }
                case ProjectileType.large:
                    {
                        projectile = new LargeProjectile(target, _dmg, X, Y);
                        break;
                    }
                case ProjectileType.building:
                    {
                        projectile = new BuildingProjectile(target, _dmg, X, Y);
                        break;
                    }
                case ProjectileType.none:
                    {
                        projectile = new NoProjectile(target, _dmg, X, Y);
                        break;
                    }
                case ProjectileType.dragon:
                    {
                        projectile = new DragonProjectile(target, _dmg, X, Y);
                        break;
                    }
                case ProjectileType.priest:
                    {
                        projectile = new PriestProjectile(target, _dmg, X, Y);
                        break;
                    }
                default:
                    throw new Exception("Unknown weapon type");
            }
            _parent.Add(projectile);
        }
    }
}
