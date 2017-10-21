using Microsoft.Xna.Framework;

namespace ColorWars.Services
{
    interface ISquareDrawable
    {
        Point[] GetPoints();
        Color GetColor();
    }
}
