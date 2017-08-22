using Microsoft.Xna.Framework;
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
        private List<SquareRenderer> renderers2;
        private GraphicsDeviceManager graphics;
        private Point mapDimension;

        public GameRenderer(GraphicsDeviceManager gManager, Point mapDimension)
        {
            this.graphics = gManager;
            this.mapDimension = mapDimension;
            this.renderers = new List<SquareRenderer>();

            this.graphics.PreferredBackBufferWidth = 600;
            this.graphics.PreferredBackBufferHeight = 600; // TODO: Change for settings-sourced
        }

        public void Draw()
        {
            this.graphics.GraphicsDevice.Clear(Color.Black);
            foreach (SquareRenderer renderer in this.renderers)
            {
                renderer.Draw();
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
    }
}
