using ColorWars.Services;

namespace ColorWars.Players.States
{
    interface IPlayerState
    {
        void OnUpdate();
        void ChangeDirection(Direction direction);
    }
}
