using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using ColorWars.Boards;

namespace ColorWars.Players
{
    class Player : IPlayer
    {
        public static readonly IPlayer MISSING = new MissingPlayer();

        private readonly BoardField startField;
        public PlayerSettings Settings { get; set; }
        public Tail Tail { get; set; }

        public BoardField Position { get; set; }
        public Direction Direction { get; set; }
        public int MoveTimer { get; set; }
        public IPlayerState State { get; set; }
        public Direction BufferedDirection { get; set; }

        public Player(PlayerSettings settings, BoardField startField)
        {
            this.Settings = settings;
            this.Position = startField;
            this.startField = startField;
            this.Direction = Direction.NONE;
            this.BufferedDirection = Direction.NONE;
            this.State = new DefensiveState(this);
            this.Tail = new Tail(this);
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
            if (this.MoveTimer != this.Settings.speed)
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
                this.BufferedDirection = this.Direction;
            }
        }

        internal void SpawnTail()
        {
            this.Tail.AddField(this.Position);
        }

        public void Kill(Player owner)
        {
            this.Tail.Delete();
            this.Position = this.startField;
            this.MoveTimer = -1 * this.Settings.deathPenalty;
        }

        public void AddTerritory()
        {
            foreach (BoardField field in this.Tail.Positions)
            {
                field.Owner = this;
            }
            this.Tail.Delete();
        }
    }
}
