using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWars
{
    class DefensiveState: IPlayerState
    {
        private Player owner;

        public DefensiveState(Player owner)
        {
            this.owner = owner;
        }

        public void OnMovement()
        {
            if (this.owner.position.owner != this.owner)
            {
                this.owner.state = new AttackingState(this.owner);
            }
        }
    }
}
