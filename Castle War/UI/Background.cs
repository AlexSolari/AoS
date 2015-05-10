using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.Units;

namespace CastleWar.UI
{
    static class Bot
    {
        private static int _nextUpgrade = GoodRnd.gen.Next(0, 3);
        public static void Update()
        {
            switch (_nextUpgrade)
            {
                case Upgades.Damage:
                    if (Teams.playerRed.coins >= 3) 
                    {
                        Teams.playerRed.SpendCoin(3);
                        Teams.playerRed.Upgrade(_nextUpgrade);
                        _nextUpgrade = GoodRnd.gen.Next(0, 3);
                        Console.WriteLine("RED: Upgraded Damage");
                        Teams.redTower.UpgradeDamage();
                    }
                    break;
                case Upgades.Armor:
                    if (Teams.playerRed.coins >= 10)
                    {
                        Teams.playerRed.SpendCoin(10);
                        Teams.playerRed.Upgrade(_nextUpgrade);
                        _nextUpgrade = GoodRnd.gen.Next(0, 3);
                        Console.WriteLine("RED: Upgraded Armor");
                        Teams.redTower.UpgradeArmor();
                    }
                    break;
                case Upgades.HP:
                    if (Teams.playerRed.coins >= 5)
                    {
                        Teams.playerRed.SpendCoin(5);
                        Teams.playerRed.Upgrade(_nextUpgrade);
                        _nextUpgrade = GoodRnd.gen.Next(0, 3);
                        Console.WriteLine("RED: Upgraded HP");
                    }
                    break;
                default:
                    break;
            }
        }
    }
    class Background : Entity
    {
        private bool gameStarted;
        public Background(string sprite, bool mode = true)
        {
            SetGraphic(new Image(sprite));
            Graphic.Blend = BlendMode.Add;
            gameStarted = mode;
        }
        public override void Update()
        {
            if (gameStarted)
            {
                Bot.Update();
                if (Global.tick % StatisticWatcher.updatePeriod == 0)
                {
                    StatisticWatcher.trackPoints(Global.tick);
                }
                if (Global.tick++ % 500 == 0)
                {
                    StatisticWatcher.trackCoins(1);
                    Teams.playerBlue.AddCoin();
                    Teams.playerRed.AddCoin();
                }
            }
            
        }
    }
}
