using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src
{
    static class Bot
    {
        private static int _nextUpgrade = Upgades.Armor;
        public static void Update()
        {
            switch (_nextUpgrade)
            {
                case Upgades.Damage:
                    if (Teams.playerRed.coins >= 3) 
                    {
                        Teams.playerRed.SpendCoin(3);
                        Teams.playerRed.Upgrade(_nextUpgrade);
                        _nextUpgrade = GoodRnd.gen.Next(0, 3);
                        Console.WriteLine("RED: Upgraded Damage");
                    }
                    break;
                case Upgades.Armor:
                    if (Teams.playerRed.coins >= 10)
                    {
                        Teams.playerRed.SpendCoin(10);
                        Teams.playerRed.Upgrade(_nextUpgrade);
                        _nextUpgrade = GoodRnd.gen.Next(0, 3);
                        Console.WriteLine("RED: Upgraded Armor");
                    }
                    break;
                case Upgades.HP:
                    if (Teams.playerRed.coins >= 5)
                    {
                        Teams.playerRed.SpendCoin(5);
                        Teams.playerRed.Upgrade(_nextUpgrade);
                        _nextUpgrade = GoodRnd.gen.Next(0, 3);
                        Console.WriteLine("RED: Upgraded HP");
                    }
                    break;
                default:
                    break;
            }
        }
    }
    class Background : Entity
    {
        private int _clickCooldown = 0;
        public Background()
        {
            SetGraphic(new Image(@"background.png"));
            Graphic.Blend = BlendMode.Add;
        }

        private void checkForBlueButtons()
        {
            if (Game.Input.MouseX > Buttons.BluHP.X &&
                Game.Input.MouseX < Buttons.BluHP.X + Buttons.BluHP.Graphic.Width &&
                Game.Input.MouseY > Buttons.BluHP.Y &&
                Game.Input.MouseY < Buttons.BluHP.Y + Buttons.BluHP.Graphic.Height &&
                Teams.playerBlue.coins >= 5)
            {
                Teams.playerBlue.SpendCoin(5);
                Teams.playerBlue.Upgrade(Upgades.HP);
                Console.WriteLine("BLU: Upgraded HP");
            }
            if (Game.Input.MouseX > Buttons.BluArmor.X &&
                Game.Input.MouseX < Buttons.BluArmor.X + Buttons.BluArmor.Graphic.Width &&
                Game.Input.MouseY > Buttons.BluArmor.Y &&
                Game.Input.MouseY < Buttons.BluArmor.Y + Buttons.BluArmor.Graphic.Height && 
                Teams.playerBlue.coins >= 10)
            {
                Teams.playerBlue.SpendCoin(10);
                Teams.playerBlue.Upgrade(Upgades.Armor);
                Console.WriteLine("BLU: Upgraded Armor");
            }
            if (Game.Input.MouseX > Buttons.BluDamage.X &&
                Game.Input.MouseX < Buttons.BluDamage.X + Buttons.BluDamage.Graphic.Width &&
                Game.Input.MouseY > Buttons.BluDamage.Y &&
                Game.Input.MouseY < Buttons.BluDamage.Y + Buttons.BluDamage.Graphic.Height &&
                Teams.playerBlue.coins >= 3)
            {
                Teams.playerBlue.SpendCoin(3);
                Teams.playerBlue.Upgrade(Upgades.Damage);
                Console.WriteLine("BLU: Upgraded Damage");
            }
        }

        public override void Update()
        {
            Bot.Update();
            _clickCooldown--;
            if (_clickCooldown<=0 && Global.playerOne.Controller.A.Down)
            {
                _clickCooldown = 30;
                checkForBlueButtons();
            }
            if (Global.tick++ % 500 == 0)
            {
                Teams.playerBlue.AddCoin();
                Teams.playerRed.AddCoin();
            }
        }
    }
}
