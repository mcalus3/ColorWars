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
        private BoardField position;
        private readonly BoardField startField;
        private Direction direction;
        private Tail tail;
        public static readonly IPlayer MISSING = new MissingPlayer();
        private int movementAccumulator;
        private int speed; // interval between moves in frames

        public Player(Color color, BoardField startField, int speed)
        {
            this.color = color;
            this.position = startField;
            this.startField = startField;
            this.direction = Direction.UP;
            this.speed = speed;
            this.movementAccumulator = -2 * speed;
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
                    this.Kill();
                }
                else
                {
                    this.movementAccumulator = 0;
                    this.position = this.position.GetNeighbor(this.direction);
                }
            }
        }

        public void Kill()
        {
            this.movementAccumulator = this.speed * -30; // penalty for death - respawn after 30 moves. To be moved to config.
            this.position = this.startField;
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
