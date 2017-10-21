using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorWars.Boards;
using ColorWars.Services;

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
