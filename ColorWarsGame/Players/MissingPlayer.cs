using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ColorWars
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
