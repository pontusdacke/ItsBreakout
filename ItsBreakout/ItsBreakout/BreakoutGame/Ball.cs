using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Breakout
{
    class Ball : GameObject
    {
        Vector2 Direction;
        Circle circle;
        float speed;

        public Ball(Vector2 Position, Texture2D Texture) : base(Position, Texture)
        {
            circle = new Circle(Texture.Width / 2, new Vector2(Position.X + Texture.Width / 2, Position.Y + Texture.Width / 2));
            speed = 6;
        }
        public void Update(ref Map map)
        {
            Position.X += Direction.X * speed; // Todo: speed
            Position.Y += Direction.Y * speed;

            circle.Center = new Vector2(Position.X + Texture.Width / 2, Position.Y + Texture.Width / 2);

            // Check collision against blocks
            for (int i = 0; i < map.Blocks.Count; i++)
            {
                if (!map.Blocks[i].Hide && Intersects(map.Blocks[i].Rectangle))
                {
                    // Find the angle between the ball and the block to know what movement direction to switch.
                    Vector2 v = new Vector2(map.Blocks[i].Rectangle.X - Position.X, map.Blocks[i].Rectangle.Y - Position.Y);

                    if (Math.Abs(v.X) == Math.Max(Math.Abs(v.X), Math.Abs(v.Y)))
                    {
                        ReverseXMovement();
                    }
                    else
                    {
                        ReverseYMovement();
                    }

                    map.Blocks.RemoveAt(i);
                    i--; // We removed one. Let the loop know.
                    break; // We can only touch one block per loop (Bullet-through-paper fix)
                }
            }
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
            Direction = new Vector2(0, 1);
        }
    }
}
