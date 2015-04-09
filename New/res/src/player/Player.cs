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

        private int _coins = 0;

        public int coins
        {
            get { return _coins;  }
        }

        public void AddCoin() { _coins++; }

        public void SpendCoin(int value) { _coins-= value; }

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
                    _bonusHP+=5;
                    break;
                default:
                    break;
            }
        }
    }
}
