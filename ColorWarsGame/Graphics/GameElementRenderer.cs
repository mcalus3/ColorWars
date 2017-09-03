using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Services;

namespace ColorWars.Graphics
{
    abstract class GameElementRenderer : IDrawable
    {
        public ISquareDrawable RenderedObject { get; set; }
        protected SpriteBatch spriteBatch;
        public Texture2D Texture { get; set; }
        protected GraphicsDeviceManager gdManager;
        protected Point mapDimension;

        public GameElementRenderer(GraphicsDeviceManager gdManager, ISquareDrawable drawedObject, Point mapDimension, SpriteBatch sBatch)
        {
            this.RenderedObject = drawedObject;
            this.gdManager = gdManager;
            this.spriteBatch = sBatch;
            this.mapDimension = mapDimension;

            //texture is one pixel with specified color
            this.Texture = new Texture2D(gdManager.GraphicsDevice, 1, 1);
            this.Texture.SetData(new[] { this.RenderedObject.GetColor() });
        }

        public virtual void Draw()
        {
            //for every field that rendered object is occupying write a rectangle covering whole field
            foreach (Point field in this.RenderedObject.GetPoints())
            {
                spriteBatch.Draw(this.Texture, this.GetRectangleFromField(field), this.RenderedObject.GetColor());
            }
        }

        //returns rectangle in game window representing given game field 
        protected Rectangle GetRectangleFromField(Point field)
        {
            int width = gdManager.GraphicsDevice.Viewport.Width;
            int height = gdManager.GraphicsDevice.Viewport.Height;

            int tileWidth = width * 3 / 4 / this.mapDimension.X;
            int tileHeight = height / this.mapDimension.Y;
            return new Rectangle(field.X * tileWidth + width / 4, field.Y * tileHeight, tileWidth, tileHeight);
        }
    }
}
