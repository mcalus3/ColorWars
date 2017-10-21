using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

using ColorWars.Players;
using ColorWars.Services;

namespace ColorWars.Actors
{
    class KeyboardActor
    {
        internal PlayerController Player { get; set; }
        private Dictionary<Direction, Keys> mapping;

        public KeyboardActor(Dictionary<Direction, Keys> mapping, PlayerController player)
        {
            this.Player = player;
            this.mapping = mapping;
        }

        internal void Execute(KeyboardState state)
        {
            foreach (Direction direction in this.mapping.Keys)
            {
                if (state.IsKeyDown(this.mapping[direction]))
                {
                    this.Player.ChangeNextDirection(direction);
                }
            }
        }
    }
}
