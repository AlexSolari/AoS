using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src
{
    class HPBar : Entity
    {
        private int _hp = 0;
        private int _team = 0;
        public HPBar(float x, float y, int hp, int team, int type)
        {
            if (type == 0)
            {
                X = x - 8;
                Y = y - 30;
                SetGraphic(new Text(10));
            }
            else
            {
                X = x - 16;
                Y = y - 60;
                SetGraphic(new Text(16));
            }
            _hp = hp;
            _team = team;
            
            ((Text)Graphic).OutlineColor = Color.Black;
            ((Text)Graphic).OutlineThickness = 2;
            ((Text)Graphic).String = ((int)_hp).ToString();
            if (_team == Team.Blu) ((Text)Graphic).Color = Color.Green;
            else ((Text)Graphic).Color = Color.Red;

        }


        public override void Update()
        {
            RemoveSelf();
        }
    }
}
