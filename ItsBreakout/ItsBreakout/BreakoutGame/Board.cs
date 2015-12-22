using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ItsBreakout.Engine;

namespace ItsBreakout
{
    class Board : GameObject
    {
        int width = 100;

        // The board owns the ball.
        Ball ball;
        private bool ballFired = false;
        

        public int Width
        {
            get { return width; }
        }

        public Board(Vector2 Position, Texture2D boardTexture, Texture2D ballTexture)
        {
            ball = new Ball(Position, ballTexture);
            Texture = boardTexture;
            this.Position = Position;
            Hide = false;
        }
        public void Update(ref Map map)
        {
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

                // Check collision against the screen
                if (ball.Position.X < 0) ball.ReverseXMovement();
                if (ball.Position.X > 800) ball.ReverseXMovement();
                if (ball.Position.Y > 600) ball.ReverseYMovement();
                if (ball.Position.Y < 0) ball.ReverseYMovement();
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
            //base.Draw(gameTime);
        }
    }
}
