using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace
{
    class ConsoleDrawer : Drawer
    {
        /// <summary>
        /// Draws the game in the console.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        /// <exception cref="System.NotImplementedException">ConsoleDrawer.DrawGame() is not implemented</exception>
        public override void DrawGame(Field[,] gameField)
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

        public override string AskForInput()
        {
            return Console.ReadLine();
        }

        public override void ShowMessage(string message)
        {
            Console.Write("\n" + message);
        }

        public override void ShowCongratulations(string message)
        {
            ShowMessage(message);
        }

        public override void ShowGameOver(string message)
        {
            ShowMessage(message);
        }

        public override void ShowNote(string message)
        {
            ShowMessage(message);
        }
    }
}
