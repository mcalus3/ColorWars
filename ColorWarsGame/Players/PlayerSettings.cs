using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ColorWars
{
    class PlayerSettings
    {
        public Color color;
        public Dictionary<Keys, Direction> keyMapping;
    }
}
