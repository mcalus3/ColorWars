using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Players;
using ColorWars.Boards;

namespace ColorWars.Graphics
{
    class GameRenderer
    {
        public List<IDrawable> Renderers {get; set;}
        public GraphicsDeviceManager Graphics{get; set;}
        private Point mapDimension;
        private SpriteBatch sBatch;

        public GameRenderer(GraphicsDeviceManager gManager, ColorWarsSettings settings)
        {
            this.Graphics = gManager;
            this.Graphics.PreferredBackBufferWidth = settings.windowSize.X;
            this.Graphics.PreferredBackBufferHeight = settings.windowSize.Y;
            this.mapDimension = settings.mapDimension;

            this.Renderers = new List<IDrawable>();
        }

        public void Initialize()
        {
            this.sBatch = new SpriteBatch(this.Graphics.GraphicsDevice);
        }

        public void DrawBoard()
        {
            this.sBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            foreach (IDrawable renderer in this.Renderers)
            {
                renderer.Draw();
            }

            this.sBatch.End();
        }

        public void CreateFieldRenderers(ISquareDrawable[] drawables)
        {
            foreach (ISquareDrawable drawable in drawables)
            {
                this.Renderers.Add(new FieldRenderer(this.Graphics, drawable, this.mapDimension, this.sBatch));
            }
        }

        public void CreatePlayerRenderers(Player[] drawables)
        {
            foreach (Player drawable in drawables)
            {
                this.Renderers.Add(new PlayerRenderer(this.Graphics, drawable, this.mapDimension, this.sBatch));
            }
        }
    }
}
