using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src.unit
{
    class Siege : Warrior
    {
        public Siege(int team, Point position)
            : base(@"siege.png", team, position, Type.siege)
        {

        }
    }
}