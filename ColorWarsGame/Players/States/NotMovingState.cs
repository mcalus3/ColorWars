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
    class NotMovingState : MovingState, IPlayerState
    {
        public NotMovingState(Player owner) : base(owner)
        {

        }

        public override void ChangeDirection(Direction direction)
        {
            base.owner.BufferedDirection = direction;
            base.ChangeDirection(direction);
            base.owner.State = new DefensiveState(owner);
        }

        public override void OnMovement()
        {

        }
    }
}
