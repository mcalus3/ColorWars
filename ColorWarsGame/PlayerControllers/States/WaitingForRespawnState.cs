namespace ColorWars.Players.States
{
    class WaitingForRespawnState : MovingState
    {
        private int RestoreTimer;

        public WaitingForRespawnState(PlayerController owner) : base(owner)
        {
            this.RestoreTimer = owner.DeathPenalty;

            owner.DeleteTail();
            owner.Field = owner.StartField;
        }

        public override void OnUpdate()
        {
            if(this.RestoreTimer == 0)
            {
                owner.MovingState = new DefensiveState(owner);
            }
            this.RestoreTimer--;
        }
    }
}
