using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using ItsBreakout.Engine;
using Microsoft.Xna.Framework.Graphics;

namespace ItsBreakout.Source
{
    class MenuState : GameState
    {
        List<Button> buttons;
        Texture2D background;

        public MenuState(Game game, StateEngine stateEngine) : base(game, stateEngine)
        {
            buttons = new List<Button>();

            Button playbutton = new Button(game, new Vector2(200, 200), "play.png");
            playbutton.OnPress += OnPlay;
            buttons.Add(playbutton);
        }


        // Overrided methods
        public override void Initialize()
        {
            Game.IsMouseVisible = true;
            buttons.ForEach(x => x.Initialize());
            base.Initialize();
        }

        protected override void LoadContent()
        {
            background = Game.Content.Load<Texture2D>("MenuBackground");
        }

        public override void Update(GameTime gameTime)
        {
            buttons.ForEach(x => x.Update(gameTime));
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(background, Vector2.Zero, Color.White);
            buttons.ForEach(x => x.Draw(gameTime));
            SpriteBatch.End();
        }

        // Private methods
        private void OnPlay()
        {
            StateEngine.PushState(new PlayState(Game, StateEngine));
        }
    }
}
