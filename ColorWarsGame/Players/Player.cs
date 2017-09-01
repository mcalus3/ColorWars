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


            List<BoardField> reversedTail = new List<BoardField>(this.tail.positions);

            reversedTail.Reverse();

            foreach (BoardField lastTailField in reversedTail)
            {
                if(lastTailField.GetNeighbor(Direction.LEFT).owner != this &&
                    lastTailField.GetNeighbor(Direction.RIGHT).owner != this)
                {
                    
                    if(MarkPossible(lastTailField.GetNeighbor(Direction.LEFT)) )
                    {
                        //w lewo jest ok
                        MarkPossibleAsOwner(lastTailField.GetNeighbor(Direction.LEFT));
                    } else if (MarkPossible(lastTailField.GetNeighbor(Direction.RIGHT)))
                    {
                        //w prawo jest ok
                        MarkPossibleAsOwner(lastTailField.GetNeighbor(Direction.RIGHT));
                    } else
                    {
                        //nigdzie nie jest ok
                    }
                    continue;
                } else if (lastTailField.GetNeighbor(Direction.UP).owner != this &&
                    lastTailField.GetNeighbor(Direction.DOWN).owner != this)
                {
                    if (MarkPossible(lastTailField.GetNeighbor(Direction.UP)))
                    {
                        //w lewo jest ok
                        MarkPossibleAsOwner(lastTailField.GetNeighbor(Direction.UP));
                    }
                    else if (MarkPossible(lastTailField.GetNeighbor(Direction.DOWN)))
                    {
                        //w prawo jest ok
                        MarkPossibleAsOwner(lastTailField.GetNeighbor(Direction.DOWN));
                    }
                    else
                    {
                        //nigdzie nie jest ok
                    }
                    continue;
                } 
            }
            
            tail.Delete();
        }

    internal void MarkPossibleAsOwner(BoardField field) 
        {
           
            field.owner = this;
            field.possibleOwner = Player.MISSING;
            BoardField leftF = field.GetNeighbor(Direction.LEFT);
            if(leftF.possibleOwner == this)
            {
                MarkPossibleAsOwner(leftF);
            }

            BoardField rightF = field.GetNeighbor(Direction.RIGHT);
            if (rightF.possibleOwner == this)
            {
                MarkPossibleAsOwner(rightF);
            }

            BoardField upF = field.GetNeighbor(Direction.UP);
            if (upF.possibleOwner == this)
            {
                MarkPossibleAsOwner(upF);
            }


            BoardField downF = field.GetNeighbor(Direction.DOWN);
            if (downF.possibleOwner == this)
            {
                MarkPossibleAsOwner(downF);
            }
            
 
        }

        internal Boolean MarkPossible(BoardField field)
        {
            if (field == null)
            {
                return false;
            }

            field.possibleOwner = this;
            BoardField leftF = field.GetNeighbor(Direction.LEFT);
            if (leftF.owner != this && leftF.possibleOwner != this)
            {
                if (!MarkPossible(leftF))
                {
                    return false;
                }
            }

            BoardField rightF = field.GetNeighbor(Direction.RIGHT);
            if (rightF.owner != this && rightF.possibleOwner != this)
            {
                if (!MarkPossible(rightF))
                {
                    return false;
                }
            }

            BoardField upF = field.GetNeighbor(Direction.UP);
            if (upF.owner != this && upF.possibleOwner != this)
            {
                if (!MarkPossible(upF))
                {
                    return false;
                }
            }


            BoardField downF = field.GetNeighbor(Direction.DOWN);
            if (downF.owner != this && downF.possibleOwner != this)
            {
                if (!MarkPossible(downF))
                {
                    return false;
                }
            }

            return true;
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
