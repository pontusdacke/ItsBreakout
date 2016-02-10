using ItsBreakout.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItsBreakout.Source
{
    class LevelLostState : GameState
    {
        Texture2D background;

        public LevelLostState(Game game, StateEngine stateEngine) : base(game, stateEngine)
        {
        }

        public override void Initialize()
        {
            base.Initialize(); // Calls LoadContent().
        }

        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("LevelFailed");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                StateEngine.PopState(); // Pop this state
                BreakoutGame.Lives--;

                if (BreakoutGame.Lives == 0)
                {
                    BreakoutGame.currentLevel = 1;
                    BreakoutGame.Lives = 3;
                    while (StateEngine.GetPeekType() != typeof(MenuState))
                        StateEngine.PopState();
                }
            }
            
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(background, Vector2.Zero, Color.White);
            SpriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
