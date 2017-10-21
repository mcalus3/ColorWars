using Microsoft.Xna.Framework;

namespace ColorWars.Players
{
    class MissingPlayer : IPlayer
    {
        public static readonly IPlayer Instance = new MissingPlayer();

        public Color GetColor()
        {
            return Color.White;
        }
    }
}
