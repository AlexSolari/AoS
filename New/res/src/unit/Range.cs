using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src.unit
{
    class Range : Warrior
    {
        public Range(int team, Point position)
            : base(@"range.png", team, position, Type.range)
        {

        }
    }
}