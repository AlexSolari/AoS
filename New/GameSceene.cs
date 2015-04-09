﻿using System;
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
            //Music sound = new Music("music/loop.ogg", true);

            //sound.Play();

            Add(new Background());

            Add(new Ancient(Team.Blu, Global.bluAncientCoords));
            Add(new Ancient(Team.Red, Global.redAncientCoords));

            Add(new Tower(Team.Blu, new Point(465, 550)));
            Add(new Tower(Team.Red, new Point(335, 250)));

            Add(new Cursor());
        }
    }
}