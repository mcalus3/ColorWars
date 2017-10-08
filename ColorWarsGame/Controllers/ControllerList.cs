using ColorWars.Players;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWars.Controllers
{
    class ControllerList
    {
        public List<PlayerKeyboardController> Commands { get; set; }

        public ControllerList()
        {
            this.Commands = new List<PlayerKeyboardController>();
        }

        internal void ExecuteCommands()
        {
            KeyboardState state = Keyboard.GetState();
            foreach(PlayerKeyboardController command in this.Commands)
            {
                command.Execute(state);
            }
        }

        internal void RemoveCommand(IPlayer player)
        {
            this.Commands.RemoveAll(c => c.Player == player);
        }
    }
}
