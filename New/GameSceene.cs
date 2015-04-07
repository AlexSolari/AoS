using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using New.res.src.unit;


namespace New
{
    static class GoodRnd
    {
        static public Random gen = new Random();
        static public int NextBin()
        {
            return gen.Next(2);
        }
    }
    class GameScene : Scene
    {
        public GameScene()
        {
            Add(new Ancient(Team.Blu, Global.bluAncientCoords));
            Add(new Ancient(Team.Red, Global.redAncientCoords));

            Add(new Tower(Team.Blu, new Point(525, 475)));
            Add(new Tower(Team.Red, new Point(250, 325)));
        }
    }
}