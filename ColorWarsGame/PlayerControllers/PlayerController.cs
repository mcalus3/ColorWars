using System;
using ColorWars.Boards;
using ColorWars.PlayerControllers.States;
using ColorWars.Players;
using ColorWars.Services;

namespace ColorWars.PlayerControllers
{
    internal class PlayerController
    {
        public readonly PlayerModel Player;
        public int PlayerSpeed;
        public int DeathPenalty;
        public readonly BoardField StartField;
        public Direction BufferedDirection { get; set; }
        public Direction Direction { get => this.Player.Direction;
            set => this.Player.Direction = value;
        }
        public BoardField Field { get => this.Player.Position;
            set => this.Player.Position = value;
        }
        public float MovementFraction { get => this.Player.MovementFraction;
            set => this.Player.MovementFraction = value;
        }

        public MovingState MovingState { get; set; }

        public bool OnOwnTerritory => this.Field.Owner == this.Player;

        public PlayerController(PlayerModel player, BoardField startField, int speed, int deathPenalty)
        {
            this.Player = player;
            this.StartField = startField;
            this.PlayerSpeed = speed;
            this.DeathPenalty = deathPenalty;

            player.KilledEvent += this.OnKillHandler;

            this.MovingState = new NotMovingState(this);
        }

        public void Update()
        {
            this.MovingState.OnUpdate();
        }

        public virtual void ChangeNextDirection(Direction direction)
        {
            this.MovingState.ChangeDirection(direction);
        }

        private void OnKillHandler(object sender, EventArgs e)
        {
            this.MovingState = new WaitingForRespawnState(this);
        }

        public void Move(Direction direction)
        {
            this.Player.Move(direction);
        }

        public void SpawnTail()
        {
            this.Player.SpawnTail();
        }

        public void DeleteTail()
        {
            this.Player.Tail.Delete();
        }

        public void Suicide()
        {
            this.Player.Kill(Player);
        }

        public void InvokeOnPlayerEntered()
        {
            this.Player.Position.OnPlayerEntered(Player);
        }

        public void InvokeOnTerritoryAdded()
        {
            this.Player.OnTerritoryAdded();
        }

    }
}
