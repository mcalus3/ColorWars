using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Boards;
using ColorWars.Services;
using Microsoft.Xna.Framework;

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
