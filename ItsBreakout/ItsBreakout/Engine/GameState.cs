using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItsBreakout.Engine
{
    abstract class GameState : DrawableGameComponent, IGameState
    {
        public StateEngine StateEngine { get; private set; }
        public SpriteBatch SpriteBatch { get { return (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); } }
        public GameState(Game game, StateEngine stateEngine) : base(game)
        {
            StateEngine = stateEngine;
            //Game.Components.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public  override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
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
