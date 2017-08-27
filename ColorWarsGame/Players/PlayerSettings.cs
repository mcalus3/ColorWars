using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Boards;

namespace ColorWars.Players
{
    class PlayerSettings
    {
        public Color color;
        public int speed;
        public int deathPenalty;
        public Dictionary<Direction, Keys> keyMapping = new Dictionary<Direction,Keys>();

    }
}
