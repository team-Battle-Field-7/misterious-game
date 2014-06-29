using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace
{
    class ConsoleDrawer : IDrawer
    {
        /// <summary>
        /// Draws the game in the console.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        /// <exception cref="System.NotImplementedException">ConsoleDrawer.DrawGame() is not implemented</exception>
        public void DrawGame(Field[,] gameField)
        {
            Console.Clear();

            string line = new String('-', 8 + (gameField.GetLength(1) * 4));
            Console.Write("r\\c  || ");
            for (int col = 0; col < gameField.GetLength(1); col++)
            {
                Console.Write("{0,-2}| ", col);
            }
            Console.WriteLine("\n" + "\n" + line + "\n" + line + "\n");
            for (int row = 0; row < gameField.GetLength(0); row++)
            {
                Console.Write("{0,-2}   || ", row);
                for (int col = 0; col < gameField.GetLength(1); col++)
                {
                    char symbolToShow = '-';
                    if (gameField[row, col].ExplosivePower > 0)
                    {
                        symbolToShow = gameField[row, col].ExplosivePower.ToString()[0];
                    } 
                    if (gameField[row, col].Condition == Condition.BlownUp)
                    {
                        symbolToShow = 'X';
                    }

                    Console.Write("{0,-2}| ", symbolToShow);
                }
                Console.WriteLine();
                Console.WriteLine(line);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Asks for size input.
        /// </summary>
        /// <returns></returns>
        public string AskForSizeInput()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Asks for position input.
        /// </summary>
        /// <returns></returns>
        public string AskForPositionInput()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Shows the message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowMessage(string message)
        {
            Console.Write("\n" + message);
        }

        /// <summary>
        /// Shows the congratulations message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowCongratulations(string message)
        {
            ShowMessage(message);
        }

        /// <summary>
        /// Shows the game over message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowGameOver(string message)
        {
            ShowMessage(message);
        }

        /// <summary>
        /// Shows a note message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowNote(string message)
        {
            ShowMessage(message);
        }

        /// <summary>
        /// Shows a request for input message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void ShowAskInput(string message)
        {
            ShowMessage(message);
        }

        /// <summary>
        /// Shows the bombs count.
        /// </summary>
        /// <param name="count">The count.</param>
        public void ShowBombsCount(int count) 
        { 
            // Do Nothing
        }

        /// <summary>
        /// Shows the turns count.
        /// </summary>
        /// <param name="count">The count.</param>
        public void ShowTurnsCount(int count)
        {
            // Do Nothing
        }
    }
}
