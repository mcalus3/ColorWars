using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Boards;
using ColorWars.Services;
using Microsoft.Xna.Framework;

namespace ColorWars.Players.States
{
    class DeadState : IPlayerState
    {
        private Player owner;

        public DeadState(Player owner)
        {
            this.owner = owner;
            owner.Position = new BoardField(Player.MISSING, new Point(-1, -1));
        }

        public void ChangeDirection(Direction direction)
        {

        }

        public void OnMovement()
        {

        }
    }
}
