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
                        { PlayerCommand.UP , Keys.W },
                        {  PlayerCommand.DOWN, Keys.S },
                        { PlayerCommand.LEFT, Keys.A },
                        { PlayerCommand.RIGHT, Keys.D }
                    }
            },
            new PlayerSettings() {
                color = new Color(230,0,50,255),
                speed = 8,
                deathPenalty = 150,
                keyMapping = {
                    { PlayerCommand.UP , Keys.Up },
                    {  PlayerCommand.DOWN, Keys.Down },
                    { PlayerCommand.LEFT, Keys.Left },
                    { PlayerCommand.RIGHT, Keys.Right }
                }
            },
            new PlayerSettings() {
                color = new Color(200,165,0,255),
                speed = 8,
                deathPenalty = 150,
                keyMapping = {
                    { PlayerCommand.UP , Keys.I },
                    {  PlayerCommand.DOWN, Keys.K },
                    { PlayerCommand.LEFT, Keys.J },
                    { PlayerCommand.RIGHT, Keys.L }
                }
            },
            new PlayerSettings() {
                color = new Color(40,0,200,255),
                speed = 8,
                deathPenalty = 150,
                keyMapping = {
                    { PlayerCommand.UP , Keys.NumPad8 },
                    {  PlayerCommand.DOWN, Keys.NumPad5 },
                    { PlayerCommand.LEFT, Keys.NumPad4 },
                    { PlayerCommand.RIGHT, Keys.NumPad6 }
                }
            }
        };
    }
}
