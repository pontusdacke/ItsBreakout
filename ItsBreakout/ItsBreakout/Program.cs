using MapBuilder;

namespace ItsBreakout
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (MapEditor map = new MapEditor())
            {
                map.Run();
            }

            using (BreakoutGame game = new BreakoutGame())
            {
                game.Run();
            }
        }
    }
#endif
}

