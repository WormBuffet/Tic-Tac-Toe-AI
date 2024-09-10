using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Lib.AI
{
    public class RBS
    {
        private readonly int MiddleX;
        private readonly int MiddleY;

        private readonly int Rows;
        private readonly int Columns;

        private readonly int PlayerVal;
        private readonly int AIVal;
        private readonly int Empty;

        private int TopLeft;
        private int BottomLeft;
        private int MiddleLeft;
        private int TopRight;
        private int BottomRight;
        private int MiddleRight;
        private int BottomMiddle;
        private int TopMiddle;

        private readonly Move Move;

        public RBS(int Rows, int Columns, int PlayerVal, int AIVal, int Empty)
        {
            this.Rows = Rows;
            this.Columns = Columns;
            this.PlayerVal = PlayerVal;
            this.AIVal = AIVal;
            this.Empty = Empty;

            MiddleX = Rows % 2;
            MiddleY = Columns % 2;
            Move = new Move(Rows, Columns);
        }

        public int[] AITurn(int[,] board)
        {
            int[] returnArr = new int[2];

            int playerWon;
            int AIWon;

            // checks to see if the AI has any opportunities to win
            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    if(board[i, j] == Empty)
                    {
                        board[i, j] = AIVal;
                        AIWon = Move.WinningMove(board);
                        board[i, j] = Empty;

                        if (AIWon == AIVal)
                        {
                            returnArr[0] = i;
                            returnArr[1] = j;
                            return returnArr;
                        }
                    }
                }
            }

            // checks to see if the Player has any opportunities to win
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (board[i, j] == Empty)
                    {
                        board[i, j] = PlayerVal;
                        playerWon = Move.WinningMove(board);
                        board[i, j] = Empty;

                        if (playerWon == PlayerVal)
                        {
                            returnArr[0] = i;
                            returnArr[1] = j;
                            return returnArr;
                        }
                    }
                }
            }

            TopLeft = board[0, 0];
            BottomLeft = board[Rows - 1, 0];
            MiddleLeft = board[MiddleX, 0];
            TopRight = board[0, Columns - 1];
            BottomRight = board[Rows - 1, Columns - 1];
            MiddleRight = board[MiddleX, Columns - 1];
            BottomMiddle = board[Rows - 1, MiddleY];
            TopMiddle = board[0, MiddleY];

            // make sure player is not trying to form a large "L" anywhere on the board
            if (TopLeft == PlayerVal && BottomRight == PlayerVal && BottomLeft == Empty)
            {
                if(BottomMiddle == Empty)
                {
                    returnArr[0] = Rows - 1;
                    returnArr[1] = 1;
                    return returnArr;
                }
            }
            else if (TopLeft == PlayerVal && BottomRight == PlayerVal && TopRight == Empty)
            {
                if (MiddleRight == Empty)
                {
                    returnArr[0] = 1;
                    returnArr[1] = Columns - 1;
                    return returnArr;
                }
            }
            else if (BottomLeft == PlayerVal && TopRight == PlayerVal && TopLeft == Empty)
            {
                if (TopMiddle == Empty)
                {
                    returnArr[0] = 0;
                    returnArr[1] = 1;
                    return returnArr;
                }
            }else if (BottomLeft == PlayerVal && TopRight == PlayerVal && BottomRight == Empty)
            {
                if (MiddleLeft == Empty)
                {
                    returnArr[0] = 1;
                    returnArr[1] = 0;
                    return returnArr;
                }
            }

            // if the center of the board is vacant, play a piece there
            if (board[MiddleX, MiddleY] == Empty)
            {
                returnArr[0] = MiddleX;
                returnArr[1] = MiddleY;
                return returnArr;
            }

            // make sure player is not trying to form a small backwards "L" in the bottom-right corner
            if (BottomMiddle == PlayerVal && MiddleRight == PlayerVal && BottomRight == Empty)
            {
                returnArr[0] = Rows - 1;
                returnArr[1] = Columns - 1;
                return returnArr;
            }

            if (board[MiddleX, MiddleY] == AIVal) 
            {
                // if both positions to the left and the right of the middle are vacant
                if(MiddleLeft == Empty && MiddleRight == Empty)
                {
                    // if the top row is filled, place your piece in the spot to the left of the middle
                    if(TopLeft != Empty && TopMiddle != Empty || TopMiddle != Empty && TopRight != Empty)
                    {
                        returnArr[0] = MiddleX;
                        returnArr[1] = 0;
                        return returnArr;
                    }
                    // if the bottom row is filled, place your piece in the spot to the right of the middle
                    else if (BottomLeft != Empty && BottomMiddle != Empty || BottomMiddle != Empty && BottomRight != Empty)
                    {
                        returnArr[0] = MiddleX;
                        returnArr[1] = Columns - 1;
                        return returnArr;
                    }
                }
                // if both positions above and below the middle are vacant
                else if (TopMiddle == Empty && BottomMiddle == Empty)
                {
                    // if the left column is filled, place your piece in the spot above the middle
                    if (TopLeft != Empty && MiddleLeft != Empty || MiddleLeft != Empty && BottomLeft != Empty)
                    {
                        returnArr[0] = 0;
                        returnArr[1] = MiddleY;
                        return returnArr;
                    }
                    // if the right column is filled, place your piece in the spot below the middle
                    else if (TopRight != Empty && MiddleRight != Empty || MiddleRight != Empty && BottomRight != Empty)
                    {
                        returnArr[0] = Rows - 1;
                        returnArr[1] = MiddleY;
                        return returnArr;
                    }
                }

                if(TopLeft != Empty && MiddleLeft != Empty && BottomLeft != Empty && MiddleRight == Empty)
                {
                    returnArr[0] = 1;
                    returnArr[1] = Columns - 1;
                    return returnArr;
                }else if(TopRight != Empty && MiddleRight != Empty && BottomRight != Empty && MiddleLeft == Empty)
                {
                    returnArr[0] = 1;
                    returnArr[1] = 0;
                    return returnArr;
                }else if(TopRight != Empty && TopMiddle != Empty && TopLeft != Empty && BottomMiddle == Empty)
                {
                    returnArr[0] = Rows - 1;
                    returnArr[1] = MiddleY;
                    return returnArr;
                }
                else if (BottomRight != Empty && BottomMiddle != Empty && BottomLeft != Empty && TopMiddle == Empty)
                {
                    returnArr[0] = 0;
                    returnArr[1] = MiddleY;
                    return returnArr;
                }
            }

            // if there is a piece in the corner adjacent, the AI should pick that spot
            if (TopLeft != TopRight && TopLeft == Empty)
            {
                returnArr[0] = 0;
                returnArr[1] = 0;
                return returnArr;
            }
            else if(TopRight != BottomRight && TopRight == Empty)
            {
                returnArr[0] = 0;
                returnArr[1] = Columns - 1;
                return returnArr;
            }
            else if(BottomRight != BottomLeft && BottomRight == Empty)
            {
                returnArr[0] = Rows - 1;
                returnArr[1] = Columns - 1;
                return returnArr;
            }
            else if(BottomLeft != TopLeft && BottomLeft == Empty)
            {
                returnArr[0] = Rows - 1;
                returnArr[1] = 0;
                return returnArr;
            }

            // if there is potential for a diagonal, try to utilise it
            if (board[MiddleX, MiddleY] == AIVal)
            {
                if (TopLeft == BottomRight && BottomRight == Empty)
                {
                    returnArr[0] = 0;
                    returnArr[1] = 0;
                    return returnArr;
                }
                else if (TopRight == BottomLeft && BottomLeft == Empty)
                {
                    returnArr[0] = 0;
                    returnArr[1] = Columns - 1;
                    return returnArr;
                }
            }

            // no other possible moves to explore
            for(int i = 0; i < Rows; i++)
            {
                for(int j = 0; j < Columns; j++)
                {
                    if(board[i, j] == Empty)
                    {
                        returnArr[0] = i;
                        returnArr[1] = j;
                        break;
                    }
                }
            }
            return returnArr;
        }

        #region testCode
        /*
         * for(int i = 0; i < Rows; i++)
                {
                    for(int j = 0; j < Columns; j++)
                    {
                        if(board[i, j] == Empty)
                        {
                            board[i, j] = PlayerVal;
                            playerWon = Move.WinningMove(board);

                            board[i, j] = AIVal;
                            AIWon = Move.WinningMove(board);

                            if (playerWon == PlayerVal || AIWon == AIVal)
                                return board;
                        }
                    }
                }
         */
        #endregion
    }
}
