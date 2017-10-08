using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorWars.Boards;

namespace ColorWars.Players.States
{
    interface IPlayerState
    {
        void OnUpdate();
        void ChangeDirection(Direction direction);
    }
}
