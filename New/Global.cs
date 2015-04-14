﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using New.res.src.player;
using New.res.src;
using New.res.src.unit;

namespace New
{
    static class Buttons
    {
        public static Control BluHP;
        public static Control BluArmor;
        public static Control BluDamage;
        public static Control RedHP;
        public static Control RedArmor;
        public static Control RedDamage;
        public static Control RedDragon;
        public static Control BlueDragon;
    }
    static class Upgades
    {
        public const int HP = 0;
        public const int Armor = 1;
        public const int Damage = 2;
    }
    static class Borders
    {
        public const int Left = 573;
        public const int Right = 226;
        public const int Top = 0;
        public const int Bottom = 800;
    }
    static class Teams
    {
        static public Tower bluTower;
        static public Tower redTower;
        static public List<Entity> redTeam = new List<Entity>();
        public static Player playerRed = new Player(Team.Red);
        public static Player playerBlue = new Player(Team.Blu);
        static public List<Entity> bluTeam = new List<Entity>();
    }
    class Global
    {
        public const int HPBarsUpdateRate = 2; //less = highter update rate
        public static Music loop = new Music("music/loop.ogg", true);
        public static Sound needGold = new Sound("music/needGold.ogg", false);

        public const float damageReducingCoefficient = 0.89f;
        public const float armorMultifier = 1.2f;

        public static Session playerOne;
        public static int tick = 0;
        public const int Width = 800;
        public const int Height = 800;

        public static double GetAngle(Vector2 a, Vector2 b)
        {
            return Math.Atan2(b.Y - a.Y, b.X - a.X);
        }

        public static void ReduceVector(ref Vector2 vector, float limit)
        {
            var EPSILON = 0.001f;
            while (vector.Length > limit + EPSILON)
            {
                vector.X *= 0.9f;
                vector.Y *= 0.9f;
            }
            if (Math.Abs(vector.X) < 0.1f) vector.X = 0;
            if (Math.Abs(vector.Y) < 0.1f) vector.Y = 0;
        }

        public static Point redAncientCoords = new Point(400, 100);
        public static Point bluAncientCoords = new Point(400, 700);
    }

    static class Team
    {
        public const int Red = 1;
        public const int Blu = -1;
    }
    static class Tags
    {
        public const int unit = 0;
        public const int projetile = 1;
        public const int rangeCheck = 2;
    }

    static class Type
    {
        public const int melee = 0;
        public const int range = 1;
        public const int siege = 2;
        public const int tower = 3;
        public const int ancient = 4;
        public const int dragonHero = 5;
        public const int priestHero = 6;
    }

    static class ProjectileType
    {
        public const int simple = 0;
        public const int large = 1;
        public const int building = 2;
        public const int unvisible = 3;
        public const int none = 4;
        public const int dragon = 5;
        public const int priest = 6;
    }
    
}
