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
        private Direction direction;
        private Tail tail;
        public static IPlayer EMPTY = new EmptyPlayer();

        public Player(Color color, BoardField field)
        {
            this.color = color;
            this.position = field;
            this.direction = Direction.UP;
        }

        public void ChangeDirection(Direction newDirection)
        {
            this.direction = newDirection;
        }

        public void Move()
        {
            this.position = this.position.GetNeighbor(this.direction);
        }

        public void Kill()
        {
            throw new NotImplementedException();
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
