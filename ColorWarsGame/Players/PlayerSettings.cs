using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Services;

namespace ColorWars.Players
{
    class PlayerSettings
    {
        public Color color;
        public int speed;
        public int deathPenalty;
        public Dictionary<PlayerCommand, Keys> keyMapping = new Dictionary<PlayerCommand,Keys>();

    }
}
