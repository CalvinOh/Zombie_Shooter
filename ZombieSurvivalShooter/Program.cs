using System;

namespace ZombieSurvivalShooter
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// </summary>
    public static class Program
    {
        public static bool restart;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            restart = false;
            do
            {
                restart = false;
                using (var game = new Game1())
                    game.Run();
            } while (restart == true);
        }
    }
#endif
}
