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

            Button playButton = new Button(game, new Vector2(50, 150), "MenuPlay.png");
            playButton.OnPress += OnPlay;
            buttons.Add(playButton);

            Button levelButton = new Button(game, new Vector2(50, 225), "MenuLevelSelect.png");
            levelButton.Enabled = false;
            levelButton.OnPress += OnLevelSelect;
            buttons.Add(levelButton);

            Button levelEditButton = new Button(game, new Vector2(50, 300), "MenuLevelEdit.png");
            levelEditButton.OnPress += OnLevelEdit;
            buttons.Add(levelEditButton);

            Button exitButton = new Button(game, new Vector2(50, 375), "MenuExit.png");
            exitButton.OnPress += OnExit;
            buttons.Add(exitButton);

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

        public override void Resume()
        {
            Game.IsMouseVisible = true;
            base.Resume();
        }

        // Private methods
        private void OnPlay()
        {
            StateEngine.PushState(new FadeInState(Game, StateEngine, new PlayState(Game, StateEngine)));
        }

        private void OnLevelEdit()
        {
            StateEngine.PushState(new MapEditorState(Game, StateEngine));
        }

        private void OnLevelSelect()
        {
            // TODO Implement level selection
            //StateEngine.PushState(new LevelSelectState(Game, StateEngine));
        }

        private void OnExit()
        {
            StateEngine.PopState();
        }
    }
}
