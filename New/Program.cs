using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
namespace New
{
    class Program
    {
        public static Game game = new Game("Game", Global.Width, Global.Height);
        static void Main(string[] args)
        {           
            Global.playerOne = game.AddSession("P1");

            //Global.playerOne.Controller.R1.AddKey(Key.W);
            //Global.playerOne.Controller.R2.AddKey(Key.S);
            Global.playerOne.Controller.A.AddMouseButton(MouseButton.Left);

            game.FirstScene = new GameScene();

            game.Start();
        }
    }
}
