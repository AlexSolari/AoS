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
        private int _creepsCooldown;
        private int _siegeCD;
        private int _creepCount; 
        public Ancient(int team, Point position)
            : base(@"ancient.png", team, position, Type.ancient)
        {
            _creepsCD = 100;
            _creepsCooldown = 600;
            _siegeCD = _creepsCD + _creepsCooldown * 3;
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

            var tmp = new Text(30);
            tmp.Y = 200;
            tmp.X = Game.HalfWidth;
            tmp.OutlineColor = Color.Black;
            tmp.OutlineThickness = 2;
            if (_team == Team.Blu)
            {
                tmp.String = "YOU LOSE";
                tmp.Color = Color.Red;

            }
            else
            {
                tmp.String = "YOU WIN";
                tmp.Color = Color.Green;
            }
            GameScene.Instance.Add(new Cursor());
            tmp.CenterOrigin();
            GameScene.Instance.AddGraphic(tmp);
            var exit = new Control("ui/buttonExit.png", Game.HalfWidth - 65, 600);
            exit.onClick = delegate()
            {
                System.Environment.Exit(0);
            };
            GameScene.Instance.Add(exit);
            var listResults = new List<Label>();
            listResults.Add(new Label(Game.HalfHeight, 250, "Units produced: " + StatisticWatcher.unitsProduced.ToString(), Color.White));
            listResults.Add(new Label(Game.HalfHeight, 300, "Units killed: " + StatisticWatcher.unitsKilled.ToString(), Color.White));
            listResults.Add(new Label(Game.HalfHeight, 350, "Damage done: " + StatisticWatcher.damageDone.ToString(), Color.White));
            listResults.Add(new Label(Game.HalfHeight, 400, "Coins earned: " + StatisticWatcher.coinsEarned.ToString(), Color.White));
            listResults.Add(new Label(Game.HalfHeight, 450, "HP healed: " + StatisticWatcher.healthHealed.ToString(), Color.White));
            listResults.Add(new Label(Game.HalfHeight, 500, "------------------", Color.White));
            listResults.Add(new Label(Game.HalfHeight, 550, "Total points: " + StatisticWatcher.totalPoints.ToString(), Color.Cyan));
            foreach (Label label in listResults)
            {
                label.Graphic.CenterOrigin();
                GameScene.Instance.Add(label);
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
                _gun = new NoWeapon(GameScene.Instance);
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
                    GameScene.Instance.Add(new Range(_team, new Point((int)(X), (int)(Y - _team * 10))));
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
