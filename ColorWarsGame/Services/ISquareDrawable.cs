using Microsoft.Xna.Framework;

namespace ColorWars.Services
{
    internal interface ISquareDrawable
    {
        Point[] GetPoints();
        Color GetColor();
    }
}
