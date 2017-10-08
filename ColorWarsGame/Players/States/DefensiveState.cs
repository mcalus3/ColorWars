using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorWars.Boards;
using ColorWars.Services;

namespace ColorWars.Players.States
{
    class DefensiveState : MovingState, IPlayerState
    {
        public DefensiveState(Player owner) : base(owner)
        {
        }

        public override void OnUpdate()
        {
            if(base.owner.Position.Owner != base.owner)
            {
                this.owner.State = new AttackingState(this.owner);
            }
            base.OnUpdate();
        }
    }
}
