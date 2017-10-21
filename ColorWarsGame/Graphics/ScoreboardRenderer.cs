using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Boards;
using ColorWars.Players;
using ColorWars.Services;

namespace ColorWars.Graphics
{
    class ScoreboardRenderer : IDrawable
    {
        private SpriteBatch sBatch;
        private Scoreboard scoreboard;
        private Dictionary<PlayerModel, Texture2D> playerTextures;
        private GraphicsDeviceManager gManager;
        private Texture2D blackTexture;

        public ScoreboardRenderer(GraphicsDeviceManager gdManager, Scoreboard scoreboard, SpriteBatch sBatch)
        {
            this.sBatch = sBatch;
            this.scoreboard = scoreboard;
            this.gManager = gdManager;
            this.playerTextures = new Dictionary<PlayerModel, Texture2D>();

            foreach (PlayerModel player in this.scoreboard.Players)
            {
                var texture = new Texture2D(gdManager.GraphicsDevice, 1, 1);
                texture.SetData(new[] { ColorTools.VeryTransparent(player.GetColor()) });
                this.playerTextures.Add(player, texture);
            }

            this.blackTexture = new Texture2D(gdManager.GraphicsDevice, 1, 1);
            blackTexture.SetData(new[] { Color.Black });

        }

        public void Draw()
        {
            int place = 0;

            foreach (PlayerModel player in this.scoreboard.Players)
            {
                int percentOfLeader;
                if (place == 0)
                {
                    percentOfLeader = 100;
                }
                else
                {
                    int leaderTerritory = this.scoreboard.Players[0].Stats.Territory;
                    if (leaderTerritory == 0)
                    {
                        percentOfLeader = 100;
                    }
                    else
                    {
                        percentOfLeader = 100 * player.Stats.Territory / this.scoreboard.Players[0].Stats.Territory;
                    }
                }

                var rectangle = this.GetRectangleFromPlace(place, percentOfLeader);
                var restRectangle = this.GetRestRectangleFromPlace(place, percentOfLeader);

                this.sBatch.Draw(playerTextures[player], rectangle, Color.White);
                this.sBatch.Draw(blackTexture, restRectangle, Color.White);

                place++;
            }
        }

        private Rectangle GetRectangleFromPlace(int place, int percent)
        {
            return new Rectangle(
                0,
                gManager.GraphicsDevice.Viewport.Height / this.scoreboard.Players.Count * place,
                gManager.GraphicsDevice.Viewport.Width / 4 * percent / 100,
                gManager.GraphicsDevice.Viewport.Height / this.scoreboard.Players.Count
                );
        }

        private Rectangle GetRestRectangleFromPlace(int place, int percent)
        {
            return new Rectangle(
                gManager.GraphicsDevice.Viewport.Width / 4 * percent / 100,
                gManager.GraphicsDevice.Viewport.Height / this.scoreboard.Players.Count * place,
                gManager.GraphicsDevice.Viewport.Width / 4 * (100 - percent) / 100,
                gManager.GraphicsDevice.Viewport.Height / this.scoreboard.Players.Count
                );
        }
    }
}
