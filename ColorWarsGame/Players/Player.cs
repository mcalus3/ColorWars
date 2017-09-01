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
            if(this.Removed)
            {
                return;
            }
            else if(RestoreTimer != 0)
            {
                RestoreTimer--;
            }
            else if(this.MoveTimer != this.Settings.speed)
            {
                this.MoveTimer++;
            }
            else if(this.Direction == Direction.NONE)
            {
                this.MoveTimer = 0;
            }
            else if(this.BufferedDirection == Direction.NONE)
            {
                this.BufferedDirection = this.Direction;
            }
            else if(this.Position.Neighbours[this.BufferedDirection] == null)
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
                if(this.Direction != Player.ReversedDirection(this.BufferedDirection))
                {
                    this.BufferedDirection = this.Direction;
                }
            }
        }

        private static Boards.Direction ReversedDirection(Boards.Direction direction)
        {
            switch(direction)
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

        public void SpawnTail()
        {
            this.Tail.AddField(this.Position);
        }

        public void AddTerritory()
        {
            foreach(BoardField field in this.Tail.Positions)
            {
                field.Owner = this;
            }


            List<BoardField> reversedTail = new List<BoardField>(this.Tail.Positions);

            reversedTail.Reverse();

            foreach(BoardField lastTailField in reversedTail)
            {
                if(lastTailField.Neighbours[Direction.LEFT] != null &&
                    lastTailField.Neighbours[Direction.LEFT].Owner != this &&
                    lastTailField.Neighbours[Direction.RIGHT] != null &&
                    lastTailField.Neighbours[Direction.RIGHT].Owner != this)
                {

                    if(MarkPossible(lastTailField.Neighbours[Direction.LEFT]))
                    {
                        //w lewo jest ok
                        MarkPossibleAsOwner(lastTailField.Neighbours[Direction.LEFT]);
                    }
                    else if(MarkPossible(lastTailField.Neighbours[Direction.RIGHT]))
                    {
                        //w prawo jest ok
                        MarkPossibleAsOwner(lastTailField.Neighbours[Direction.RIGHT]);
                    }
                    else
                    {
                        //nigdzie nie jest ok
                    }
                    CleanPossible(lastTailField.Neighbours[Direction.LEFT]);
                    CleanPossible(lastTailField.Neighbours[Direction.RIGHT]);
                    continue;
                }
                else if(lastTailField.Neighbours[Direction.UP] != null &&
                  lastTailField.Neighbours[Direction.UP].Owner != null &&
                  lastTailField.Neighbours[Direction.DOWN] != null &&
                  lastTailField.Neighbours[Direction.DOWN].Owner != this)
                {
                    if(MarkPossible(lastTailField.Neighbours[Direction.UP]))
                    {
                        //w lewo jest ok
                        MarkPossibleAsOwner(lastTailField.Neighbours[Direction.UP]);
                    }
                    else if(MarkPossible(lastTailField.Neighbours[Direction.DOWN]))
                    {
                        //w prawo jest ok
                        MarkPossibleAsOwner(lastTailField.Neighbours[Direction.DOWN]);
                    }
                    else
                    {
                        //nigdzie nie jest ok
                    }
                    CleanPossible(lastTailField.Neighbours[Direction.UP]);
                    CleanPossible(lastTailField.Neighbours[Direction.DOWN]);
                    continue;
                }
            }

            this.Tail.Delete();
            this.OnTerritoryAdded(this);
        }

        internal void MarkPossibleAsOwner(BoardField field)
        {

            field.Owner = this;
            field.possibleOwner = Player.MISSING;
            BoardField leftF = field.Neighbours[Direction.LEFT];
            if(leftF != null && leftF.possibleOwner == this)
            {
                MarkPossibleAsOwner(leftF);
            }

            BoardField rightF = field.Neighbours[Direction.RIGHT];
            if(rightF != null && rightF.possibleOwner == this)
            {
                MarkPossibleAsOwner(rightF);
            }

            BoardField upF = field.Neighbours[Direction.UP];
            if(upF != null && upF.possibleOwner == this)
            {
                MarkPossibleAsOwner(upF);
            }


            BoardField downF = field.Neighbours[Direction.DOWN];
            if(downF != null && downF.possibleOwner == this)
            {
                MarkPossibleAsOwner(downF);
            }


        }


        internal void CleanPossible(BoardField field)
        {

            field.possibleOwner = Player.MISSING;
            BoardField leftF = field.Neighbours[Direction.LEFT];
            if(leftF != null && leftF.possibleOwner == this)
            {
                CleanPossible(leftF);
            }

            BoardField rightF = field.Neighbours[Direction.RIGHT];
            if(rightF != null && rightF.possibleOwner == this)
            {
                CleanPossible(rightF);
            }

            BoardField upF = field.Neighbours[Direction.UP];
            if(upF != null && upF.possibleOwner == this)
            {
                CleanPossible(upF);
            }


            BoardField downF = field.Neighbours[Direction.DOWN];
            if(downF != null && downF.possibleOwner == this)
            {
                CleanPossible(downF);
            }


        }

        internal Boolean MarkPossible(BoardField field)
        {
            if(field == null)
            {
                return false;
            }

            BoardField leftF = field.Neighbours[Direction.LEFT];
            BoardField rightF = field.Neighbours[Direction.RIGHT];
            BoardField upF = field.Neighbours[Direction.UP];
            BoardField downF = field.Neighbours[Direction.DOWN];

            if(leftF == null || rightF == null || upF == null || downF == null)
            {
                return false;
            }

            field.possibleOwner = this;

            if(leftF.Owner != this && leftF.possibleOwner != this)
            {
                if(!MarkPossible(leftF))
                {
                    return false;
                }
            }


            if(rightF.Owner != this && rightF.possibleOwner != this)
            {
                if(!MarkPossible(rightF))
                {
                    return false;
                }
            }


            if(upF.Owner != this && upF.possibleOwner != this)
            {
                if(!MarkPossible(upF))
                {
                    return false;
                }
            }



            if(downF.Owner != this && downF.possibleOwner != this)
            {
                if(!MarkPossible(downF))
                {
                    return false;
                }
            }

            return true;
        }

        internal void OnTerritoryAdded(Player player)
        {
            if(this.TerritoryAddedEvent != null)
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
