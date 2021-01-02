using ItsBreakout.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItsBreakout.Source
{
    class FadeInState : GameState
    {
        float fadeValue;
        Texture2D blackTexture;
        GameState newState;

        public FadeInState(Game game, StateEngine stateEngine, GameState newState) : base(game, stateEngine)
        {
            fadeValue = 0.0f;
            this.newState = newState;
        }
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            blackTexture = Game.Content.Load<Texture2D>("black");
            base.LoadContent();
        }
        public override void Update(GameTime gameTime)
        {
            fadeValue += 3f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (fadeValue >= 1.0f)
            {
                StateEngine.PopState();
                StateEngine.PushState(newState);
                StateEngine.PushState(new FadeOutState(Game, StateEngine));
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch.Begin();
            SpriteBatch.Draw(blackTexture, Vector2.Zero, Color.White * fadeValue);
            SpriteBatch.End();
            base.Draw(gameTime);
        }


    }
}
