using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Players;

namespace ColorWars
{
    class ColorWarsSettings
    {
        public Point mapDimension = new Point(10, 10);
        public int startingTerritorySize = 2;
        public PlayerSettings[] players = new PlayerSettings[] {
            new PlayerSettings() {
                color = Color.Blue
            },
            new PlayerSettings() {
                color = Color.Green
            }
        };
    }
}
