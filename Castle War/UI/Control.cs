using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace CastleWar.UI
{
    delegate void ClickAction();
    class Control : Entity
    {
        private int _clickCooldown = 30;
        public ClickAction onClick = delegate() { };    
        public Control(string path, float x, float y)
        {
            SetGraphic(new Image(path));
            X = x;
            Y = y;
        }

        public override void Update()
        {
            if (Game.Input.MouseX > X &&
                Game.Input.MouseX < X + Graphic.Width &&
                Game.Input.MouseY > Y &&
                Game.Input.MouseY < Y + Graphic.Height && 
                _clickCooldown <= 0 && Global.playerOne.Controller.A.Down)
            {
                onClick();
                _clickCooldown = 30;
            }
            _clickCooldown--;
        }
    }
}
