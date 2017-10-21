using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

using ColorWars.Players;
using ColorWars.Services;
using System;

namespace ColorWars.Actors
{
    class KeyboardActor
    {
        internal PlayerController Player { get; set; }
        private Dictionary<PlayerCommand, Keys> mapping;

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
                case PlayerCommand.UP:
                    this.Player.ChangeNextDirection(Direction.UP);
                    break;

                case PlayerCommand.DOWN:
                    this.Player.ChangeNextDirection(Direction.DOWN);
                    break;

                case PlayerCommand.LEFT:
                    this.Player.ChangeNextDirection(Direction.LEFT);
                    break;

                case PlayerCommand.RIGHT:
                    this.Player.ChangeNextDirection(Direction.RIGHT);
                    break;
            }
        }
    }
}
