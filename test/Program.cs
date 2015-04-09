using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace test
{
    class MSG : Entity
    {
        public MSG()
        {
            SetGraphic(new Text(16));
            ((Text)Graphic).String = "LOX";
            ((Text)Graphic).OutlineColor = Color.Black;
            ((Text)Graphic).OutlineThickness = 2;
            X = 250;
            Y = 250;
            Graphic.CenterOrigin();
        }
    }
    class Background : Entity
    {
        public Background()
        {
            SetGraphic(new Image(@"D:\1.png"));
        }
    }
    class Unit : Entity
    {
        protected int direction;
        private int _num;
        private int _tick = 0;
        public Unit(int number)
        {
            X = 250;
            Y = 250;
            SetGraphic(Image.CreateCircle(5, Color.Red));
            Graphic.CenterOrigin();
            direction = 3;
            _num = number;
        }

        private bool IfOut()
        {
            return (X > 500 || X < 0 || Y > 500 || Y < 0);
        }

        public override void Update()
        {
            _tick++;
            if (_tick % 50 != 0) return;
            if (_num == 0)
            {
                if (direction == 0) X += 5;
                if (direction == 1) Y -= 5;
                if (direction == 2) Y += 5;
                if (direction == 3) X-=5;
                for (var i = 1; i < Global.shake.Count; i++)
                {
                    double Distance = (Global.shake[i].X - Global.shake[i - 1].X);
                    if (Distance > 5)
                    { 
                        Global.shake[i].X = Global.shake[i - 1].X - 6;
                    }
                    Distance = (Global.shake[i].Y - Global.shake[i - 1].Y);
                    if (Distance > 5)
                    {
                        Global.shake[i].Y = Global.shake[i - 1].Y;
                    }
                }
            }
                

            if (IfOut())
            {
                RemoveSelf();
                Game.Scene.Add(new MSG());
            }
            if (Global.player.Controller.Up.Down) direction = 1;
            if (Global.player.Controller.Down.Down) direction = 2;
            if (Global.player.Controller.Left.Down) direction = 3;
            if (Global.player.Controller.Right.Down) direction = 0;
            base.Update();
        }
    }
    class GameScene : Scene
    {
        public GameScene()
        {
            Add(new Background());

            Global.shake.Add(Add(new Unit(0)));
            Global.shake.Add(Add(new Unit(1)));
        }
    }
    class Global
    {
        public static List<Unit> shake = new List<Unit>();
        public static Session player;
    }
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game("g", 500, 500);

            Global.player = game.AddSession("red");

            Global.player.Controller.Up.AddKey(Key.Up);
            Global.player.Controller.Down.AddKey(Key.Down);
            Global.player.Controller.Left.AddKey(Key.Left);
            Global.player.Controller.Right.AddKey(Key.Right);

            game.FirstScene = new GameScene();

            game.Start();

        }
    }
}
