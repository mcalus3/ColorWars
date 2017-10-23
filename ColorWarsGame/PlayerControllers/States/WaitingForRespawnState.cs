namespace ColorWars.PlayerControllers.States
{
    internal class WaitingForRespawnState : MovingState
    {
        private int restoreTimer;

        public WaitingForRespawnState(PlayerController owner) : base(owner)
        {
            this.restoreTimer = owner.DeathPenalty;

            owner.DeleteTail();
            owner.Field = owner.StartField;
        }

        public override void OnUpdate()
        {
            if(this.restoreTimer == 0)
            {
                Owner.MovingState = new DefensiveState(Owner);
            }
            this.restoreTimer--;
        }
    }
}
