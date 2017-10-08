using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

using ColorWars.Players;
using ColorWars.Boards;

namespace ColorWars.Controllers
{
    class PlayerKeyboardController
    {
        internal Player Player { get; set; }
        private Dictionary<Direction, Keys> mapping;

        public PlayerKeyboardController(Dictionary<Direction, Keys> mapping, Player player)
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
