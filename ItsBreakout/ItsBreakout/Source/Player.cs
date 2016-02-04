using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ItsBreakout.Engine;
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
        private bool ballFired = false;

        // TODO load from file?
        const string boardTexturePath = "board.png";
        const string ballTexturePath = "ball.png";

        public Player(Game game)
        {
            ball = new Ball(Position, Texture2D.FromStream(game.GraphicsDevice, File.OpenRead("Content\\" + ballTexturePath)));

            Texture = Texture2D.FromStream(game.GraphicsDevice, File.OpenRead("Content\\" + boardTexturePath));
            if (Texture == null) throw new FileNotFoundException();

            Hide = false;
            
        }
        public void Update(ref BlockCollection map)
        {
            // Make board follow mouse
            Position.X = Mouse.GetState().X - Width / 2;

            if (ballFired)
            {
                // Move the ball ("physics")
                ball.Update(ref map);

                // Check collision against board (the player)
                if (ball.Intersects(Rectangle))
                {
                    ball.CalculateNewDirection(Rectangle);
                    ball.ReverseYMovement();
                }
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

        public override void Draw(SpriteBatch spriteBatch)
        {
            ball.Draw(spriteBatch);
            spriteBatch.Draw(Texture, Position, Color.White);
        }
        
        public void Reset()
        {
            ballFired = false;
            Position = new Vector2(350, 550);
        }
    }
}
