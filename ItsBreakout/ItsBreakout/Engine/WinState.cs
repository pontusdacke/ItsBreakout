﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ItsBreakout.Source;

namespace ItsBreakout.Engine
{
    class WinState : GameState
    {
        Texture2D background;

        public WinState(Game game, StateEngine stateEngine) : base(game, stateEngine)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("WinBackground");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                while (StateEngine.GetPeekType() != typeof(MenuState))
                    StateEngine.PopState(); 
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
