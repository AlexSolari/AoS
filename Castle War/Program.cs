using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace CastleWar
{
    class Program
    {
        public static Game game = new Game("Castle War", Global.Width, Global.Height);
        static void Main(string[] args)
        {           
            Global.playerOne = game.AddSession("Player 1");

            Global.playerOne.Controller.A.AddMouseButton(MouseButton.Left);

            game.FirstScene = new GameScene();

            game.Start();
        }
    }
}
