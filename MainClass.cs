using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace
{
    static class MainClass
    {
        /// <summary>
        /// Simply calls the engine and runs it.
        /// </summary>
        static void Main()
        {
            BattleFieldGame game = new BattleFieldGame();
            game.Logger = new Logger();

            try
            {
                //game.RunOldVersion();

                //game.RunNewConsoleVersion();
                //game.RunNewWinFormsVersion();

                //game.RunTheNewDesignVersionInTheConsole();
                game.RunTheNewDesignVersionInWinForms();
            }
            catch (Exception ex)
            {
                if (game.Logger != null)
                {
                    game.Logger.LogError(ex.ToString());
                }
                throw;
            }
        }
    }
}
