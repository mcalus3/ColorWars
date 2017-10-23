using Microsoft.Xna.Framework;

namespace ColorWars.Services
{
    internal class ColorTools
    {
        public static Color HalfTransparent(Color color)
        {
            var a = 180;
            var r = color.R;
            var g = color.G;
            var b = color.B;
            return new Color(r, g, b, a);
        }

        public static Color VeryTransparent(Color color)
        {
            var a = 50;
            var r = color.R;
            var g = color.G;
            var b = color.B;
            return new Color(r, g, b, a);
        }

        public static Color TwiceLighter(Color color)
        {
            var a = 160;
            var r = 255 - ((255 - color.R) / 4*2);
            var g = 255 - ((255 - color.G) / 4 * 2);
            var b = 255 - ((255 - color.B) / 4 * 2);
            return new Color(r, g, b, a);
        }
    }
}
