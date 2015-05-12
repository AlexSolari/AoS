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
    abstract class Hero : Unit
    {
        protected Ability _heroPassive;
        public Hero(string spritePath, Team team, Point position, Type type)
            :base(spritePath, team, position, type)
        {
            
        }
        public override void Update()
        {
            GameScene.Instance.RemoveGraphic<Text>(Bar);
            if (_hp > 0 && Global.tick % Global.HPBarsUpdateRate == 0)
            {
                GameScene.Instance.Add(new HPBar(X, Y, _hp, _team, SubType.Hero));
            }
            base.Update();
        }
    }
}
