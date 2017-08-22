using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWars
{
    class SquareRenderer
    {
        private ISquareDrawable renderedObject;
        private SpriteBatch spriteBatch;
        private Texture2D texture;
        private GraphicsDeviceManager gdManager;
        private Point mapDimension;

        public SquareRenderer(GraphicsDeviceManager gdManager, ISquareDrawable drawedObject, Point mapDimension)
        {
            this.renderedObject = drawedObject;
            this.gdManager = gdManager;
            this.spriteBatch = new SpriteBatch(gdManager.GraphicsDevice);
            this.mapDimension = mapDimension;

            //texture is one pixel with specified color
            this.texture = new Texture2D(gdManager.GraphicsDevice, 1, 1);
            this.texture.SetData(new[] { this.renderedObject.GetColor() });
        }

        public void Draw()
        {
            spriteBatch.Begin();
            //for every field that rendered object is occupying write a rectangle covering whole field
            foreach (Point field in this.renderedObject.GetPoints())
            {
                spriteBatch.Draw(this.texture, this.GetRectangleFromField(field), this.renderedObject.GetColor());
            }
            spriteBatch.End();
        }

        //returns rectangle covering given game field
        private Rectangle GetRectangleFromField(Point field)
        {
            int tileWidth = gdManager.GraphicsDevice.Viewport.Width / this.mapDimension.X;
            int tileHeight = gdManager.GraphicsDevice.Viewport.Height / this.mapDimension.Y;
            return new Rectangle(field.X * tileWidth, field.Y * tileHeight, tileWidth, tileHeight);
        }
    }
}
