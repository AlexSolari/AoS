using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.UI;
using CastleWar.Weapons;
using CastleWar.Units.Types.Warriors;

namespace CastleWar.Units.Types.Buildings
{
    class Ancient : Building
    {

        private int _creepsCD;
        private int _creepsCooldown;
        private int _siegeCD;
        private int _creepCount; 
        public Ancient(int team, Point position)
            : base(Assets.BuildingAncient, team, position, Type.ancient)
        {
            _creepsCD = 100;
            _creepsCooldown = 200;
            _siegeCD = _creepsCD + _creepsCooldown * 5;
            _creepCount = 0;
        }

        private void showResults()
        {
            var entities = GameScene.Instance.GetEntities<Entity>();

            foreach(Entity obj in entities)
            {
                if (obj != this)
                    obj.RemoveSelf();
            }
            Label tmp = null;
            if (_team == Team.Blu)
            {
                tmp = new Label(Game.HalfWidth, 200, "YOU LOSE", Color.Red, 0, false, 24);
            }
            else
            {
                tmp = new Label(Game.HalfWidth, 200, "YOU WIN", Color.Green, 0, false, 24);
            }
            GameScene.Instance.Add(new Cursor());
            tmp.Graphic.CenterOrigin();
            GameScene.Instance.Add(tmp);
            var statsOffset = Game.HalfWidth + 200;
            var exit = new Control("ui/buttonMenu.png", Game.HalfWidth - 65, 600);
            exit.onClick = delegate()
            {
                GameScene.Instance.RemoveAll();
                ((GameScene)GameScene.Instance).showMenu();
            };
            GameScene.Instance.Add(exit);
            var listResults = new List<Label>();
            listResults.Add(new Label(statsOffset, 250, "Units produced: " + StatisticWatcher.unitsProduced.ToString(), Color.White));
            listResults.Add(new Label(statsOffset, 300, "Units killed: " + StatisticWatcher.unitsKilled.ToString(), Color.White));
            listResults.Add(new Label(statsOffset, 350, "Damage done: " + StatisticWatcher.damageDone.ToString(), Color.White));
            listResults.Add(new Label(statsOffset, 400, "Coins earned: " + StatisticWatcher.coinsEarned.ToString(), Color.White));
            listResults.Add(new Label(statsOffset, 450, "HP healed: " + StatisticWatcher.healthHealed.ToString(), Color.White));
            listResults.Add(new Label(statsOffset, 500, "------------------", Color.White));
            listResults.Add(new Label(statsOffset, 550, "Total points: " + StatisticWatcher.totalPoints.ToString(), Color.Cyan));
            foreach (Label label in listResults)
            {
                label.Graphic.CenterOrigin();
                GameScene.Instance.Add(label);
            }

            using(var stream = new StreamWriter("results.dat",true))
            {
                stream.WriteLine(Environment.UserName + ':' + StatisticWatcher.totalPoints.ToString());
            }

            //statistic grafic here, still WIP


            float scaleX = (float)425 / (float)StatisticWatcher.statistic.Count;
            float scaleY = (float)325 / (float)StatisticWatcher.maxPoints;
            Point graphStart = new Point(50, 565);
            Console.WriteLine("Scale: {0}; {1}", scaleX, scaleY);
            var startValue = 0;
            var field = new Entity();
            if (scaleX < 1)
            {
                field.Graphic = Image.CreateRectangle(425, 325, Color.Mix(Color.Gray, Color.Black));
                scaleX = 1;
                startValue = StatisticWatcher.lastRecord - 425 * StatisticWatcher.updatePeriod;
            }
            else
            {
                field.Graphic = Image.CreateRectangle(425, 325, Color.Mix(Color.Gray, Color.Black));
            }

            field.SetPosition(50, 240);
            GameScene.Instance.Add(field);

            for (var time = startValue; time <= StatisticWatcher.lastRecord; time += StatisticWatcher.updatePeriod)
            {
                var temp = new Entity();
                temp.Graphic = Image.CreateRectangle((int)scaleX, (int)((StatisticWatcher.statistic[time]) * scaleY), Color.Cyan);
                temp.SetPosition(graphStart.X + (float)Math.Round(scaleX, 2) * time / StatisticWatcher.updatePeriod, graphStart.Y - StatisticWatcher.statistic[time] * scaleY);
                GameScene.Instance.Add(temp);
            }
        }
        public override void Damage(int damagePure)
        {
            base.Damage(damagePure);
            if (!alive)
                showResults();
        }
        public override void Update()
        {
            if (!isInitialized)
            {

                _armor = 10;
                _range = 140;
                _hp = 1000;
                _damage = 3;
                _gun = new DragonWeapon(_damage, GameScene.Instance);//NoWeapon(GameScene.Instance);
                _cooldownValue = 5;
                _speed = 0;
            }
            _creepsCD--;
            _siegeCD--;
            if(_creepsCD <= 0 )
            {
                spawnCreeps();
                
            }
            base.Update();
        }

        public void spawnCreeps()
        {
            switch (_creepCount)
            {
                case 0:
                    GameScene.Instance.Add(new Melee(_team, new Point((int)(X - 20 * GoodRnd.NextFloatBin()), (int)(Y + _team * 10))));
                    _creepCount++;
                    break;
                case 1:
                    GameScene.Instance.Add(new Melee(_team, new Point((int)(X + 20 * GoodRnd.NextFloatBin()), (int)(Y + _team * 10))));
                    _creepCount++;
                    break;
                case 2:
                    GameScene.Instance.Add(new Ranged(_team, new Point((int)(X), (int)(Y - _team * 10))));
                    _creepCount++;
                    break;
                case 3:
                    if (_siegeCD <= 0)
                    {
                        GameScene.Instance.Add(new Siege(_team, new Point((int)(X + _team * 20), (int)(Y - _team * 20))));
                        _siegeCD = 3 * _creepsCooldown;
                    }
                    _creepCount++;
                    break;
                case 4:
                    GameScene.Instance.Add(new Melee(_team, new Point((int)(X), (int)(Y))));
                    _creepCount++;
                    break;
                default:
                    _creepsCD = _creepsCooldown;
                    _creepCount = 0;
                    break;                        
            }
            
        }
    }
}
