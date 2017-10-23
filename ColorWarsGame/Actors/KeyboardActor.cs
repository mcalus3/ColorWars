using System;
using System.Collections.Generic;
using ColorWars.PlayerControllers;
using Microsoft.Xna.Framework.Input;

using ColorWars.Services;

namespace ColorWars.Actors
{
    internal class KeyboardActor
    {
        internal PlayerController Player { get; set; }
        private readonly Dictionary<PlayerCommand, Keys> mapping;

        public KeyboardActor(Dictionary<PlayerCommand, Keys> mapping, PlayerController player)
        {
            this.Player = player;
            this.mapping = mapping;
        }

        internal void PollKeyboard(KeyboardState state)
        {
            foreach (PlayerCommand command in this.mapping.Keys)
            {
                if(state.IsKeyDown(mapping[command]))
                {
                    this.Execute(command);
                }
            }
        }

        private void Execute(PlayerCommand command)
        {
            switch (command)
            {
                case PlayerCommand.Up:
                    this.Player.ChangeNextDirection(Direction.Up);
                    break;

                case PlayerCommand.Down:
                    this.Player.ChangeNextDirection(Direction.Down);
                    break;

                case PlayerCommand.Left:
                    this.Player.ChangeNextDirection(Direction.Left);
                    break;

                case PlayerCommand.Right:
                    this.Player.ChangeNextDirection(Direction.Right);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(command), command, null);
            }
        }
    }
}
