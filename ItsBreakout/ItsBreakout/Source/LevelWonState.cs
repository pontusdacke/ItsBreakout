using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ItsBreakout.Engine;

namespace ItsBreakout.Source
{

    class LevelWonState : GameState
    {
        Texture2D background;
        public LevelWonState(Game game, StateEngine stateEngine) : base(game, stateEngine)
        {
        }

        public override void Initialize()
        {
            base.Initialize(); // Calls LoadContent().
        }

        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("LevelCompleted");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                StateEngine.PopState();
                return;
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
