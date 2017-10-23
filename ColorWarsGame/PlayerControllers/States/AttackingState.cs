using ColorWars.Services;

namespace ColorWars.PlayerControllers.States
{
    internal class AttackingState : MovingState
    {
        public AttackingState(PlayerController owner) : base(owner)
        {
            base.Owner.SpawnTail();
        }

        public override void ChangeDirection(Direction direction)
        {
            if(direction == PlayerServices.ReversedDirection(Owner.Direction))
            {
                return;
            }

            base.ChangeDirection(direction);
        }

        public override void OnUpdate()
        {
            if(Owner.OnOwnTerritory)
            {
                PlayerServices.AddTerritory(Owner.Player);

                Owner.DeleteTail();
                Owner.InvokeOnTerritoryAdded();

                Owner.MovingState = new DefensiveState(base.Owner);
            }
            else
            {
                Owner.SpawnTail();
            }
            base.OnUpdate();
        }
    }
}
