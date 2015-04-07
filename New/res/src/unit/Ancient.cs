using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;

namespace New.res.src.unit
{
    class Ancient : Building
    {

        private int _creepsCD;
        private int _cooldown;
        private int _siegeCD;
        public Ancient(int team, Point position)
            : base(@"ancient.png", team, position, Type.ancient)
        {
            _creepsCD = 100;
            _cooldown = 500;
            _siegeCD = 1600;
        }

        public override void Update()
        {
            
            _creepsCD--;
            _siegeCD--;
            if(_creepsCD <= 0 )
            {
                spawnCreeps();
                _creepsCD = _cooldown;
            }
            base.Update();
        }

        public void spawnCreeps()
        {
            Console.WriteLine("CREEPZ!!!!");

            Game.Scene.Add(new Melee(_team, new Point((int)(X + GoodRnd.gen.Next(-20,20)), (int)(Y + GoodRnd.gen.Next(-20, 20)))));


            Game.Scene.Add(new Melee(_team, new Point((int)(X + GoodRnd.gen.Next(-20, 20)), (int)(Y + GoodRnd.gen.Next(-20, 20)))));


            Game.Scene.Add(new Range(_team, new Point((int)(X + GoodRnd.gen.Next(-20, 20)), (int)(Y + GoodRnd.gen.Next(-20, 20)))));

            if (_siegeCD == 0)
            {
                Game.Scene.Add(new Siege(_team, new Point((int)(X), (int)(Y))));
                _siegeCD = 1500;
            }
        }
    }
}
