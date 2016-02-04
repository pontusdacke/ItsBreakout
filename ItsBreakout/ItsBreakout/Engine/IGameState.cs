using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItsBreakout.Engine
{
    interface IGameState
    {
        void Pause();
        void Resume();
    }
}
