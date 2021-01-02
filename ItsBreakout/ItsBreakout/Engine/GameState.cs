using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItsBreakout.Engine
{
    abstract class GameState : DrawableGameComponent, IGameState
    {
        public StateEngine StateEngine { get; private set; }
        public SpriteBatch SpriteBatch { get { return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); } }

        protected GameState(Game game, StateEngine stateEngine) 
            : base(game)
        {
            StateEngine = stateEngine;
            //Game.Components.Add(this);
        }

        public void Pause()
        {
            //Game.Components.Remove(this);
        }

        public virtual void Resume()
        {
            //Game.Components.Add(this);
        }
    }
}
