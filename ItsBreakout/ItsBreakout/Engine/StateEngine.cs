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
                if (states.Count > 0)
                    states.Peek().Resume(); // Resume top state
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (states.Count == 0)
                Game.Exit();
            else
                states.Peek().Update(gameTime);
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            for (int i = states.Count - 1; i >= 0; i--)
            {
                states.ElementAt(i).Draw(gameTime);
            }

            base.Draw(gameTime);
        }

        public Type GetPeekType()
        {
            return states.Peek().GetType();
        }
    }
}
