using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using New.res.src.unit;

namespace New.res.src.abilities
{
    class AbilityList
    {
        public const int DragonBreath = 0;
        public const int HealingAura = 1;
        public const int Vampirism = 2;
        public const int Demolisher = 3;
    }
    class Ability
    {
        protected int _type;
        protected int _range;
        protected bool _isTargeted;
        protected Unit _owner;
        public Ability(int type, bool isTargeted, int range, Unit owner)
        {
            _owner = owner;
            _range = range;
            _isTargeted = isTargeted;
            _type = type;
        }

        public void Effect()
        {
            switch (_type)
            {
                case AbilityList.DragonBreath:
                    {
                        List<Entity> enemyTeam;
                        if (_owner.team == Team.Red) enemyTeam = Teams.bluTeam;
                        else enemyTeam = Teams.redTeam;

                        var tmpGun = new DragonWeapon(_owner.damage, GameScene.Instance);
                        tmpGun.Move((int)_owner.Xcoord, (int)_owner.Ycoord);

                        foreach (Unit target in enemyTeam)
                        {
                            //if (target.type == Type.dragonHero) continue;
                            double Distance = (Math.Sqrt(Math.Pow(_owner.Xcoord - target.X, 2) + Math.Pow(_owner.Ycoord - target.Y, 2)));
                            if (Distance <= _range)
                            {
                                tmpGun.Shoot(target);
                            }
                        }
                    }
                    break;
                case AbilityList.HealingAura:
                    break;
                case AbilityList.Vampirism:
                    break;
                case AbilityList.Demolisher:
                    break;
                default:
                    break;
            }
        }


    }
}
