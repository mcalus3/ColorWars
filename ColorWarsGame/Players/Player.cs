using System;
using Microsoft.Xna.Framework;

using ColorWars.Boards;
using ColorWars.Players.States;
using ColorWars.Services;

namespace ColorWars.Players
{
    class Player : IPlayer, ISquareDrawable
    {
        public static readonly IPlayer MISSING = new MissingPlayer();

        public event EventHandler TerritoryAddedEvent;
        public readonly BoardField startField;
        public PlayerSettings Settings { get; set; }
        public PlayerStats Stats { get; set; }
        public Tail Tail { get; set; }

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

        public void Update()
        {
            this.State.OnUpdate();
        }

        public virtual void ChangeNextDirection(Direction direction)
        {
            this.State.ChangeDirection(direction);
        }

        public void Kill(Player killer)
        {
            this.State = new WaitingForRespawnState(this);
            if(killer != this)
            {
                this.Stats.Deaths++;
                killer.Stats.Kills++;
            }
        }

        public void Move(Direction direction)
        {
            this.Position = this.Position.Neighbours[direction];
        }

        public void SpawnTail()
        {
            this.Tail.AddField(this.Position);
        }

        internal void OnTerritoryAdded(Player player)
        {
            this.TerritoryAddedEvent?.Invoke(player, new EventArgs());
        }
    }
}
