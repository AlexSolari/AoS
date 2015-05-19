using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CastleWar.Players.Bot
{
    static class Bot
    {
        private static int _nextUpgrade = GoodRnd.gen.Next(0, 3);
        public static void Update()
        {
            switch (_nextUpgrade)
            {
                case (int)Upgades.Damage:
                    if (Teams.playerRed.coins >= 3)
                    {
                        Teams.playerRed.SpendCoin(3);
                        Teams.playerRed.Upgrade((Upgades)_nextUpgrade);
                        _nextUpgrade = GoodRnd.gen.Next(0, 3);
                        Console.WriteLine("RED: Upgraded Damage");
                        Teams.redTower.UpgradeDamage();
                    }
                    break;
                case (int)Upgades.Armor:
                    if (Teams.playerRed.coins >= 10)
                    {
                        Teams.playerRed.SpendCoin(10);
                        Teams.playerRed.Upgrade((Upgades)_nextUpgrade);
                        _nextUpgrade = GoodRnd.gen.Next(0, 3);
                        Console.WriteLine("RED: Upgraded Armor");
                        Teams.redTower.UpgradeArmor();
                    }
                    break;
                case (int)Upgades.HP:
                    if (Teams.playerRed.coins >= 5)
                    {
                        Teams.playerRed.SpendCoin(5);
                        Teams.playerRed.Upgrade((Upgades)_nextUpgrade);
                        _nextUpgrade = GoodRnd.gen.Next(0, 3);
                        Console.WriteLine("RED: Upgraded HP");
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
