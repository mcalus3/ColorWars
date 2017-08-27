using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Players;
using ColorWars.Boards;

namespace ColorWars
{
    class ColorWarsSettings
    {
        public Point windowSize = new Point(800, 600);
        public Point mapDimension = new Point(20, 20);
        public int startingTerritorySize = 2;
        public PlayerSettings[] players = new PlayerSettings[] {
            new PlayerSettings() {
                color = Color.Green,
                speed = 15,
                deathPenalty = 15,
                keyMapping = {
                        { Direction.UP , Keys.W },
                        {  Direction.DOWN, Keys.S },
                        { Direction.LEFT, Keys.A },
                        { Direction.RIGHT, Keys.D }
                    }
            },
            new PlayerSettings() {
                color = Color.Blue,
                speed = 15,
                deathPenalty = 15,
                keyMapping = {
                    { Direction.UP , Keys.Up },
                    {  Direction.DOWN, Keys.Down },
                    { Direction.LEFT, Keys.Left },
                    { Direction.RIGHT, Keys.Right }
                }
            }
        };
    }
}
