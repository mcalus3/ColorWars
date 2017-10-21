using System;
using Microsoft.Xna.Framework;

using ColorWars.Boards;
using ColorWars.Players.States;
using ColorWars.Services;

namespace ColorWars.Players
{
    class PlayerModel : IPlayer, ISquareDrawable
    {
        public event EventHandler TerritoryAddedEvent;
        public event EventHandler KilledEvent;
        public Color Color { get; set; }
        public PlayerStats Stats { get; set; }
        public Tail Tail { get; set; }

        public BoardField Position { get; set; }
        public Direction Direction { get; set; }
        public float MovementFraction { get; set; }

        public PlayerModel(Color color, BoardField startField)
        {
            this.Color = color;
            this.Position = startField;
            this.Direction = Direction.NONE;
            this.Tail = new Tail(this);
            this.Stats = new PlayerStats();
        }

        public Color GetColor()
        {
            return this.Color;
        }

        public Point[] GetPoints()
        {
            return new Point[] { this.Position.GetPoints()[0] };
        }

        public virtual void ChangeDirection(Direction direction)
        {
            this.Direction = direction;
        }

        public void Kill(PlayerModel killer)
        {
            this.onKill(killer);
            if(killer != this)
            {
                this.Stats.Deaths++;
                killer.Stats.Kills++;
            }
        }

        public void Move(Direction direction)
        {
            this.Position = this.Position.Neighbours[direction];
        }

        public void SpawnTail()
        {
            this.Tail.AddField(this.Position);
        }

        internal void OnTerritoryAdded()
        {
            this.TerritoryAddedEvent?.Invoke(this, new EventArgs());
        }

        private void onKill(PlayerModel killer)
        {
            this.KilledEvent?.Invoke(killer, new EventArgs());
        }

    }
}
