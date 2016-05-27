using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex05
{
    internal class BoardView
    {
        private const char k_P1Symbol = 'O';
        private const char k_P2Symbol = 'X';
        private const char k_EmptySymbol = ' ';
        private const string k_SlotTemplate = @"  {0}  ";
        private const char k_TableHorizontalSeperatorChar = '|';
        private const char k_TableVerticalSeperatorChar = '=';

        /// <summary>
        /// Prints a given board
        /// </summary>
        /// <param name="i_board"></param>
        public static void PrintBoard(Board i_board)
        {
            int numOfColumns = i_board.r_numOfColumns;
            int numOfRows = i_board.r_numOfRows;
            Board.eSlotState[,] slotMatrix = i_board.SlotsMatrix;

            StringBuilder separatorRow = new StringBuilder();
            int seperatorRowLength = (numOfColumns * (k_SlotTemplate.Length - 1)) + 1;
            separatorRow.Append(k_TableVerticalSeperatorChar, seperatorRowLength);

            //// Ex02.ConsoleUtils.Screen.Clear();
            PrintHeaderRow(numOfColumns);

            for (int row = 0; row < numOfRows; row++)
            {
                for (int column = 0; column < numOfColumns; column++)
                {
                    Board.eSlotState slotType = slotMatrix[column, row];

                    if (column == 0)
                    {
                        Console.Write(k_TableHorizontalSeperatorChar);
                    }

                    Printslot(slotType);
                    Console.Write(k_TableHorizontalSeperatorChar);
                }

                Console.Write(Environment.NewLine);
                Console.WriteLine(separatorRow);
            }

            // extra padding at the bottom
            Console.Write(Environment.NewLine);
        }

        /// <summary>
        /// Print the board header with number for each column
        /// </summary>
        private static void PrintHeaderRow(int i_numColumns)
        {
            Console.Write(' ');
            for (int column = 0; column < i_numColumns; column++)
            {
                Console.Write(string.Format(k_SlotTemplate, column + 1));
                Console.Write(' ');
            }

            Console.Write(Environment.NewLine);
        }

        /// <summary>
        /// Print board slot from string template
        /// </summary>
        /// <param name="i_slotType"></param>
        private static void Printslot(Board.eSlotState i_slotType)
        {
            char slotPieceView = k_EmptySymbol;
            switch (i_slotType)
            {
                case Board.eSlotState.Empty:
                    slotPieceView = k_EmptySymbol;
                    break;

                case Board.eSlotState.Player1:
                    slotPieceView = k_P1Symbol;
                    break;

                case Board.eSlotState.Player2:
                    slotPieceView = k_P2Symbol;
                    break;
            }

            Console.Write(string.Format(k_SlotTemplate, slotPieceView));
        }
    }
}
