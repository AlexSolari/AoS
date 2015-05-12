using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.Players;

namespace CastleWar.UI.Counters
{
    class AddArmorCounter : Counter
    {
        private Team _team;
        public AddArmorCounter(float x, float y, Team team)
            : base(x, y)
        {
            _team = team;
            if (_team == Team.Blu) ((Text)Graphic).String = (Teams.playerBlue._bonusArmor+1).ToString();
            else ((Text)Graphic).String = (Teams.playerRed._bonusArmor+1).ToString();
        }


        public override void Update()
        {
            if (_team == Team.Blu) ((Text)Graphic).String = (Teams.playerBlue._bonusArmor + 1).ToString();
            else ((Text)Graphic).String = (Teams.playerRed._bonusArmor + 1).ToString();
        }
    }
}
