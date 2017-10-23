using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ColorWars.Players;
using ColorWars.Services;

namespace ColorWars.Graphics
{
    internal class PlayerRenderer : GameElementRenderer
    {
        internal PlayerModel RenderedPlayer { get; set; }

        public PlayerRenderer(GraphicsDeviceManager gdManager, PlayerModel drawedObject, Point mapDimension, SpriteBatch sBatch)
            : base(gdManager, drawedObject, mapDimension, sBatch)
        {
            this.RenderedPlayer = drawedObject;
        }

        public override void Draw()
        {
            //for every field that rendered object is occupying write a rectangle covering whole field
            foreach (Point field in this.RenderedPlayer.GetPoints())
            {
                base.SpriteBatch.Draw(base.Texture, this.GetPlayerLocation(base.GetRectangleFromField(field)), base.RenderedObject.GetColor());
            }
        }

        //returns rectangle in game window representing given game field 
        private Rectangle GetPlayerLocation(Rectangle rectangle)
        {
            int newX = rectangle.X;
            int newY = rectangle.Y;
            int differenceX = (int)(rectangle.Width * this.RenderedPlayer.MovementFraction);
            int differenceY = (int)(rectangle.Height * this.RenderedPlayer.MovementFraction);

            switch (this.RenderedPlayer.Direction)
            {
                case Direction.Up:
                    newY -= differenceY;
                    break;
                case Direction.Down:
                    newY += differenceY;
                    break;
                case Direction.Left:
                    newX -= differenceX;
                    break;
                case Direction.Right:
                    newX += differenceX;
                    break;
                case Direction.None:
                    break;
            }
            return new Rectangle(newX, newY, rectangle.Width, rectangle.Height);
        }
    }
}
