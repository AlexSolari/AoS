using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src.player
{
    class Player
    {
        protected int _team;
        public int _bonusDamage = 0;
        public int _bonusArmor = -1;
        public int _bonusHP = 0;

        public Player(int team)
        {
            _team = team;
        }
        public void Upgrade(int type)
        {
            switch (type)
            {
                case Upgades.Armor:
                    _bonusArmor++;
                    break;
                case Upgades.Damage:
                    _bonusDamage++;
                    break;
                case Upgades.HP:
                    _bonusHP++;
                    break;
                default:
                    break;
            }
        }
    }
}
