using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ColorWars
{
    class Player : IPlayer
    {
        private Color color;
        internal BoardField position;
        private readonly BoardField startField;
        private Direction direction;
        public Tail tail;
        public static readonly IPlayer MISSING = new MissingPlayer();
        private int movementAccumulator;
        private int speed; // interval between moves in frames
        internal IPlayerState state;

        public Player(Color color, BoardField startField, int speed)
        {
            this.tail = new Tail(this);
            this.color = color;
            this.position = startField;
            this.startField = startField;
            this.direction = Direction.UP;
            this.speed = speed;
            this.movementAccumulator = -2 * speed;
            this.state = new DefensiveState(this);
        }

        public void ChangeDirection(Direction newDirection)
        {
            this.direction = newDirection;
        }

        public void Move()
        {
            this.movementAccumulator++;
            if (this.movementAccumulator == this.speed)
            {
                if (this.position.GetNeighbor(this.direction) == null)
                {
                    this.Kill(this);
                }
                else
                {
                    this.movementAccumulator = 0;
                    this.state.OnMovement();
                    this.position = this.position.GetNeighbor(this.direction);
                    this.position.OnPlayerEntered(this);
                }
            }
        }

        public void Kill(Player killer)
        {
            this.movementAccumulator = this.speed * -1; // penalty for death - re spawn after 30 moves. To be moved to config.
            this.position = this.startField;
            this.tail.Delete();
        }

        internal void AddTerritory()
        {
            foreach (BoardField field in this.tail.positions)
            {
                field.owner = this;
            }
            tail.Delete();
        }

    internal void SpawnTail()
        {
            this.tail.AddField(this.position);
        }

        public Color GetColor()
        {
            return this.color;
        }

        public Point[] GetPoints()
        {
            return this.position.GetPoints();
        }
    }
}
