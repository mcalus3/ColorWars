using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWars
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
            if (this.owner.position.owner == this.owner)
            {
                this.owner.AddTerritory();
                this.owner.state = new DefensiveState(this.owner);
            }
            else
            {
                this.owner.SpawnTail();
            }
        }
    }
}
