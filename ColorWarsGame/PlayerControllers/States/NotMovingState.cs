using ColorWars.Services;

namespace ColorWars.PlayerControllers.States
{
    internal class NotMovingState : MovingState
    {
        public NotMovingState(PlayerController owner) : base(owner)
        {
        }

        public override void ChangeDirection(Direction direction)
        {
            Owner.Direction = direction;
            base.ChangeDirection(direction);
            Owner.MovingState = new DefensiveState(Owner);
        }

        public override void OnUpdate()
        {
        }
    }
}
