using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using ColorWars.Boards;
using ColorWars.Players.States;
using ColorWars.Graphics;

namespace ColorWars.Players
{
    class Player : IPlayer, ISquareDrawable
    {
        public static readonly IPlayer MISSING = new MissingPlayer();

        private readonly BoardField startField;
        public PlayerSettings Settings { get; set; }
        public Tail Tail { get; set; }
        public PlayerStats Stats { get; set; }

        public BoardField Position { get; set; }
        public Direction Direction { get; set; }
        public int MoveTimer { get; set; }
        public IPlayerState State { get; set; }
        public event EventHandler TerritoryAddedEvent;
        public Direction BufferedDirection { get; set; }
        public int RestoreTimer { get; set; }
        private bool Removed;

        public Player(PlayerSettings settings, BoardField startField)
        {
            this.Settings = settings;
            this.Position = startField;
            this.startField = startField;
            this.Direction = Direction.NONE;
            this.BufferedDirection = Direction.NONE;
            this.State = new DefensiveState(this);
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

        public void ChangeDirection(Direction direction)
        {
            this.Direction = direction;
        }

        public void Move()
        {
            if (this.Removed)
            {
                return;
            }
            else if (RestoreTimer != 0)
            {
                RestoreTimer--;
            }
            else if (this.MoveTimer != this.Settings.speed)
            {
                this.MoveTimer++;
            }
            else if (this.Direction == Direction.NONE)
            {
                this.MoveTimer = 0;
            }
            else if (this.BufferedDirection == Direction.NONE)
            {
                this.BufferedDirection = this.Direction;
            }
            else if (this.Position.Neighbours[this.BufferedDirection] == null)
            {
                this.Kill(this);
                return;
            }
            else
            {
                this.MoveTimer = 0;
                this.State.OnMovement();
                this.Position = this.Position.Neighbours[this.BufferedDirection];
                this.Position.OnPlayerEntered(this);
                if (this.Direction != Player.ReversedDirection(this.BufferedDirection))
                {
                    this.BufferedDirection = this.Direction;
                }
            }
        }

        private static Boards.Direction ReversedDirection(Boards.Direction direction)
        {
            switch (direction)
            {
                case Direction.UP:
                    return Direction.DOWN;
                case Direction.DOWN:
                    return Direction.UP;
                case Direction.LEFT:
                    return Direction.RIGHT;
                case Direction.RIGHT:
                    return Direction.LEFT;
                default:
                    return Direction.NONE;
            }
        }

        internal void SpawnTail()
        {
            this.Tail.AddField(this.Position);
        }

        public void AddTerritory()
        {
            foreach (BoardField field in this.Tail.Positions)
            {
                field.Owner = this;
            }
            this.Tail.Delete();

            this.OnTerritoryAdded(this);
        }

        internal void OnTerritoryAdded(Player player)
        {
            if (this.TerritoryAddedEvent != null)
                this.TerritoryAddedEvent(player, new EventArgs());
        }

        public void Kill(Player killer)
        {
            this.Stats.Deaths += 1;
            killer.Stats.Kills += 1;

            this.Tail.Delete();
            this.Position = this.startField;
            this.RestoreTimer = this.Settings.deathPenalty;
            this.MoveTimer = 0;
        }

        internal void RemoveFromGame()
        {
            this.Position = new BoardField(Player.MISSING, new Point(-1, -1));
            this.Removed = true;
        }
    }
}
