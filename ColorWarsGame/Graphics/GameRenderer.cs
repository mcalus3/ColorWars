using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWars
{
    class GameRenderer
    {
        private List<SquareRenderer> renderers;
        public GraphicsDeviceManager graphics;
        private Point mapDimension;

        public GameRenderer(GraphicsDeviceManager gManager, Point mapDimension, Point windowSize, ContentManager Content)
        {
            this.graphics = gManager;
            this.mapDimension = mapDimension;
            this.renderers = new List<SquareRenderer>();
            this.graphics.PreferredBackBufferWidth = windowSize.X;
            this.graphics.PreferredBackBufferHeight = windowSize.Y;

            //Content.RootDirectory = "Content";
            //this.font = Content.Load<SpriteFont>("File");

        }

        public void DrawBoard()
        {
            this.graphics.GraphicsDevice.SetRenderTarget(null);
            this.graphics.GraphicsDevice.Clear(Color.Black);

            foreach (SquareRenderer renderer in this.renderers)
            {
                renderer.Draw();
            }
        }

        internal void DrawScoreboard(IPlayer[] playerList)
        {
            int i = 0;
            foreach (IPlayer player in playerList)
            {
                var texture = new Texture2D(graphics.GraphicsDevice, 1, 1);
                var color = player.GetColor();
                color.A = 50;
                texture.SetData(new[] { color });
                var rectangle = new Rectangle(0, graphics.GraphicsDevice.Viewport.Height/4 * i, graphics.GraphicsDevice.Viewport.Width/4, graphics.GraphicsDevice.Viewport.Height/4);
                var spriteBatch = new SpriteBatch(graphics.GraphicsDevice);

                spriteBatch.Begin();
                spriteBatch.Draw(texture, rectangle, color);
                spriteBatch.End();

                i++;
            }


        }

        public void AddRenderer(ISquareDrawable drawable)
        {
            this.renderers.Add(new SquareRenderer(this.graphics, drawable, this.mapDimension));
        }

        public void RemoveRenderer(SquareRenderer renderer)
        {
            this.renderers.Remove(renderer);
        }

        internal void DrawEndGameMessage(IPlayer[] winners)
        {
            var texture = new Texture2D(graphics.GraphicsDevice, 1, 1);
            var color = winners[0].GetColor();
            color.A = 255;
            texture.SetData(new[] { color });
            var rectangle = new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            var spriteBatch = new SpriteBatch(graphics.GraphicsDevice);
            spriteBatch.Begin();
            spriteBatch.Draw(texture, rectangle, Color.White);
            spriteBatch.End();
        }

        internal void RemoveAllRenderers()
        {
            this.renderers = new List<SquareRenderer>();
        }

        private Rectangle GetRectangleFromField(Point field)
        {
            int tileWidth = graphics.GraphicsDevice.Viewport.Width / this.mapDimension.X;
            int tileHeight = graphics.GraphicsDevice.Viewport.Height / this.mapDimension.Y;
            return new Rectangle(field.X * tileWidth, field.Y * tileHeight, tileWidth, tileHeight);
        }
    }
}
