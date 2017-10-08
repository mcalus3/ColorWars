using System;
using Microsoft.Xna.Framework;

using ColorWars.Boards;
using ColorWars.Players.States;
using ColorWars.Services;
using ColorWars.Players;

namespace ColorWarsTest.Mocks
{
    class PlayerMock : Player
    {
        public new event EventHandler TerritoryAddedEvent;
        public int updated;
        public int dirChanged;
        public int killed;
        public int moved;
        public int tailSpawned;

        public PlayerMock(PlayerSettings settings, BoardField startField) : base(settings, startField)
        {
        }

        public new void Update()
        {
            this.updated++;
        }

        public override void ChangeNextDirection(Direction direction)
        {
            this.dirChanged++;
        }

        public new void Kill(Player killer)
        {
            this.killed++;
        }

        public new void Move(Direction direction)
        {
            this.moved++;
        }

        public new void SpawnTail()
        {
            this.tailSpawned++;
        }

        internal new void OnTerritoryAdded(Player player)
        {
            this.TerritoryAddedEvent?.Invoke(player, new EventArgs());
        }
    }
}
