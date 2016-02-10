using ItsBreakout.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace ItsBreakout.Source
{
    class Ball : GameObject
    {
        Vector2 Direction;
        Circle circle;
        float speed;
        // Debug float
        const float slowmo = 1.0f;
        public Vector2 Origin
        {
            get;
            private set;
        }

        public Ball(Vector2 Position, Texture2D Texture) : base(Position, Texture)
        {
            circle = new Circle(Texture.Width / 2, new Vector2(Position.X + Texture.Width / 2, Position.Y + Texture.Width / 2));
            speed = 6 + (1.2f * BreakoutGame.currentLevel);
            Origin = new Vector2(Texture.Width / 2, Texture.Height / 2);


        }
        public void Update(ref BlockCollection map)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                // DEBUG
                // Movement physics
                Position.X += Direction.X * slowmo;
                Position.Y += Direction.Y * slowmo;

            }
            else
            {
                // Movement physics
                Position.X += Direction.X * speed;
                Position.Y += Direction.Y * speed;

            }

            // Center coordinate
            circle.Center = new Vector2(Position.X + Texture.Width / 2, Position.Y + Texture.Width / 2);

            #region Collision detection / response
            // Check collision against blocks
            for (int i = 0; i < map.Blocks.Count; i++)
            {
                if (Intersects(map.Blocks[i].Rectangle))
                {
                    // Back the ball
                    Position.X -= Direction.X * speed;
                    Position.Y -= Direction.Y * speed;

                    // Find the angle between the ball and the block to know what movement direction to switch.
                    Vector2 v = new Vector2(
                        map.Blocks[i].Rectangle.Center.X - Rectangle.Center.X,
                        map.Blocks[i].Rectangle.Center.Y - Rectangle.Center.Y);

                    // Correct angles from square to rectangle
                    float width = 64;
                    float height = 26;
                    float whratio = width / height; // y axis change (increase)
                    float hwratio = height / width; // x axis change (decrease)
                    v.Y = v.Y * whratio;
                    v.X = v.X * hwratio;

                    // Reverse X or Y direction based on the Vector2 v.
                    if (Math.Abs(v.X) == Math.Max(Math.Abs(v.X), Math.Abs(v.Y)) && v.X != v.Y)
                    {
                        ReverseXMovement();
                    }
                    else
                    {
                        ReverseYMovement();
                    }

                    // Damage block. If function returns true block is destroyed.
                    if (map.DamageBlock(i)) i--;

                    break; // We can only touch one block per frame (per update)
                }
            }
            #endregion

            // Check collision against the screen
            if (Position.X < 0) ReverseXMovement();
            if (Position.X > 800 - Width) ReverseXMovement();
            if (Position.Y < 0) ReverseYMovement();
        }
        public bool Intersects(Rectangle rectangle)
        {
            return circle.Intersects(rectangle);
        }
        public void ReverseXMovement()
        {
            Direction = new Vector2(-Direction.X, Direction.Y);
        }
        public void ReverseYMovement()
        {
            Direction = new Vector2(Direction.X, -Direction.Y);
        }
        public void CalculateNewDirection(Rectangle collisionObject)
        {
            int collisionObjectCenter = collisionObject.X + collisionObject.Width / 2;
            float ballBoardCenterDistance = (Position.X + Rectangle.Width / 2) - collisionObjectCenter;
            Direction.X = 2 * (ballBoardCenterDistance / collisionObject.Width);
        }
        public void Fire()
        {
            Direction = new Vector2(1, -1);
        }

        // Private Methods
        float Dot(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

       
    }
}
