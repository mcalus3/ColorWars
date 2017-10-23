using Microsoft.Xna.Framework;

namespace ColorWars.Players
{
    internal class MissingPlayer : IPlayer
    {
        public static readonly IPlayer Instance = new MissingPlayer();

        public Color GetColor()
        {
            return Color.White;
        }
    }
}
