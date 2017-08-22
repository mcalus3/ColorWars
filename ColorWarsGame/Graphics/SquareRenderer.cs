﻿using Microsoft.Xna.Framework;
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

            this.texture = new Texture2D(gdManager.GraphicsDevice, 1, 1);
            this.texture.SetData(new[] { this.renderedObject.GetColor() });
        }

        public void Draw()
        {
            spriteBatch.Begin();
            foreach (Point field in this.renderedObject.GetPoints())
            {
                spriteBatch.Draw(this.texture, this.GetRectangleFromField(field), Color.White);
            }
            spriteBatch.End();
        }

        private Rectangle GetRectangleFromField(Point field)
        {
            int tileWidth = gdManager.GraphicsDevice.Viewport.Width / this.mapDimension.X;
            int tileHeight = gdManager.GraphicsDevice.Viewport.Height / this.mapDimension.Y;
            return new Rectangle(field.X * tileWidth, field.Y * tileHeight, tileWidth, tileHeight);
        }
    }
}