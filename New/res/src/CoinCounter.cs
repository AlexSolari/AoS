using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using New.res.src.player;

namespace New.res.src
{
    class CoinCounter : Entity
    {
        private int _team;
        public CoinCounter(float x, float y, int team)
        {
            
            _team = team;
            SetGraphic(new Text());
            ((Text)Graphic).OutlineColor = Color.Black;
            ((Text)Graphic).OutlineThickness = 2;
            if (_team == Team.Blu) ((Text)Graphic).String = Teams.playerBlue.coins.ToString();
            else ((Text)Graphic).String = Teams.playerRed.coins.ToString();
            ((Text)Graphic).Color = Color.Gold;
            X = x;
            Y = y;
        }


        public override void Update()
        {
            if (_team == Team.Blu) ((Text)Graphic).String = Teams.playerBlue.coins.ToString();
            else ((Text)Graphic).String = Teams.playerRed.coins.ToString();
        }
    }
}
