using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ColorWars
{
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var settings = new ColorWarsSettings() {
                startingTerritorySize = 2,
                gameTime = 10,
                dimension = new Point(30,30),
                windowSize = new Point(800, 600),
                playerSettings = new PlayerSettings[] {
                    new PlayerSettings() {
                        speed = 10,
                        color = Color.Blue,
                        keyMapping = new Dictionary<Keys, Direction>() {
                            { Keys.Up, Direction.UP },
                            {  Keys.Down, Direction.DOWN },
                            { Keys.Left, Direction.LEFT },
                            { Keys.Right, Direction.RIGHT },
                        }
                    },
                    new PlayerSettings() {
                        speed = 10,
                        color = Color.Green,
                        keyMapping = new Dictionary<Keys, Direction>() {
                            { Keys.W, Direction.UP },
                            { Keys.S, Direction.DOWN },
                            { Keys.A, Direction.LEFT },
                            { Keys.D, Direction.RIGHT },
                        }
                    }
                }
            };
            using (var game = new ColorWarsGame(settings))
                game.Run();
        }
    }
}
