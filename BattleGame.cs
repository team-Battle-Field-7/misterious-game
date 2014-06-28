using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BattleField7Namespace
{
    /// <summary>
    /// Това е по - лесен за четене вариант на основния клас. Единствените промени до момента са в именуването и подреждането,
    /// има изведени и няколко помощни метода от повтарящия се код. Оставил съм закоментирани старите съдържания на най - 
    /// безобразно заплетените методи за сравнение и в случай, че абстракцията ми не е сработила добре.
    /// Не съм променял нищо по логиката, макар на места да може да има нужда (NumberInRange() методът със сигурност).
    /// Ако ще пренаписваме всичко отначало, поне ще може да сравняваме функционалността с този вариант.
    /// Оставих името Bombi, за да не заемам някое по - подходящо, което да дадем на нов проект. //vnci
    /// </summary>
    class BattleGame
    {
        int minesDeployed = 0;
        string[,] field;
        int minesExploded = 0;
        int minesCleared = 0;
        int fieldSize;

        public void PlantMines()
        {
            int i;
            int j;
            while (minesDeployed + 1 <= 0.3 * fieldSize * fieldSize)
            {
                i = NumberInRange(0, fieldSize - 1);
                j = NumberInRange(0, fieldSize - 1);

                if (field[i, j] == "-")
                {
                    field[i, j] = Convert.ToString(NumberInRange(1, 5));
                    minesDeployed++;

                    if (minesDeployed >= 0.15 * fieldSize * fieldSize)
                    {
                        int flag = NumberInRange(0, 1);
                        if (flag == 1)
                        {
                            break;
                        }
                    }
                }
            }
        }

        public void Start()
        {
            fieldSize = ReadFieldSize();

            field = new string[fieldSize, fieldSize];
            for (int i = 0; i <= fieldSize - 1; i++)
            {
                for (int j = 0; j <= fieldSize - 1; j++)
                {
                    field[i, j] = "-";
                }
            }

            PlantMines();
            DrawField();

            while (!(AllMinesCleared()))
            {
                Console.Write("Please Enter Coordinates : ");

                string inputRowAndColumn = Console.ReadLine();
                string[] rowAndColumnSplit = inputRowAndColumn.Split(' ');
                int row;
                int column;

                if ((rowAndColumnSplit.Length) <= 0)
                {
                    row = -1;
                    column = -1;
                }
                else
                {
                    if (!(int.TryParse(rowAndColumnSplit[0], out row)))
                        row = -1;
                    if (!(int.TryParse(rowAndColumnSplit[1], out column)))
                        column = -1;
                }

                if (!ValidCoordinates(row, column))
                {
                    Console.WriteLine("This Move Is Out Of Area.");
                }
                else
                {
                    DetonateMine(row, column);
                }
            }

            Console.WriteLine("Game Over.Detonated Mines {0}", minesExploded);
        }

        public void DetonateMine(int row, int column)
        {
            int cellNumber;

            if ((field[row, column] == "X") || ((field[row, column]) == "-"))
                cellNumber = 0;
            else
                cellNumber = Convert.ToInt32(field[row, column]);
            switch (cellNumber)
            {
                case 1:
                    {
                        BombOne(row, column);
                        DrawField();
                        minesExploded++;
                        break;
                    }
                case 2:
                    {
                        BombTwo(row, column);
                        DrawField();
                        minesExploded++;
                        break;
                    }
                case 3:
                    {
                        BombThree(row, column);
                        DrawField();
                        minesExploded++;
                        break;
                    }
                case 4:
                    {
                        BombFour(row, column);
                        DrawField();
                        minesExploded++;
                        break;
                    }
                case 5:
                    {
                        BombFive(row, column);
                        DrawField();
                        minesExploded++;
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Invalid Move!");
                        Console.WriteLine();
                        break;
                    }
            }
        }

        /// <summary>
        /// Random number generator
        /// </summary>
        /// <param name="min">lower bound</param>
        /// <param name="max">upper bound</param>
        /// <returns>An Integer in the range between <paramref name="min"/> and <paramref name="max"/></returns>
        /// <remarks>Needs rebuild to generate new numbers, otherwise the field prodiced is always the
        /// same for the given size.</remarks>
        public static int NumberInRange(int min, int max)
        {
            Random random = new Random();
            return Convert.ToInt32(random.Next(min, max));
        }

        public void BombOne(int row, int column)
        {
            field[row, column] = "X";
            minesCleared++;
            DiagonalSplash(row, column, 1);
        }

        public void BombTwo(int row, int column)
        {
            field[row, column] = "X";
            minesCleared++;
            DiagonalSplash(row, column, 1);
            StraightSplash(row, column, 1);
        }

        public void BombThree(int row, int column)
        {
            field[row, column] = "X";
            minesCleared++;
            DiagonalSplash(row, column, 1);
            StraightSplash(row, column, 2);
            #region previous body
            //BombTwo(row, column);

            //if (row - 2 >= 0)
            //{
            //    if ((field[row - 2, column] != "X") && (field[row - 2, column] != "-"))
            //        minesCleared++;
            //    field[row - 2, column] = "X";
            //}

            //if (column - 2 >= 0)
            //{
            //    if ((field[row, column - 2] != "X") && (field[row, column - 2] != "-"))
            //        minesCleared++;
            //    field[row, column - 2] = "X";
            //}

            //if (column + 2 <= fieldSize - 1)
            //{
            //    if ((field[row, column + 2] != "X") && (field[row, column + 2] != "-"))
            //        minesCleared++;
            //    field[row, column + 2] = "X";
            //}

            //if (row + 2 <= fieldSize - 1)
            //{
            //    if ((field[row + 2, column] != "X") && (field[row + 2, column] != "-"))
            //        minesCleared++;
            //    field[row + 2, column] = "X";
            //}
            #endregion
        }

        public void BombFour(int row, int column)
        {
            field[row, column] = "X";
            minesCleared++;
            DiagonalSplash(row, column, 1);
            StraightSplash(row, column, 2);

            for (int i = -1; i <= 1; i += 2)
            {
                for (int j = -1; j <= 1; j += 2)
                {
                    StraightSplash(row + i, column + j, 1);
                }
            }
            #region previous body
            //BombThree(row, column);


            //if ((row - 1 >= 0) && (column - 2 >= 0))
            //{
            //    if ((field[row - 1, column - 2] != "X") && (field[row - 1, column - 2] != "-"))
            //        minesCleared++;
            //    field[row - 1, column - 2] = "X";
            //}
            //;

            //if ((row + 1 <= fieldSize - 1) && (column - 2 >= 0))
            //{
            //    if ((field[row + 1, column - 2] != "X") && (field[row + 1, column - 2] != "-"))
            //        minesCleared++;
            //    field[row + 1, column - 2] = "X";
            //}
            //;

            //if ((row - 2 >= 0) && (column - 1 >= 0))
            //{
            //    if ((field[row - 2, column - 1] != "X") && (field[row - 2, column - 1] != "-"))
            //        minesCleared++;
            //    field[row - 2, column - 1] = "X";
            //}
            //;

            //if ((row + 2 <= fieldSize - 1) && (column - 1 >= 0))
            //{
            //    if ((field[row + 2, column - 1] != "X") && (field[row + 2, column - 1] != "-"))
            //        minesCleared++;
            //    field[row + 2, column - 1] = "X";
            //}
            //;
            ////

            //if ((row - 1 >= 0) && (column + 2 <= fieldSize - 1))
            //{
            //    if ((field[row - 1, column + 2] != "X") && (field[row - 1, column + 2] != "-"))
            //        minesCleared++;
            //    field[row - 1, column + 2] = "X";
            //}
            //;

            //if ((row + 1 <= fieldSize - 1) && (column + 2 <= fieldSize - 1))
            //{
            //    if ((field[row + 1, column + 2] != "X") && (field[row + 1, column + 2] != "-"))
            //        minesCleared++;
            //    field[row + 1, column + 2] = "X";
            //}
            //;

            //if ((row - 2 >= 0) && (column + 1 <= fieldSize - 1))
            //{
            //    if ((field[row - 2, column + 1] != "X") && (field[row - 2, column + 1] != "-"))
            //        minesCleared++;
            //    field[row - 2, column + 1] = "X";
            //}
            //;

            //if ((row + 2 <= fieldSize - 1) && (column + 1 <= fieldSize - 1))
            //{
            //    if ((field[row + 2, column + 1] != "X") && (field[row + 2, column + 1] != "-"))
            //        minesCleared++;
            //    field[row + 2, column + 1] = "X";
            //}
            //;
            #endregion
        }

        public void BombFive(int row, int column)
        {
            field[row, column] = "X";
            minesCleared++;
            DiagonalSplash(row, column, 1);
            StraightSplash(row, column, 3);

            for (int i = -1; i <= 1; i += 2)
            {
                for (int j = -1; j <= 1; j += 2)
                {
                    StraightSplash(row + i, column + j, 1);
                }
            }
            #region previous body
            //BombFour(row, column);

            //if ((row - 2 >= 0) && (column - 2 >= 0))
            //{
            //    if ((field[row - 2, column - 2] != "X") && (field[row - 2, column - 2] != "-"))
            //        minesCleared++;
            //    field[row - 2, column - 2] = "X";
            //}
            //;

            //if ((row + 2 <= fieldSize - 1) && (column - 2 >= 0))
            //{
            //    if ((field[row + 2, column - 2] != "X") && (field[row + 2, column - 2] != "-"))
            //        minesCleared++;
            //    field[row + 2, column - 2] = "X";
            //}
            //;

            //if ((row - 2 >= 0) && (column + 2 <= fieldSize - 1))
            //{
            //    if ((field[row - 2, column + 2] != "X") && (field[row - 2, column + 2] != "-"))
            //        minesCleared++;
            //    field[row - 2, column + 2] = "X";
            //}
            //;

            //if ((row + 2 <= fieldSize - 1) && (column + 2 <= fieldSize - 1))
            //{
            //    if ((field[row + 2, column + 2] != "X") && (field[row + 2, column + 2] != "-"))
            //        minesCleared++;
            //    field[row + 2, column + 2] = "X";
            //}
            //;
            #endregion
        }



        public bool AllMinesCleared()
        {
            bool result = minesCleared == minesDeployed;
            return result;
        }

        public int ReadFieldSize()
        {
            string readThings;
            int readNumber;
            do
            {
                Console.Write("Please Enter Valid Size Of The Field.n=");
                readThings = Console.ReadLine();

                if (!(Int32.TryParse(readThings, out readNumber)))
                {
                    readNumber = -1;
                }
            }
            while (!(1 < readNumber && readNumber < 10));

            return readNumber;
        }

        public void DrawField()
        {
            Console.Write("   ");
            for (int k = 0; k <= fieldSize - 1; k++)
            {
                Console.Write(k + " ");
            }
            Console.WriteLine();
            Console.Write("   ");
            for (int k = 0; k <= fieldSize - 1; k++)
            {
                Console.Write("--");
            }
            Console.WriteLine();

            for (int i = 0; i <= fieldSize - 1; i++)
            {
                Console.Write(i + "| ");
                for (int j = 0; j <= fieldSize - 1; j++)
                {
                    Console.Write(field[i, j] + " ");
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Represents an explosion that spreads straight in all four directions,
        /// clearing bombs and empty cells in its path
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="range"></param>
        private void StraightSplash(int row, int column, int range)
        {
            for (int i = 1; i <= range; i++)
            {
                if (row - i >= 0)
                {
                    if (IsBomb(row - i, column))
                        minesCleared++;
                    field[row - i, column] = "X";
                }

                if (column - i >= 0)
                {
                    if (IsBomb(row, column - i))
                        minesCleared++;
                    field[row, column - i] = "X";
                }

                if (column + i <= fieldSize - 1)
                {
                    if (IsBomb(row, column + i))
                        minesCleared++;
                    field[row, column + i] = "X";
                }

                if (row + i <= fieldSize - 1)
                {
                    if (IsBomb(row + i, column))
                        minesCleared++;
                    field[row + i, column] = "X";
                }
            }
        }

        /// <summary>
        /// Represents an explosion that spreads diagonally in all four directions,
        /// clearing bombs and empty cells in its path
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="range"></param>
        private void DiagonalSplash(int row, int column, int range)
        {
            for (int i = 1; i <= range; i++)
            {
                if ((row - i >= 0) && (column - i >= 0))
                {
                    if (IsBomb(row - i, column - i))
                    {
                        minesCleared++;
                    }
                    field[row - i, column - i] = "X";
                }

                if ((row + i <= fieldSize - 1) && (column - i >= 0))
                {
                    if (IsBomb(row + i, column - i))
                        minesCleared++;
                    field[row + i, column - i] = "X";
                }

                if ((row - i >= 0) && (column + i <= fieldSize - 1))
                {
                    if (IsBomb(row - i, column + i))
                        minesCleared++;
                    field[row - i, column + i] = "X";
                }

                if ((row + i <= fieldSize - 1) && (column + i <= fieldSize - 1))
                {
                    if (IsBomb(row + i, column + i))
                        minesCleared++;
                    field[row + i, column + i] = "X";
                }
            }
        }

        /// <summary>
        /// Checks if the coordinates given are within the field
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        private bool ValidCoordinates(int row, int column)
        {
            if ((row >= 0) && (row <= fieldSize - 1) && (column >= 0) && (column <= fieldSize - 1))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines if there is a bomb at given coordinates
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>True if the cell has not been cleared already and is not marked as empty in the beginning</returns>
        private bool IsBomb(int row, int column)
        {
            bool cleared = field[row, column] == "X";
            bool bombFree = field[row, column] == "-";
            return !cleared && !bombFree;
        }
    }
}
