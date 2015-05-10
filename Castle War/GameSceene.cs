using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.Units;
using CastleWar.Units.Types.Heroes;
using CastleWar.Units.Types.Buildings;
using CastleWar.UI;
using CastleWar.UI.Counters;

namespace CastleWar
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
        public void showMenu()
        {
            Add(new Background(Assets.BackgroungMenu, false));
            Add(new Cursor());
            var tmp = new Control(Assets.ButtonStart, 350, 300);
            tmp.onClick = delegate()
            {
                GameScene.Instance.RemoveAll();
                startGame();
            };
            Add(tmp);
            tmp = new Control(Assets.ButtonResults, 350, 375);
            tmp.onClick = delegate()
            {
                GameScene.Instance.RemoveAll();
                showResults();
            };
            Add(tmp);
            tmp = new Control(Assets.ButtonExit, 350, 455);
            tmp.onClick = delegate()
            {
                Environment.Exit(0);
            };
            Add(tmp);
        }
        public void showResults()
        {
            Add(new Background(Assets.BackgroungMenu, false));
            Add(new Cursor());
            var str = new Label(415, 290, "TOP SCORES", Color.White, 0, false, 24);
            str.Graphic.CenterOrigin();
            Add(str);
            var results = Records.Results;
            var counter = 1;
            foreach (var item in results)
            {
                str = new Label(415, 300 + counter * 18, item.ToString(), Color.White, 0, false, 18);
                str.Graphic.CenterOrigin();
                Add(str);
                counter++;
            }
            var tmp = new Control(Assets.ButtonMenu, 350, 300 + counter * 20);
            tmp.onClick = delegate()
            {
                GameScene.Instance.RemoveAll();
                showMenu();
            };
            Add(tmp);
        }

        void resetPlayers()
        {
            Teams.playerBlue.reset();
            Teams.playerRed.reset();

            Teams.bluTeam.Clear();
            Teams.redTeam.Clear();

            StatisticWatcher.reset();
            Global.reset();
        }
        public void startGame()
        {
            resetPlayers();

            Add(new Background(Assets.BackgroungGame));

            AddUI();

            Add(new Ancient(Team.Blu, Global.bluAncientCoords));
            Add(new Ancient(Team.Red, Global.redAncientCoords));

            Teams.bluTower = Add(new Tower(Team.Blu, new Point(465, 550)));
            Teams.redTower = Add(new Tower(Team.Red, new Point(335, 250)));
        }
        private void AddUI()
        {
            Add(new Label(5, 780, "TIP: You are controlling bottom (blue) castle.", Color.White, 1000));

            Buttons.BluHP = Add(new Control(Assets.ButtonHP, 633, 129));
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
            Buttons.BluArmor = Add(new Control(Assets.ButtonArmor, 633, 196));
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
            Buttons.BluDamage = Add(new Control(Assets.ButtonDamage, 633, 258));
            Buttons.BluDamage.onClick = delegate()
            {
                if (Teams.playerBlue.coins >= 3)
                {
                    Teams.playerBlue.SpendCoin(3);
                    Teams.playerBlue.Upgrade(Upgades.Damage);
                    Teams.bluTower.UpgradeDamage();
                    Console.WriteLine("BLU: Upgraded Damage");
                }
                else Global.needGold.Play();
            };
            Buttons.BlueDragon = Add(new Control(Assets.ButtonDragon, 633, 476));
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
            Buttons.BluePriest = Add(new Control(Assets.ButtonPriest, 633, 556));
            Buttons.BluePriest.onClick = delegate()
            {
                if (Teams.playerBlue.coins >= 15)
                {
                    GameScene.Instance.Add(new Priest(Team.Blu, Global.bluAncientCoords));
                    Console.WriteLine("BLU: Hired Priest");
                    Teams.playerBlue.SpendCoin(15);
                }
                else Global.needGold.Play();
            };

            Buttons.RedHP = Add(new Control(Assets.ButtonHP, 40, 129));
            Buttons.RedArmor = Add(new Control(Assets.ButtonArmor, 40, 196));
            Buttons.RedDamage = Add(new Control(Assets.ButtonDamage, 40, 258));
            Buttons.RedDragon = Add(new Control(Assets.ButtonDragon, 37, 476));
            Buttons.RedPriest = Add(new Control(Assets.ButtonPriest, 37, 556));

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
            showMenu();
            Records.Load();
            //startGame();
        }
    }
}