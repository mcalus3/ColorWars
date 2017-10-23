using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using ColorWars.Services;

namespace ColorWars.Graphics
{
    internal class FieldRenderer : GameElementRenderer
    {
        public FieldRenderer(GraphicsDeviceManager gdManager, ISquareDrawable drawedObject, Point mapDimension, SpriteBatch sBatch)
            : base(gdManager, drawedObject, mapDimension, sBatch)
        {
        }

        public override void Draw()
        {
            //for every field that rendered object is occupying write a rectangle covering whole field
            foreach (Point field in base.RenderedObject.GetPoints())
            {
                base.SpriteBatch.Draw(base.Texture, base.GetRectangleFromField(field), base.RenderedObject.GetColor());
            }
        }
    }
}
