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
                Bar = new Text(16);
                Bar.SetPosition(X - 16, Y - 60);
                Bar.String = ((int)_hp).ToString();
                if (_team == Team.Blu) Bar.Color = Color.Green;
                else Bar.Color = Color.Red;
                Bar.Render();
                GameScene.Instance.AddGraphics(Bar);
            }
            base.Update();
        }
    }
}
