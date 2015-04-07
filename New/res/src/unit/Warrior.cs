using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src.unit
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
            if (_hp > 0)
            {
                Bar = new Text(8);
                Bar.SetPosition(X, Y - 20);
                Bar.String = ((int)_hp).ToString();
                if (_team == Team.Blu) Bar.Color = Color.Green;
                else Bar.Color = Color.Red;
                GameScene.Instance.AddGraphics(Bar);
            }
            base.Update();
        }
    }
}