using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using CastleWar.Units;
using CastleWar.Players.Bot;

namespace CastleWar.UI
{
    
    class Background : Entity
    {
        private bool gameStarted;
        public Background(string sprite, bool mode = true)
        {
            SetGraphic(new Image(sprite));
            Graphic.Blend = BlendMode.Add;
            gameStarted = mode;
        }
        public override void Update()
        {
            if (gameStarted && !Global.isGamePaused)
            {
                Bot.Update();
                if (Global.tick % StatisticWatcher.updatePeriod == 0)
                {
                    StatisticWatcher.trackPoints(Global.tick);
                }
                if (Global.tick++ % 500 == 0)
                {
                    StatisticWatcher.trackCoins(1);
                    Teams.playerBlue.AddCoin();
                    Teams.playerRed.AddCoin();
                }
            }
            
        }
    }
}
