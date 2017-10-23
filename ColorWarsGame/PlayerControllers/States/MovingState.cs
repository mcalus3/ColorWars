using ColorWars.Services;

namespace ColorWars.PlayerControllers.States
{
    internal abstract class MovingState : IPlayerState
    {
        internal PlayerController Owner;
        internal Direction Direction { get; set; }
        protected int MoveTimer { get; set; }

        protected MovingState(PlayerController owner)
        {
            this.Owner = owner;
        }

        public virtual void OnUpdate()
        {
            if(this.MoveTimer != Owner.PlayerSpeed)
            {
                this.MoveTimer++;
            }
            else
            {
                this.MoveTimer = 0;
                Owner.Move(Owner.Direction);

                if(Owner.Field == null)
                {
                    Owner.Suicide();
                }

                Owner.InvokeOnPlayerEntered();
                Owner.Direction = Owner.BufferedDirection;
            }
            this.Owner.MovementFraction = (float)this.MoveTimer / this.Owner.PlayerSpeed;
        }

        public virtual void ChangeDirection(Direction direction)
        {
            this.Owner.BufferedDirection = direction;
        }
    }
}
