using ItsBreakout.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace ItsBreakout.Source
{
    class Player : GameObject
    {
        /* TODO Public because we need to access position to check if it collides with bottom of screen
         * Solve by breaking out physics and physics events more. (Delegate/Events)
         * Somehow ball collision must be able to trigger a GameState(LevelLostState) creation.
         */   
        public Ball ball;
        bool ballFired = false;
        Trail trail;

        // TODO load from file?
        const string boardTexturePath = "board.png";
        const string ballTexturePath = "ball.png";
        public Player(Game game)
        {
            ball = new Ball(Position, Texture2D.FromStream(game.GraphicsDevice, File.OpenRead("Content\\" + ballTexturePath)));
            trail = new Trail(game, Color.Black);

            Texture = Texture2D.FromStream(game.GraphicsDevice, File.OpenRead("Content\\" + boardTexturePath));

            Hide = false;

        }
        public void Update(ref BlockCollection map)
        {
            // Make board follow mouse
            FollowMouse();

            if (ballFired)
            {
                ball.Update(ref map);

                CheckCollission();

                trail.AddPosition(ball.Position + ball.Origin);
            }
            else
            {
                ball.Position = new Vector2(
                    Position.X + Texture.Width / 2 - ball.Texture.Width / 2,
                    Position.Y - ball.Texture.Height - 1);

                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    ballFired = true;
                    ball.Fire();
                }
            }

        }

        private void CheckCollission()
        {
            if (ball.Intersects(Rectangle))
            {
                ball.CalculateNewDirection(Rectangle);
                ball.ReverseYMovement();
            }
        }

        private void FollowMouse()
        {
            Position.X = Mouse.GetState().X - Width / 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            ball.Draw(spriteBatch);
            trail.Draw(spriteBatch);
            spriteBatch.Draw(Texture, Position, Color.White);
        }
        
        public void Reset()
        {
            trail.Clear();
            ballFired = false;
            Position = new Vector2(350, 550);
        }

    }
}
