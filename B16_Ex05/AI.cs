using System;
using System.Collections.Generic;
using System.Text;

namespace B16_Ex05
{
    internal class AI
    {
        public static int SelectMove(ref Board i_board, Board.eSlotState i_pieceTypePlayer, Board.eSlotState i_pieceTypeOpponent)
        {
            int res = 0;
            for (int column = 0; column < i_board.r_numOfColumns; column++)
            {
                if (BoardAnalyzer.IsWinnningMove(ref i_board, column, i_pieceTypePlayer))
                {
                    return column;
                }
            }

            for (int column = 0; column < i_board.r_numOfColumns; column++)
            {
                if (BoardAnalyzer.IsWinnningMove(ref i_board, column, i_pieceTypeOpponent))
                {
                    return column;
                }
            }

            Random r = new Random();
            int randomInt = r.Next(0, i_board.r_numOfColumns);
            while (!i_board.AddPieceToColumn(randomInt, i_pieceTypePlayer))
            {
                randomInt = r.Next(0, i_board.r_numOfColumns);
            }

            res = randomInt;
            i_board.RemovePieceFromColumn(res);
            return res;
        }
    }
}
