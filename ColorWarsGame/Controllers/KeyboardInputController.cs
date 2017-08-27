using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWars.Controllers
{
    class KeyboardInputController
    {
        public List<PlayerMoveCommand> Commands { get; set; }

        public KeyboardInputController()
        {
            this.Commands = new List<PlayerMoveCommand>();
        }

        internal void ExecuteCommands()
        {
            foreach(PlayerMoveCommand command in this.Commands)
            {
                command.Execute();
            }
        }
    }
}
