using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using ColorWars.Boards;
using ColorWars.Players.States;
using ColorWars.Services;


namespace ColorWars.Players
{
    class Player : IPlayer, ISquareDrawable
    {
        public static readonly IPlayer MISSING = new MissingPlayer();

        public readonly BoardField startField;
        public PlayerSettings Settings { get; set; }
        public Tail Tail { get; set; }
        public PlayerStats Stats { get; set; }
        public event EventHandler TerritoryAddedEvent;

        public IPlayerState State { get; set; }
        public BoardField Position { get; set; }
        public Direction BufferedDirection { get; set; }
        public int MoveTimer { get; set; }

        public Player(PlayerSettings settings, BoardField startField)
        {
            this.Settings = settings;
            this.Position = startField;
            this.startField = startField;
            this.BufferedDirection = Direction.NONE;
            this.State = new NotMovingState(this);
            this.Tail = new Tail(this);
            this.Stats = new PlayerStats();
        }

        public Color GetColor()
        {
            return this.Settings.color;
        }

        public Point[] GetPoints()
        {
            return new Point[] { this.Position.GetPoints()[0] };
        }

        public void Move()
        {
            this.State.OnMovement();
        }

        public void SpawnTail()
        {
            this.Tail.AddField(this.Position);
        }

        public void ChangeDirection(Direction direction)
        {
            this.State.ChangeDirection(direction);
        }

        internal void OnTerritoryAdded(Player player)
        {
            this.TerritoryAddedEvent?.Invoke(player, new EventArgs());
        }

        public void Kill(Player killer)
        {
            this.State = new WaitingForRespawnState(this);
        }

        internal void RemoveFromGame()
        {
            this.State = new DeadState(this);
        }
    }
}
