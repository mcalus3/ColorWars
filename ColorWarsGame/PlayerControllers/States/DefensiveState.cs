namespace ColorWars.PlayerControllers.States
{
    internal class DefensiveState : MovingState
    {
        public DefensiveState(PlayerController owner) : base(owner)
        {
        }

        public override void OnUpdate()
        {
            if(!Owner.OnOwnTerritory)
            {
                this.Owner.MovingState = new AttackingState(this.Owner);
            }
            base.OnUpdate();
        }
    }
}
