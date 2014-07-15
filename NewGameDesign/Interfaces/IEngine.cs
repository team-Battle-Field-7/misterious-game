using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace.NewGameDesign.Interfaces
{
    interface IEngine
    {
        /// <summary>
        /// Runs the game.
        /// </summary>
        /// <param name="ui">The UI.</param>
        /// <param name="battleField">The battle field.</param>
        void RunGame(IUserInterface ui, IBattleField battleField);
    }
}
