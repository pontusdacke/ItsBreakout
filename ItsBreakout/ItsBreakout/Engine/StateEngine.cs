using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ItsBreakout.Engine
{
    class StateEngine : DrawableGameComponent
    {
        private readonly Stack<GameState> states;
        
        public StateEngine(Game game) : base(game)
        {
            states = new Stack<GameState>();
        }

        public void ChangeState(GameState state)
        {
            if (states.Count > 0)
            {
                states.Pop();
            }

            states.Push(state);
            states.Peek().Initialize();
        }

        public void PushState(GameState state)
        {
            if (states.Count > 0)
            {
                states.Peek().Pause();
            }

            states.Push(state);
            states.Peek().Initialize();
        }

        public void PopState()
        {
            if (states.Count > 0)
            {
                states.Pop(); 
                if (states.Count > 0)
                    states.Peek().Resume();
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
