using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src.unit
{
    abstract class Building : Unit
    {
        public Building(string spritePath, int team, Point position, int type)
            :base(spritePath, team, position, type)
        {
            
        }
        public override void Update()
        {
            GameScene.Instance.RemoveGraphic<Text>(Bar);
            if (_hp > 0)
            {
                GameScene.Instance.Add(new HPBar(X, Y, _hp, _team, 1));
            }
            base.Update();
        }
    }
}
