using Microsoft.Xna.Framework;

namespace ColorWars.Services
{
    class ColorTools
    {
        public static Color HalfTransparent(Color color)
        {
            var A = 180;
            var R = color.R;
            var G = color.G;
            var B = color.B;
            return new Color(R, G, B, A);
        }

        public static Color VeryTransparent(Color color)
        {
            var A = 50;
            var R = color.R;
            var G = color.G;
            var B = color.B;
            return new Color(R, G, B, A);
        }

        public static Color TwiceLighter(Color color)
        {
            var A = 160;
            var R = 255 - ((255 - color.R) / 4*2);
            var G = 255 - ((255 - color.G) / 4 * 2);
            var B = 255 - ((255 - color.B) / 4 * 2);
            return new Color(R, G, B, A);
        }
    }
}
