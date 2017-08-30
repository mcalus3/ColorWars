using Microsoft.Xna.Framework;
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
    class PlayerRenderer : GameElementRenderer
    {
        private Player renderedPlayer;

        public PlayerRenderer(GraphicsDeviceManager gdManager, Player drawedObject, Point mapDimension, SpriteBatch sBatch)
            : base(gdManager, (ISquareDrawable)drawedObject, mapDimension, sBatch)
        {
            this.renderedPlayer = drawedObject;
        }

        public override void Draw()
        {
            //for every field that rendered object is occupying write a rectangle covering whole field
            foreach (Point field in this.renderedPlayer.GetPoints())
            {
                base.spriteBatch.Draw(base.Texture, this.GetPlayerLocation(base.GetRectangleFromField(field)), base.RenderedObject.GetColor());
            }
        }

        //returns rectangle in game window representing given game field 
        private Rectangle GetPlayerLocation(Rectangle rectangle)
        {
            int newX = rectangle.X;
            int newY = rectangle.Y;
            int differenceX = rectangle.Width * this.renderedPlayer.MoveTimer / this.renderedPlayer.Settings.speed;
            int differenceY = rectangle.Height * this.renderedPlayer.MoveTimer / this.renderedPlayer.Settings.speed;

            switch (this.renderedPlayer.BufferedDirection)
            {
                case Direction.UP:
                    newY -= differenceY;
                    break;
                case Direction.DOWN:
                    newY += differenceY;
                    break;
                case Direction.LEFT:
                    newX -= differenceX;
                    break;
                case Direction.RIGHT:
                    newX += differenceX;
                    break;
                case Direction.NONE:
                    break;
            }
            return new Rectangle(newX, newY, rectangle.Width, rectangle.Height);
        }
    }
}
