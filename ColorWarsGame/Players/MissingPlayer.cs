using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace ColorWars.Players
{
    class MissingPlayer : IPlayer
    {
        public Color GetColor()
        {
            return Color.White;
        }

        public Point[] GetPoints()
        {
            throw new InvalidOperationException();
        }
    }
}
