using System;

namespace PacMan_CHABRIER_REGNARD
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            Game1 game = new Game1(); //We create our game and we run it
            game.Run();
        }
    }
#endif
}

