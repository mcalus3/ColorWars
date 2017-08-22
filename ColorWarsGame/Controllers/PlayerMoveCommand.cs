using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace ColorWars
{
    class PlayerMoveCommand
    {
        private Player player;
        private Dictionary<Keys, Direction> mapping;
        private KeyboardState oldState;

        public PlayerMoveCommand(Dictionary<Keys, Direction> mapping, Player player)
        {
            this.player = player;
            this.mapping = mapping;
        }

        internal void Execute()
        {
            KeyboardState newState = Keyboard.GetState();
            foreach (Keys key in mapping.Keys)
            {
                if (newState.IsKeyDown(key))
                {
                    player.ChangeDirection(mapping[key]);
                }
            }
            this.oldState = newState;  // set the new state as the old state for next time
        }
    }
}
