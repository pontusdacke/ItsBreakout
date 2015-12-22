using Breakout;
using Microsoft.Xna.Framework;

namespace ItsBreakout.Engine
{
    interface IEntity
    {
        void Initialize();
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        bool HasComponent<T>() where T : GameObject;
        T GetComponent<T>() where T : GameObject;
    }
}
