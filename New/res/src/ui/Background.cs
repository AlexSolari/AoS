using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using New.res.src.unit;

namespace New.res.src
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
        public Background()
        {
            SetGraphic(new Image(@"background.png"));
            Graphic.Blend = BlendMode.Add;
        }
        public override void Update()
        {
            Bot.Update();
            if (Global.tick++ % 500 == 0)
            {
                Teams.playerBlue.AddCoin();
                Teams.playerRed.AddCoin();
            }
        }
    }
}
