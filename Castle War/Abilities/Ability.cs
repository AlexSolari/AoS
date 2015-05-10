using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.Units;
using CastleWar.Weapons;

namespace CastleWar.Abilities
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
                            double Distance = (Math.Sqrt(Math.Pow(_owner.Xcoord - target.X, 2) + Math.Pow(_owner.Ycoord - target.Y, 2)));
                            if (Distance <= _range)
                            {
                                tmpGun.Shoot(target);
                            }
                        }
                    }
                    break;
                case AbilityList.HealingAura:
                    {
                        List<Entity> ownTeam;
                        if (_owner.team != Team.Red) ownTeam = Teams.bluTeam;
                        else ownTeam = Teams.redTeam;

                        foreach (Unit target in ownTeam)
                        {
                            double Distance = (Math.Sqrt(Math.Pow(_owner.Xcoord - target.X, 2) + Math.Pow(_owner.Ycoord - target.Y, 2)));
                            if (!target.isBuilding && Distance <= _range)
                            {
                                target.Heal(5);
                                for (var counter = 0; counter < 4; counter++)
                                    GameScene.Instance.Add(new Particle(target.X, target.Y, Assets.ParticleStar, 4, 4)
                                    {
                                        LifeSpan = 10,
                                        Angle = GoodRnd.gen.Next(-360, 360),
                                        FinalAlpha = 0,
                                        FinalX = target.X + GoodRnd.gen.Next(-50, 50) * (float)GoodRnd.gen.NextDouble(),
                                        FinalY = target.Y + GoodRnd.gen.Next(-50, 50) * (float)GoodRnd.gen.NextDouble(),
                                        FinalAngle = GoodRnd.gen.Next(-360, 360),
                                        FinalScaleX = 0.5f,
                                        LockScaleRatio = true
                                    });
                            }
                        }
                    }
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
