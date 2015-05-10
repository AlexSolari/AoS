using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.Players;

namespace CastleWar.UI.Counters
{
    class Counter : Entity
    {
        public Counter(float x, float y)
        {
            SetGraphic(new Text());
            ((Text)Graphic).OutlineColor = Color.Black;
            ((Text)Graphic).OutlineThickness = 2;
            ((Text)Graphic).Color = Color.White;
            X = x;
            Y = y;
        }
    }
}
