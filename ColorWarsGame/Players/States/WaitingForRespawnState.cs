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
    class WaitingForRespawnState : MovingState, IPlayerState
    {
        private int RestoreTimer;

        public WaitingForRespawnState(Player owner) : base(owner)
        {
            this.RestoreTimer = owner.Settings.deathPenalty;

            owner.Tail.Delete();
            owner.Position = owner.startField;
        }

        public override void OnUpdate()
        {
            if(this.RestoreTimer == 0)
            {
                base.owner.State = new DefensiveState(owner);
            }
            this.RestoreTimer--;

        }
    }
}
