﻿using System;
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
            _creepsCooldown = 500;
            _siegeCD = 1600;
            _creepCount = 0;
        }

        public override void Update()
        {
            if (!isInitialized)
            {

                _armor = 10;
                _range = 140;
                _hp = 800;
                _damage = 2;
                _gun = new AncientWeapon(_damage, GameScene.Instance);
                _cooldownValue = 2;
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
                    GameScene.Instance.Add(new Melee(_team, new Point((int)(X - 20), (int)(Y + _team * 10))));
                    _creepCount++;
                    break;
                case 1:
                    GameScene.Instance.Add(new Melee(_team, new Point((int)(X + 20 ), (int)(Y + _team * 10))));
                    _creepCount++;
                    break;
                case 2:
                    GameScene.Instance.Add(new Range(_team, new Point((int)(X), (int)(Y - _team * 30))));
                    _creepCount++;
                    break;
                case 3:
                    if (_siegeCD <= 0)
                    {
                        GameScene.Instance.Add(new Siege(_team, new Point((int)(X), (int)(Y))));
                        _siegeCD = 1500;
                    }
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