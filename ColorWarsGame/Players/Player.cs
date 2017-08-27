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
        private PlayerSettings settings;
        public Tail Tail { get; set; }

        public BoardField Position { get; set; }
        public Direction Direction { get; set; }
        private int moveTimer;
        public IPlayerState State { get; set; }

        public Player(PlayerSettings settings, BoardField startField)
        {
            this.settings = settings;
            this.Position = startField;
            this.startField = startField;
            this.Direction = Direction.NONE;
            this.State = new DefensiveState(this);
            this.Tail = new Tail(this);
        }

        public Color GetColor()
        {
            return this.settings.color;
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
            if (this.moveTimer != this.settings.speed)
            {
                this.moveTimer++;
            }
            else if (this.Direction == Direction.NONE)
            {
                this.moveTimer = 0;
            }
            else if (this.Position.Neighbours[this.Direction] == null)
            {
                this.Tail.Delete();
                this.Kill(this);
                return;
            }
            else
            {
                this.State.OnMovement();
                this.Position = this.Position.Neighbours[this.Direction];
                this.moveTimer = 0;
            }
        }

        internal void SpawnTail()
        {
            this.Tail.Positions.Add(this.Position);
        }

        private void Kill(Player owner)
        {
            this.Position = this.startField;
            this.moveTimer = -1 * this.settings.deathPenalty;
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
