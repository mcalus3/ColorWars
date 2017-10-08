using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorWars.Boards;
using ColorWars.Services;

namespace ColorWars.Players.States
{
    abstract class MovingState : IPlayerState
    {
        internal Player owner;

        internal Direction direction { get; set; }

        public MovingState(Player owner)
        {
            this.owner = owner;
            this.direction = owner.BufferedDirection;
        }

        public virtual void ChangeDirection(Direction direction)
        {
            this.direction = direction;
        }

        public virtual void OnUpdate()
        {
            if(owner.MoveTimer != owner.Settings.speed)
            {
                owner.MoveTimer++;
            }
            else
            {
                owner.MoveTimer = 0;
                owner.Move(owner.BufferedDirection);

                if(owner.Position == null)
                {
                    owner.Kill(owner);
                }

                owner.Position.OnPlayerEntered(owner);

                owner.BufferedDirection = this.direction;
            }
        }
    }
}
