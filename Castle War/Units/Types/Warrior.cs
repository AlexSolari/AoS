using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.UI;

namespace CastleWar.Units.Types
{
    
    abstract class Warrior : Unit
    {
        public Warrior(string spritePath, int team, Point position, int type)
            : base(spritePath, team, position, type)
        {

        }

        public override void Update()
        {
            GameScene.Instance.RemoveGraphic<Text>(Bar);
            if (_hp > 0 && Global.tick % Global.HPBarsUpdateRate == 0)
            {
                GameScene.Instance.Add(new HPBar(X, Y, _hp, _team, 0));
            }
            base.Update();
        }
    }
}