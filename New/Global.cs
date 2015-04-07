using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New
{
    class Global
    {
        public static Session playerOne;
        public const int Width = 800;
        public const int Height = 800;

        public static double GetAngle(Vector2 a, Vector2 b)
        {
            return Math.Atan2(b.Y - a.Y, b.X - a.X);
        }

        public static void ReduceVector(ref Vector2 vector, float limit)
        {
            var EPSILON = 0.00001f;
            while (vector.Length > limit + EPSILON)
            {
                vector.X *= 0.9f;
                vector.Y *= 0.9f;
            }
            if (Math.Abs(vector.X) < 0.001f) vector.X = 0;
            if (Math.Abs(vector.Y) < 0.001f) vector.Y = 0;
        }

        public static Point redAncientCoords = new Point(100, 100);
        public static Point bluAncientCoords = new Point(700, 700);
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
    }

    static class ProjectileType
    {
        public const int simple = 0;
        public const int large = 1;
        public const int building = 2;
        public const int unvisible = 3;
    }
    
}
