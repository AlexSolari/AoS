using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.UI;
using CastleWar.Abilities;

namespace CastleWar.Units.Types
{
    class Hero : Unit
    {
        protected Ability _heroPassive;
        public Hero(string spritePath, int team, Point position, int type)
            :base(spritePath, team, position, type)
        {
            
        }
        public override void Update()
        {
            GameScene.Instance.RemoveGraphic<Text>(Bar);
            if (_hp > 0 && Global.tick % Global.HPBarsUpdateRate == 0)
            {
                GameScene.Instance.Add(new HPBar(X, Y, _hp, _team, 2));
            }
            base.Update();
        }
    }
}
