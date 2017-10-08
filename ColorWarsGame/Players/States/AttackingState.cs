using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Boards;
using ColorWars.Services;

namespace ColorWars.Players.States
{
    class AttackingState : MovingState, IPlayerState
    {
        public AttackingState(Player owner) : base(owner)
        {
            base.owner.SpawnTail();
        }

        public override void ChangeDirection(Direction direction)
        {
            if(direction == PlayerServices.ReversedDirection(base.owner.BufferedDirection))
            {
                return;
            }

            base.ChangeDirection(direction);
        }

        public override void OnUpdate()
        {
            if(base.owner.Position.Owner == base.owner)
            {
                PlayerServices.AddTerritory(this.owner);

                base.owner.Tail.Delete();
                base.owner.OnTerritoryAdded(this.owner);

                base.owner.State = new DefensiveState(base.owner);
            }
            else
            {
                base.owner.SpawnTail();
            }
            base.OnUpdate();
        }
    }
}
