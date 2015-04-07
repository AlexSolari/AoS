using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src.unit
{
    class Tower : Building
    {
        public Tower(int team, Point position)
            : base(@"tower.png", team, position, Type.tower)
        {
        }
    }
}
