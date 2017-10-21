using ColorWars.Services;

namespace ColorWars.Players.States
{
    class NotMovingState : MovingState, IPlayerState
    {
        public NotMovingState(PlayerController owner) : base(owner)
        {
        }

        public override void ChangeDirection(Direction direction)
        {
            owner.Direction = direction;
            base.ChangeDirection(direction);
            owner.MovingState = new DefensiveState(owner);
        }

        public override void OnUpdate()
        {
        }
    }
}
