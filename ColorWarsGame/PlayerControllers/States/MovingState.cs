using ColorWars.Services;

namespace ColorWars.Players.States
{
    abstract class MovingState : IPlayerState
    {
        internal PlayerController owner;
        internal Direction direction { get; set; }
        protected int MoveTimer { get; set; }

        public MovingState(PlayerController owner)
        {
            this.owner = owner;
        }

        public virtual void OnUpdate()
        {
            if(this.MoveTimer != owner.PlayerSpeed)
            {
                this.MoveTimer++;
            }
            else
            {
                this.MoveTimer = 0;
                owner.Move(owner.Direction);

                if(owner.Field == null)
                {
                    owner.Suicide();
                }

                owner.InvokeOnPlayerEntered();
                owner.Direction = owner.BufferedDirection;
            }
            this.owner.MovementFraction = (float)this.MoveTimer / this.owner.PlayerSpeed;
        }

        public virtual void ChangeDirection(Direction direction)
        {
            this.owner.BufferedDirection = direction;
        }
    }
}
