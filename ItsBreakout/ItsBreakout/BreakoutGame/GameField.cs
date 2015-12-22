using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ItsBreakout.Engine;

namespace ItsBreakout
{
    class GameField
    {
        Texture2D ballTexture, boardTexture, blockTexture;
        Vector2 ballpos;
        Vector2 boardpos;
        Board board;
        Map map;

        public GameField()
        {
            // Todo: Load from mapdata
            ballpos = new Vector2(390, 500);
            boardpos = new Vector2(350, 550);
        }
        public void LoadContent(ContentManager Content)
        {
            ballTexture = Content.Load<Texture2D>("ball_big");
            boardTexture = Content.Load<Texture2D>("board");
            blockTexture = Content.Load<Texture2D>("block");
            board = new Board(boardpos, boardTexture, ballTexture);

            map = Map.Load(@"map1", blockTexture);
        }
        public void UpdateGameField()
        {
            // Make board follow mouse
            board.Position.X = Mouse.GetState().X;

            // Move the ball ("physics")
            board.Update(ref map);
        }
        public void DrawGameField(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (Block b in map.Blocks)
            {
                b.Draw(spriteBatch);
            }
            
            board.Draw(spriteBatch);
        }
    }
}
