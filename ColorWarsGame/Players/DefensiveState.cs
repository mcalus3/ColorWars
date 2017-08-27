using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWars.Players
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
            if (this.owner.Position.Owner != this.owner)
            {
                this.owner.State = new AttackingState(this.owner);
            }
        }
    }
}
