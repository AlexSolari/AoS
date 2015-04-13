using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using New.res.src.unit;
using New.res.src;

namespace New
{
    static class GoodRnd
    {
        static public Random gen = new Random();
        static public int NextBin()
        {
            return gen.Next(2);
        }

        static public double NextFloatBin()
        {
            return gen.NextDouble();
        }
    }
    class GameScene : Scene
    {
        private void AddUI()
        {
            Add(new Label(5, 780, "TIP: You are controlling bottom (blue) castle.", Color.White, 1000));

            Buttons.BluHP = Add(new Control(@"ui/buttonHP.png", 633, 129));
            Buttons.BluHP.onClick = delegate()
            {
                if (Teams.playerBlue.coins >= 5)
                {
                    Teams.playerBlue.SpendCoin(5);
                    Teams.playerBlue.Upgrade(Upgades.HP);
                    Console.WriteLine("BLU: Upgraded HP");
                }
                else Global.needGold.Play();
            };
            Buttons.BluArmor = Add(new Control(@"ui/buttonArmor.png", 633, 196));
            Buttons.BluArmor.onClick = delegate()
            {
                if (Teams.playerBlue.coins >= 10)
                {
                    Teams.playerBlue.SpendCoin(10);
                    Teams.playerBlue.Upgrade(Upgades.Armor);
                    Console.WriteLine("BLU: Upgraded Armor");
                    Teams.bluTower.UpgradeArmor();
                }
                else Global.needGold.Play();
            };
            Buttons.BluDamage = Add(new Control(@"ui/buttonDamage.png", 633, 258));
            Buttons.BluDamage.onClick = delegate()
            {
                if (Teams.playerBlue.coins >= 3)
                {
                    Teams.playerBlue.SpendCoin(3);
                    Teams.playerBlue.Upgrade(Upgades.Damage);
                    Console.WriteLine("BLU: Upgraded Damage");
                }
                else Global.needGold.Play();
            };
            Buttons.BlueDragon = Add(new Control(@"ui/summonDragon.png", 633, 476));
            Buttons.BlueDragon.onClick = delegate()
            {
                if (Teams.playerBlue.coins >= 50)
                {
                    GameScene.Instance.Add(new Dragon(Team.Blu, Global.bluAncientCoords));
                    Console.WriteLine("BLU: Hired Dragon");
                    Teams.playerBlue.SpendCoin(50);
                }
                else Global.needGold.Play();
                
            };

            Buttons.RedHP = Add(new Control(@"ui/buttonHP.png", 40, 129));
            Buttons.RedArmor = Add(new Control(@"ui/buttonArmor.png", 40, 196));
            Buttons.RedDamage = Add(new Control(@"ui/buttonDamage.png", 40, 258));
            Buttons.RedDragon = Add(new Control(@"ui/summonDragon.png", 37, 476));

            Add(new CoinCounter(700, 70, Team.Blu));
            Add(new CoinCounter(110, 70, Team.Red));

            Add(new AddHPCounter(95, 353, Team.Red));
            Add(new AddArmorCounter(95, 387, Team.Red));
            Add(new AddDMGCounter(95, 421, Team.Red));

            Add(new AddHPCounter(695, 353, Team.Blu));
            Add(new AddArmorCounter(695, 387, Team.Blu));
            Add(new AddDMGCounter(695, 421, Team.Blu));

            Add(new Cursor());
        }
        public GameScene()
        {
            
            Global.loop.Volume = 0.2f;
            Global.needGold.Volume = 0.5f;

            Global.loop.Play();

            Add(new Background());

            AddUI();          

            Add(new Ancient(Team.Blu, Global.bluAncientCoords));
            Add(new Ancient(Team.Red, Global.redAncientCoords));

            Teams.bluTower = Add(new Tower(Team.Blu, new Point(465, 550)));
            Teams.redTower = Add(new Tower(Team.Red, new Point(335, 250)));

            
        }
    }
}