namespace ColorWars.Players.States
{
    class DefensiveState : MovingState
    {
        public DefensiveState(PlayerController owner) : base(owner)
        {
        }

        public override void OnUpdate()
        {
            if(!owner.OnOwnTerritory)
            {
                this.owner.MovingState = new AttackingState(this.owner);
            }
            base.OnUpdate();
        }
    }
}
