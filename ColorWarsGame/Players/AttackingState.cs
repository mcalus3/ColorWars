using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Boards;

namespace ColorWars.Players
{
    class AttackingState: IPlayerState
    {
        private Player owner;

        public AttackingState(Player owner)
        {
            this.owner = owner;
            this.OnMovement();
        }

        public void OnMovement()
        {
            if (this.owner.Position.Owner == this.owner)
            {
                this.owner.AddTerritory();
                this.owner.State = new DefensiveState(this.owner);
            }
            else
            {
                this.owner.SpawnTail();
            }
        }
    }
}
