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
        private int? _cost;
        private Image _disabledVersion;
        private Image _normalVersion;
        public ClickAction onClick = delegate() { };    
        public Control(string path, float x, float y, int? cost = null, string disabledPath = null)
        {
            _normalVersion = new Image(path);
            if (disabledPath != null) _disabledVersion = new Image(disabledPath);
            SetGraphic(_normalVersion);
            X = x;
            Y = y;
            _cost = cost;
        }

        public override void Update()
        {
            if (_cost != null)
            {
                if (Teams.playerBlue._coins < _cost && Graphic != _disabledVersion) SetGraphic(_disabledVersion);
                else if (Teams.playerBlue._coins >= _cost && Graphic != _normalVersion) SetGraphic(_normalVersion);
            }
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
