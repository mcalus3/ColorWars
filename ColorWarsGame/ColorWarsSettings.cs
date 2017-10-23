using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using ColorWars.Players;
using ColorWars.Services;

namespace ColorWars
{
    internal class ColorWarsSettings
    {
        public Point WindowSize = new Point(800, 600);
        public Point MapDimension = new Point(30, 30);
        public int StartingTerritorySize = 2;
        public int EndTime = 100;

        public int PlayersCount = 4;
        public PlayerSettings[] Players = new PlayerSettings[] {
            new PlayerSettings() {
                Color = new Color(0,220,20,255),
                Speed = 8,
                DeathPenalty = 150,
                KeyMapping = {
                        { PlayerCommand.Up , Keys.W },
                        {  PlayerCommand.Down, Keys.S },
                        { PlayerCommand.Left, Keys.A },
                        { PlayerCommand.Right, Keys.D }
                    }
            },
            new PlayerSettings() {
                Color = new Color(230,0,50,255),
                Speed = 8,
                DeathPenalty = 150,
                KeyMapping = {
                    { PlayerCommand.Up , Keys.Up },
                    {  PlayerCommand.Down, Keys.Down },
                    { PlayerCommand.Left, Keys.Left },
                    { PlayerCommand.Right, Keys.Right }
                }
            },
            new PlayerSettings() {
                Color = new Color(200,165,0,255),
                Speed = 8,
                DeathPenalty = 150,
                KeyMapping = {
                    { PlayerCommand.Up , Keys.I },
                    {  PlayerCommand.Down, Keys.K },
                    { PlayerCommand.Left, Keys.J },
                    { PlayerCommand.Right, Keys.L }
                }
            },
            new PlayerSettings() {
                Color = new Color(40,0,200,255),
                Speed = 8,
                DeathPenalty = 150,
                KeyMapping = {
                    { PlayerCommand.Up , Keys.NumPad8 },
                    {  PlayerCommand.Down, Keys.NumPad5 },
                    { PlayerCommand.Left, Keys.NumPad4 },
                    { PlayerCommand.Right, Keys.NumPad6 }
                }
            }
        };
    }
}
