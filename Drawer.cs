using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace
{
    /// <summary>
    /// Basic functionality to display Battle Field 7 game.
    /// </summary>
    abstract class Drawer
    {
        /// <summary>
        /// Draws the game.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        public abstract void DrawGame(Field[,] gameField);

        /// <summary>
        /// Asks for input.
        /// </summary>
        /// <returns></returns>
        public abstract string AskForInput();

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public abstract void ShowMessage(string message);

        /// <summary>
        /// Shows the congratulations message.
        /// </summary>
        /// <param name="message">The message.</param>
        public abstract void ShowCongratulations(string message);

        /// <summary>
        /// Shows the game over message.
        /// </summary>
        /// <param name="message">The message.</param>
        public abstract void ShowGameOver(string message);

        /// <summary>
        /// Shows a note message.
        /// </summary>
        /// <param name="message">The message.</param>
        public abstract void ShowNote(string message);
    }
}
