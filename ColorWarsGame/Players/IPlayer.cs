using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ColorWars.Players
{
    interface IPlayer
    {
        Color GetColor();
        Point[] GetPoints();
    }
}
