using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using New.res.src.player;

namespace New.res.src
{
    class AddDMGCounter : Counter
    {
        private int _team;
        public AddDMGCounter(float x, float y, int team)
            : base(x, y)
        {
            _team = team;
            if (_team == Team.Blu) ((Text)Graphic).String = Teams.playerBlue._bonusDamage.ToString();
            else ((Text)Graphic).String = Teams.playerRed._bonusDamage.ToString();
        }


        public override void Update()
        {
            if (_team == Team.Blu) ((Text)Graphic).String = Teams.playerBlue._bonusDamage.ToString();
            else ((Text)Graphic).String = Teams.playerRed._bonusDamage.ToString();
        }
    }
}
