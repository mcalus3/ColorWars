using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

using ColorWars.Services;

namespace ColorWars.Players
{
    internal class PlayerSettings
    {
        public Color Color;
        public int Speed;
        public int DeathPenalty;
        public Dictionary<PlayerCommand, Keys> KeyMapping = new Dictionary<PlayerCommand,Keys>();

    }
}
