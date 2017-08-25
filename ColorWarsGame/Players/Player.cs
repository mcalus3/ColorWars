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
        static Random rnd = new Random();

        private Color color;
        internal BoardField position;
        private readonly BoardField startField;
        private Direction direction;
        public Tail tail;
        public static readonly IPlayer MISSING = new MissingPlayer();
        private int movementAccumulator;
        private int speed; // interval between moves in frames
        internal IPlayerState state;
        private GameBoard gameBoard = null;

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

        public void setGameBoard(GameBoard gameBoard)
        {
            this.gameBoard = gameBoard;
        }

        public void Kill(Player killer)
        {
            this.movementAccumulator = this.speed * -1; // penalty for death - re spawn after 30 moves. To be moved to config.
            this.position = this.startField;
            this.tail.Delete();
        }

        internal bool isMarked(BoardField field, List<BoardField> tail, HashSet<BoardField> markedFields) {
            return field.owner.Equals(this) || tail.Contains(field) || (markedFields != null && markedFields.Contains(field)); 
        } 

        internal HashSet<BoardField> rozlejSie(BoardField startowe, HashSet<BoardField> markedFields)
        {
            HashSet<BoardField> przejrzane = new HashSet<BoardField>();
            Queue<BoardField> listaDoPrzejrzenia = new Queue<BoardField>();

            przejrzane.Add(startowe);
            listaDoPrzejrzenia.Enqueue(startowe);

            while (listaDoPrzejrzenia.Count > 0)
            {
                BoardField przeglądany = listaDoPrzejrzenia.Dequeue();
                przejrzane.Add(przeglądany);
                foreach (KeyValuePair<Direction, BoardField> para in przeglądany.neighbors)
                {
                    BoardField sąsiad = para.Value;
                    if (sąsiad == null) {
                        continue;
                    }
                    if ((przejrzane.Contains(sąsiad)) || (this.isMarked(sąsiad, this.tail.positions, markedFields))) {
                        continue;
                    }
                    listaDoPrzejrzenia.Enqueue(sąsiad);
                    przejrzane.Add(sąsiad);
                }
            }

            return przejrzane;
        }

        internal void AddTerritory()
        {       
            HashSet<BoardField> naLewo = new HashSet<BoardField>();
            HashSet<BoardField> naPrawo = new HashSet<BoardField>();
            String gdzieSieRozlewamy = "naLewo";     
            foreach (BoardField field in this.gameBoard.GetFields())
            {
                if (this.isMarked(field, this.tail.positions, naLewo)) {
                    continue;
                }
                if (gdzieSieRozlewamy.Equals("naLewo"))
                {
                    naLewo = this.rozlejSie(field, null);
                    gdzieSieRozlewamy = "naPrawo";
                } else
                {
                    naPrawo = this.rozlejSie(field, naLewo);
                    break;
                }
            }

            int losowa = rnd.Next(0, naLewo.Count() + naPrawo.Count());
            HashSet<BoardField> referencja = (losowa > naLewo.Count() ? naLewo : naPrawo);

            foreach (BoardField field in this.tail.positions.Concat(referencja))
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
