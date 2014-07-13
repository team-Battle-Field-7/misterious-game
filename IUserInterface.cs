using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace
{
    /// <summary>
    /// Basic functionality to display Battle Field 7 game.
    /// </summary>
    interface IUserInterface
    {
        /// <summary>
        /// Draws the game.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        void DrawGame(Field[,] gameField);

        /// <summary>
        /// Asks for size input.
        /// </summary>
        /// <returns></returns>
        string AskForSizeInput(string message);

        /// <summary>
        /// Asks for position input.
        /// </summary>
        /// <returns></returns>
        string AskForPositionInput(string message);

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowMessage(string message);

        /// <summary>
        /// Shows the congratulations message.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowCongratulations(string message);

        /// <summary>
        /// Shows the game over message.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowGameOver(string message);

        /// <summary>
        /// Shows a note message.
        /// </summary>
        /// <param name="message">The message.</param>
        void ShowNote(string message);

        /// <summary>
        /// Shows the bombs count.
        /// </summary>
        /// <param name="count">The count.</param>
        void ShowBombsCount(int count);

        /// <summary>
        /// Shows the turns count.
        /// </summary>
        /// <param name="count">The count.</param>
        void ShowTurnsCount(int count);
    }
}
