using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using New.res.src.player;

namespace New.res.src
{
    class AddArmorCounter : Counter
    {
        private int _team;
        public AddArmorCounter(float x, float y, int team)
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
