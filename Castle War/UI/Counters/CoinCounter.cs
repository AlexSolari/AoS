using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.Players;

namespace CastleWar.UI.Counters
{
    class CoinCounter : Counter
    {
        private int _team;
        public CoinCounter(float x, float y, int team): base(x, y)
        {
            _team = team;
            if (_team == Team.Blu) ((Text)Graphic).String = Teams.playerBlue.coins.ToString();
            else ((Text)Graphic).String = Teams.playerRed.coins.ToString();
            ((Text)Graphic).Color = Color.Gold;
        }


        public override void Update()
        {
            if (_team == Team.Blu) ((Text)Graphic).String = Teams.playerBlue.coins.ToString();
            else ((Text)Graphic).String = Teams.playerRed.coins.ToString();
        }
    }
}
