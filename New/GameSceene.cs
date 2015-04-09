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
        public GameScene()
        {
            
            Global.loop.Volume = 0.2f;
            Global.needGold.Volume = 0.5f;

            Global.loop.Play();

            Add(new Background());

            Add(new Label(5, 780, "TIP: You are controlling bottom (blue) castle.", Color.White, 1000));

            Buttons.BluHP = Add(new Control(@"ui/buttonHP.png", 633, 129));
            Buttons.BluArmor = Add(new Control(@"ui/buttonArmor.png", 633, 196));
            Buttons.BluDamage = Add(new Control(@"ui/buttonDamage.png", 633, 258));

            Buttons.RedHP = Add(new Control(@"ui/buttonHP.png", 40, 129));
            Buttons.RedArmor = Add(new Control(@"ui/buttonArmor.png", 40, 196));
            Buttons.RedDamage = Add(new Control(@"ui/buttonDamage.png", 40, 258));

            Add(new CoinCounter(700, 70, Team.Blu));
            Add(new CoinCounter(110, 70, Team.Red));

            Add(new Ancient(Team.Blu, Global.bluAncientCoords));
            Add(new Ancient(Team.Red, Global.redAncientCoords));

            Add(new Tower(Team.Blu, new Point(465, 550)));
            Add(new Tower(Team.Red, new Point(335, 250)));

            Add(new Cursor());
        }
    }
}