using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWars
{
    class InputController
    {
        private List<PlayerMoveCommand> commands;

        public InputController()
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
