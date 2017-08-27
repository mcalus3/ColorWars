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
    class PlayerMoveCommand
    {
        private Player player;
        private Dictionary<Direction, Keys> mapping;

        public PlayerMoveCommand(Dictionary<Direction, Keys> mapping, Player player)
        {
            this.player = player;
            this.mapping = mapping;
        }

        internal void Execute()
        {
            KeyboardState state = Keyboard.GetState();
            foreach (Direction direction in this.mapping.Keys)
            {
                if (state.IsKeyDown(this.mapping[direction]))
                {
                    player.ChangeDirection(direction);
                }
            }
        }
    }
}
