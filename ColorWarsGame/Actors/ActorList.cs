using ColorWars.Players;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorWars.Actors
{
    class ActorList
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
                actor.Execute(state);
            }
        }

        internal void RemoveActor(IPlayer player)
        {
            this.Actors.RemoveAll(c => c.Player.Player == player);
        }
    }
}
