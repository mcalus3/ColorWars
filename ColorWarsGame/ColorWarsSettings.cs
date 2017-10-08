using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ColorWars.Players;
using ColorWars.Services;

namespace ColorWars
{
    class ColorWarsSettings
    {
        public Point windowSize = new Point(800, 600);
        public Point mapDimension = new Point(30, 30);
        public int startingTerritorySize = 2;
        public int endTime = 100;

        public int playersCount = 4;
        public PlayerSettings[] players = new PlayerSettings[] {
            new PlayerSettings() {
                color = new Color(0,220,20,255),
                speed = 8,
                deathPenalty = 150,
                keyMapping = {
                        { Direction.UP , Keys.W },
                        {  Direction.DOWN, Keys.S },
                        { Direction.LEFT, Keys.A },
                        { Direction.RIGHT, Keys.D }
                    }
            },
            new PlayerSettings() {
                color = new Color(230,0,50,255),
                speed = 8,
                deathPenalty = 150,
                keyMapping = {
                    { Direction.UP , Keys.Up },
                    {  Direction.DOWN, Keys.Down },
                    { Direction.LEFT, Keys.Left },
                    { Direction.RIGHT, Keys.Right }
                }
            },
            new PlayerSettings() {
                color = new Color(200,165,0,255),
                speed = 8,
                deathPenalty = 150,
                keyMapping = {
                    { Direction.UP , Keys.I },
                    {  Direction.DOWN, Keys.K },
                    { Direction.LEFT, Keys.J },
                    { Direction.RIGHT, Keys.L }
                }
            },
            new PlayerSettings() {
                color = new Color(40,0,200,255),
                speed = 8,
                deathPenalty = 150,
                keyMapping = {
                    { Direction.UP , Keys.NumPad8 },
                    {  Direction.DOWN, Keys.NumPad5 },
                    { Direction.LEFT, Keys.NumPad4 },
                    { Direction.RIGHT, Keys.NumPad6 }
                }
            }
        };
    }
}
