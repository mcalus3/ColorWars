using ColorWars.Players;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ColorWars.Actors
{
    internal class ActorList
    {
        public List<KeyboardActor> Actors { get; set; }

        public ActorList()
        {
            this.Actors = new List<KeyboardActor>();
        }

        internal void ExecuteActors()
        {
            KeyboardState state = Keyboard.GetState();
            foreach(KeyboardActor actor in this.Actors)
            {
                actor.PollKeyboard(state);
            }
        }

        internal void RemoveActor(IPlayer player)
        {
            this.Actors.RemoveAll(c => c.Player.Player == player);
        }
    }
}
