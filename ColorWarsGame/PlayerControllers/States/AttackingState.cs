using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ColorWars.Boards;
using ColorWars.Services;

namespace ColorWars.Players.States
{
    class AttackingState : MovingState
    {
        public AttackingState(PlayerController owner) : base(owner)
        {
            base.owner.SpawnTail();
        }

        public override void ChangeDirection(Direction direction)
        {
            if(direction == PlayerServices.ReversedDirection(owner.Direction))
            {
                return;
            }

            base.ChangeDirection(direction);
        }

        public override void OnUpdate()
        {
            if(owner.OnOwnTerritory)
            {
                PlayerServices.AddTerritory(owner.Player);

                owner.DeleteTail();
                owner.InvokeOnTerritoryAdded();

                owner.MovingState = new DefensiveState(base.owner);
            }
            else
            {
                owner.SpawnTail();
            }
            base.OnUpdate();
        }
    }
}
