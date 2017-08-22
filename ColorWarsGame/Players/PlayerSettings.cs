using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ColorWars
{
    class PlayerSettings
    {
        public Color color;
        public Dictionary<Keys, Direction> keyMapping;
        public int speed;
    }
}
