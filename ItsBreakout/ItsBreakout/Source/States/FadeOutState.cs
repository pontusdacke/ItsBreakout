using ItsBreakout.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ItsBreakout.Source
{
    class FadeOutState : GameState
    {
        Texture2D blackTexture;
        float fadeValue;

        public FadeOutState(Game game, StateEngine stateEngine) : base(game, stateEngine)
        {
            fadeValue = 1.0f;
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
            fadeValue -= 3f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (fadeValue <= 0.0f)
            {
                // Pop this state.
                StateEngine.PopState(); 
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