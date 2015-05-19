using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.UI;
using CastleWar.Players;
using CastleWar.Units;
using CastleWar.Units.Types.Buildings;

namespace CastleWar
{
    static class Assets
    {
        public const string BackgroungMenu = @"ui/backgroundMenu.png";
        public const string BackgroungGame = @"ui/background.png";

        public const string ButtonStart = @"ui/buttonStart.png";
        public const string ButtonMenu = @"ui/buttonMenu.png";
        public const string ButtonExit = @"ui/buttonExit.png";
        public const string ButtonResults = @"ui/buttonResults.png";

        public const string ButtonHP = @"ui/buttonHP.png";
        public const string ButtonArmor = @"ui/buttonArmor.png";
        public const string ButtonDamage = @"ui/buttonDamage.png";
        public const string ButtonDragon = @"ui/summonDragon.png";
        public const string ButtonPriest = @"ui/summonPriest.png";

        public const string ButtonHPDisabled = @"ui/buttonHPDisabled.png";
        public const string ButtonArmorDisabled = @"ui/buttonArmorDisabled.png";
        public const string ButtonDamageDisabled = @"ui/buttonDamageDisabled.png";
        public const string ButtonDragonDisabled = @"ui/summonDragonDisabled.png";
        public const string ButtonPriestDisabled = @"ui/summonPriestDisabled.png";

        public const string UnitMeele = @"melee.png";
        public const string UnitRange = @"range.png";
        public const string UnitSiege = @"siege.png";

        public const string BuildingAncient = @"ancient.png";
        public const string BuildingTower = @"tower.png";

        public const string HeroDragon = @"dragon.png";
        public const string HeroPriest = @"priest.png";

        public const string Cursor = @"ui/cursor.png";

        public const string ParticleFire = @"particles/fire.png";
        public const string ParticleStar = @"particles/star.png";
        public const string ParticleBlood = @"particles/blood.png";
    }
    static class Records
    {
        static List<int> _Results = new List<int>();

        public static List<int> Get
        {
            get
            {
                var counter = 0;
                var tmp = new List<int>();
                foreach (var item in _Results)
                {
                    if (counter++ >= 10) return tmp;
                    tmp.Add(item);
                }
                return tmp;
            }
        }
        public static void Load()
        {
            try 
            {
                using (var stream = new StreamReader("results.dat"))
                {
                    while (!stream.EndOfStream)
                    {
                        var str = stream.ReadLine();
                        var res = Convert.ToInt32(str.Split(':')[1]);
                        if (!_Results.Contains(res)) _Results.Add(res);
                    }
                    _Results.Sort();
                    _Results.Reverse();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(e.Message);
            }
        }
    }
    static class StatisticWatcher
    {
        public static int updatePeriod = 100;

        private static int _coinsEarned = 0;
        private static int _unitsProduced = 0;
        private static int _unitsKilled = 0;
        private static int _damageDone = 0;
        private static int _healthHealed = 0;

        private static int _lastRecord = 0;
        private static int _maxPoints = 0;
        private static Dictionary<int, int> _statistic = new Dictionary<int,int>();
        public static Dictionary<int, int> statistic
        {
            get { return _statistic; }
        }
        public static int coinsEarned
        {
            get { return _coinsEarned; }
        }
        public static int unitsProduced
        {
            get { return _unitsProduced; }
        }
        public static int unitsKilled
        {
            get { return _unitsKilled; }
        }
        public static int damageDone
        {
            get { return _damageDone; }
        }
        public static int healthHealed
        {
            get { return _healthHealed; }
        }
        public static void trackCoins(int value) { _coinsEarned += value; }
        public static void trackUnitsProduced(int value) { _unitsProduced += value; }
        public static void trackUnitsKilled(int value) { _unitsKilled += value; }
        public static void trackDamageDone(int value) { _damageDone += value; }
        public static void trackHealthHealed(int value) { _healthHealed += value; }
        public static int totalPoints
        {
            get
            {
                var rank = Convert.ToInt32(_damageDone / _unitsProduced + (_healthHealed*10)/_unitsProduced + _coinsEarned + _unitsKilled);
                return rank;
            }
        }
        public static int lastRecord
        {
            get { return _lastRecord; }
        }
        public static int maxPoints
        {
            get { return _maxPoints; }
        }
        public static void trackPoints(int time)
        {
            if (_maxPoints < totalPoints) _maxPoints = totalPoints;
            _lastRecord = time;
            _statistic.Add(time, totalPoints);
        }

        public static void reset()
        {
             _coinsEarned = 0;
             _unitsProduced = 0;
             _unitsKilled = 0;
             _damageDone = 0;
             _healthHealed = 0;

             _lastRecord = 0;
             _maxPoints = 0;
             _statistic.Clear();
        }
    }
    static class Buttons
    {
        public static Control BluHP;
        public static Control BluArmor;
        public static Control BluDamage;
        public static Control BlueDragon;
        public static Control BluePriest;

        public static Control RedHP;
        public static Control RedArmor;
        public static Control RedDamage;
        public static Control RedDragon;
        public static Control RedPriest;
    }
    enum Upgades
    {
        HP,
        Armor,
        Damage
    }
    enum Border
    {
        Left = 573,
        Right = 226,
        Top = 0,
        Bottom = 800
    }
    static class Teams
    {
        static public Tower bluTower;
        static public Tower redTower;
        static public List<Entity> redTeam = new List<Entity>();
        public static Player playerRed = new Player(Convert.ToInt32(Team.Red));
        public static Player playerBlue = new Player(Convert.ToInt32(Team.Blu));
        static public List<Entity> bluTeam = new List<Entity>();
    }
    class Global
    {
        public static int HPBarsUpdateRate = 2; //less = highter update rate
        public static Music loop = new Music("music/loop.ogg", true);
        public static Sound needGold = new Sound("music/needGold.ogg", false);

        public const float damageReducingCoefficient = 0.89f;
        public const float armorMultifier = 1.2f;

        public static Session playerOne;
        public static int tick = 0;
        public static int Width = 800;
        public static int Height = 800;

        public static void reset() { tick = 0; }
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

    enum Team
    {
        Red,
        Blu
    }
    enum Type
    {
        melee,
        range,
        siege,
        tower,
        ancient,
        dragonHero,
        priestHero
    }
    enum SubType
    {
        Warrior,
        Building,
        Hero
    }
    enum ProjectileType
    {
        simple,
        large,
        building,
        unvisible,
        none,
        dragon,
        priest
    }
}
