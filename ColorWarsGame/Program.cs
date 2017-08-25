using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

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
            ColorWarsSettings settings;
            try
            {
                settings = Newtonsoft.Json.JsonConvert.DeserializeObject<ColorWarsSettings>(File.ReadAllText("settings.json"));
            }
            catch (Exception ex)
            {
                settings = new ColorWarsSettings()
                {
                    startingTerritorySize = 2,
                    gameTime = 100,
                    mapDimension = new Point(30, 30),
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

                string json = Newtonsoft.Json.JsonConvert.SerializeObject(settings, Newtonsoft.Json.Formatting.Indented);
                File.WriteAllText("settings.json", json);
            }

            using (var game = new ColorWarsGame(settings))
                game.Run();
        }
    }
}
