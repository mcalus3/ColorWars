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

        public void AddRenderer(ISquareDrawable drawable)
        {
            this.Renderers.Add(new SquareRenderer(this.Graphics, drawable, this.mapDimension));
        }
    }
}
