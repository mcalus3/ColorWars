using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Players;

namespace ColorWars.Graphics
{
    class GameRenderer
    {
        public List<SquareRenderer> Renderers {get; set;}
        public GraphicsDeviceManager Graphics{get; set;}
        private Point mapDimension;

        public GameRenderer(GraphicsDeviceManager gManager, Point mapDimension, Point windowSize)
        {
            this.Graphics = gManager;
            this.Graphics.PreferredBackBufferWidth = windowSize.X;
            this.Graphics.PreferredBackBufferHeight = windowSize.Y;
            this.mapDimension = mapDimension;
            this.Renderers = new List<SquareRenderer>();
        }

        public void DrawBoard()
        {
            this.Graphics.GraphicsDevice.SetRenderTarget(null);
            this.Graphics.GraphicsDevice.Clear(Color.Black);

            foreach (SquareRenderer renderer in this.Renderers)
            {
                renderer.Draw();
            }
        }

        internal void DrawScoreboard(IPlayer[] playerList)
        {
            int playerNumber = 0;
            GraphicsDevice g = this.Graphics.GraphicsDevice;
            int playersCount = playerList.Count();

            int scoreWidth = g.Viewport.Width / 4;
            int scoreHeight = g.Viewport.Height / playersCount;

            foreach (IPlayer player in playerList)
            {
                var color = player.GetColor();
                color.A = 50;
                var texture = new Texture2D(g, 1, 1);
                texture.SetData(new[] { color });
                var spriteBatch = new SpriteBatch(g);

                int scorePos = g.Viewport.Height / playersCount * playerNumber;
                var rectangle = new Rectangle(0, scorePos, scoreWidth, scoreHeight);

                spriteBatch.Begin();
                spriteBatch.Draw(texture, rectangle, color);
                spriteBatch.End();

                playerNumber++;
            }
        }

        public void AddRenderer(ISquareDrawable drawable)
        {
            this.Renderers.Add(new SquareRenderer(this.Graphics, drawable, this.mapDimension));
        }
    }
}
