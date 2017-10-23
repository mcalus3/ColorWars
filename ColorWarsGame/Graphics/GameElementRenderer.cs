using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ColorWars.Services;
using IDrawable = ColorWars.Services.IDrawable;

namespace ColorWars.Graphics
{
    internal abstract class GameElementRenderer : IDrawable
    {
        public ISquareDrawable RenderedObject { get; set; }
        protected SpriteBatch SpriteBatch;
        public Texture2D Texture { get; set; }
        protected GraphicsDeviceManager GdManager;
        protected Point MapDimension;

        protected GameElementRenderer(GraphicsDeviceManager gdManager, ISquareDrawable drawedObject, Point mapDimension, SpriteBatch sBatch)
        {
            this.RenderedObject = drawedObject;
            this.GdManager = gdManager;
            this.SpriteBatch = sBatch;
            this.MapDimension = mapDimension;

            //texture is one pixel with specified color
            this.Texture = new Texture2D(gdManager.GraphicsDevice, 1, 1);
            this.Texture.SetData(new[] { this.RenderedObject.GetColor() });
        }

        public virtual void Draw()
        {
            //for every field that rendered object is occupying write a rectangle covering whole field
            foreach (Point field in this.RenderedObject.GetPoints())
            {
                SpriteBatch.Draw(this.Texture, this.GetRectangleFromField(field), this.RenderedObject.GetColor());
            }
        }

        //returns rectangle in game window representing given game field 
        protected Rectangle GetRectangleFromField(Point field)
        {
            int width = GdManager.GraphicsDevice.Viewport.Width;
            int height = GdManager.GraphicsDevice.Viewport.Height;

            int tileWidth = width * 3 / 4 / this.MapDimension.X;
            int tileHeight = height / this.MapDimension.Y;
            return new Rectangle(field.X * tileWidth + width / 4, field.Y * tileHeight, tileWidth, tileHeight);
        }
    }
}
