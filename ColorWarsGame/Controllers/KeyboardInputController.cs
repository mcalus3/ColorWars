using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWars
{
    class KeyboardInputController
    {
        private List<PlayerMoveCommand> commands;

        public KeyboardInputController()
        {
            this.commands = new List<PlayerMoveCommand>();
        }

        internal void AddInputCommand(PlayerMoveCommand command)
        {
            this.commands.Add(command);
        }

        internal void ExecuteCommands()
        {
            foreach(PlayerMoveCommand command in this.commands)
            {
                command.Execute();
            }
        }
    }
}
