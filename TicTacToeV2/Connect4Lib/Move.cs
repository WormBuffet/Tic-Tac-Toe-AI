using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Lib
{
    public class Move
    {
        public int WinningMove(int[,] boardArr, int Rows, int Columns)
        {
            // REMEMBER: top to bottom on grid is 0-2, not the other way around; if you want to go up, remember to decrement and vice versa

            // checks if player has a vertical tic tac toe
            for (int i = 0; i < Columns; i++)
            {
                if (boardArr[0, i] == boardArr[0 + 1, i] && boardArr[0 + 1, i] == boardArr[0 + 2, i])
                {
                    if (boardArr[0 + 2, i] != 0)
                    {
                        return boardArr[0 + 2, i];
                    }
                }
            }

            // checks if player has a horizontal tic tac toe
            for (int i = 0; i < Rows; i++)
            {
                if (boardArr[i, 0] == boardArr[i, 0 + 1] && boardArr[i, 0 + 1] == boardArr[i, 0 + 2])
                {
                    if(boardArr[i, 0 + 2] != 0)
                    {
                        return boardArr[i, 0 + 2];
                    }
                }
            }

            // checks if player has an acending (left to right) diagonal tic tac toe
            if (boardArr[2, 0] == boardArr[2 - 1, 0 + 1] && boardArr[2 - 1, 0 + 1] == boardArr[2 - 2, 0 + 2])
            {
                if(boardArr[2 - 2, 0 + 2] != 0)
                {
                    return boardArr[2 - 2, 0 + 2];
                }
            }

            // checks if player has a decending (right to left) diagonal tic tac toe
            if (boardArr[2, 2] == boardArr[2 - 1, 2 - 1] && boardArr[2 - 1, 2 - 1] == boardArr[2 - 2, 2 - 2])
            {
                if(boardArr[2 - 2, 2 - 2] != 0) 
                {
                    return boardArr[2 - 2, 2 - 2];
                }
            }


            int openSpots = 0;
            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    if(boardArr[i, j] == 0)
                    {
                        openSpots++;
                    }
                }
            }

            if(openSpots == 0)
            {
                return 0;
            }else
            {
                return -1;
            }
        }
    }
}
