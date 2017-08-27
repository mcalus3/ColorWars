﻿using System;
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
        public Tail Tail{get; set;}

        private BoardField position;
        public Direction Direction { get; set; }
        private int moveTimer;

        public Player(PlayerSettings settings, BoardField startField)
        {
            this.settings = settings;
            this.position = startField;
            this.startField = startField;
            this.Direction = Direction.NONE;
            this.Tail = new Tail(this);
        }

        public Color GetColor()
        {
            return this.settings.color;
        }

        public Point[] GetPoints()
        {
            return new Point[] { this.position.GetPoints()[0] };
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
            else if (this.position.Neighbours[this.Direction] == null)
            {
                this.Tail.Delete();
                this.Kill(this);
                return;
            }
            else
            {
                this.Tail.Positions.Add(this.position);
                this.position = this.position.Neighbours[this.Direction];
                this.moveTimer = 0;
            }
        }

        private void Kill(Player owner)
        {
            this.position = this.startField;
            this.moveTimer = -1 * this.settings.deathPenalty;
        }
    }
}
