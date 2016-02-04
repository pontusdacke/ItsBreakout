using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItsBreakout.Engine
{
    class StateEngine : DrawableGameComponent
    {
        Stack<GameState> states;

        bool isRunning;

        public bool IsRunning
        {
            get { return isRunning; }
        }
        public StateEngine(Game game) : base(game)
        {
            states = new Stack<GameState>();
        }
        public override void Initialize()
        {
        }
        public void ChangeState(GameState state)
        {
            if (states.Count > 0)
            {
                states.Pop();
            }

            // Store and initialize new state.
            states.Push(state);
            states.Peek().Initialize();
        }
        public void PushState(GameState state)
        {
            if (states.Count > 0)
            {
                states.Peek().Pause();
            }

            // Store and initialize new state.
            states.Push(state);
            states.Peek().Initialize();
        }
        public void PopState()
        {
            if (states.Count > 0)
            {
                states.Pop(); // Pop current state.
                states.Peek().Resume(); // Resume top state
            }
        }

        public override void Update(GameTime gameTime)
        {
            states.Peek().Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            states.Peek().Draw(gameTime);   
            base.Draw(gameTime);
        }
    }
}
