using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src.unit
{
    class Melee : Warrior
    {
        public Melee(int team, Point position)
            : base(@"melee.png", team, position, Type.melee)
        {

        }

        
    }
}