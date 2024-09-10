using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connect4Lib.AI
{
    public class MiniMax
    {
        private readonly int MaxVal = int.MaxValue;
        private readonly int MinVal = int.MinValue;

        private readonly int Rows;
        private readonly int Columns;

        private readonly bool IsMaximizing;

        private readonly int PlayerVal;
        private readonly int AIVal;
        private readonly int Empty;

        private int count;

        public MiniMax(int Rows, int Columns, bool IsMaximizing, int PlayerVal, int AIVal, int Empty)
        {
            this.Rows = Rows;
            this.Columns = Columns;
            this.IsMaximizing = IsMaximizing;

            this.PlayerVal = PlayerVal;
            this.AIVal = AIVal;
            this.Empty = Empty;
        }

        public int[] AITurn(int[,] boardArr, int Depth)
        {
            int bestScore = MinVal;
            int[] bestMove = new int[2];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    // only attempt to minimax the move if the current spot is not occupied
                    if (boardArr[i, j] == Empty)
                    {
                        boardArr[i, j] = AIVal;
                        int score = MiniMaxBoard(boardArr, Depth, MinVal, MaxVal, IsMaximizing);
                        //int score = MiniMaxBoardNoABP(boardArr, Depth, IsMaximizing);
                        boardArr[i, j] = Empty;

                        // if the returned score is greater than the current best score, the best score should be changed to the current score
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove[0] = i;
                            bestMove[1] = j;
                        }
                    }
                }
            }
            //Debug.WriteLine(count);
            return bestMove;
        }

        public int MiniMaxBoard(int[,] boardArr, int Depth, int alpha, int beta, bool IsMaximizing)
        {
            Move move = new Move();
            int winner = move.WinningMove(boardArr, Rows, Columns);

            // if the board that has been passed in is not in a losing state, return the outcome of the game
            // we increment/decrement the return value by the depth to help the AI find the fastest way it can win
            if (winner != -1)
            {
                count++;
                if(winner == 1)
                {
                    return -10 + Depth; // if the winner is the minimizing player
                }else if(winner == 2)
                {
                    return 10 - Depth; // if the winner is the maximizing player
                }
                else
                {
                    return 0; // if the game ends in a draw
                }
            }

            if (IsMaximizing)
            {
                int bestScoreMax = MinVal;
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (boardArr[i, j] == 0)
                        {
                            boardArr[i, j] = AIVal; // set the space on the board denoted by the variables i and j to be occupied by the AI
                            int score = MiniMaxBoard(boardArr, Depth + 1, alpha, beta, false); // recursively call function, passing in the new board
                            boardArr[i, j] = Empty; // clear the space

                            bestScoreMax = Math.Max(bestScoreMax, score); // if the score is higher than the current best score, the score becomes the new best score

                            // set the alpha to whichever is higher between it and the score
                            alpha = Math.Max(alpha, score);

                            // if the beta is less than or equal to the alpha, break out of the loop; we are pruning this position
                            if(beta <= alpha)
                                break;
                        }
                    }
                }
                return bestScoreMax;
            }else
            {
                int bestScoreMin = MaxVal;
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (boardArr[i, j] == Empty)
                        {
                            boardArr[i, j] = PlayerVal; // set the space on the board denoted by the variables i and j to be occupied by the player
                            int score = MiniMaxBoard(boardArr, Depth + 1, alpha, beta, true); // recursively call function, passing in the new board
                            boardArr[i, j] = Empty; // clear the space

                            bestScoreMin = Math.Min(bestScoreMin, score); // if the score is lower than the current best score, the score becomes the new best score

                            // set the beta to whichever is higher between it and the score
                            beta = Math.Min(beta, score);

                            // if the beta is less than or equal to the alpha, break out of the loop; we are pruning this position
                            if (beta <= alpha)
                                break;
                        }
                    }
                }
                return bestScoreMin;
            }
        }

        public int MiniMaxBoardNoABP(int[,] boardArr, int Depth, bool IsMaximizing)
        {
            Move move = new Move();
            int winner = move.WinningMove(boardArr, Rows, Columns);
            if (winner != -1)
            {
                count++;
                if (winner == 1)
                {
                    return -10 + Depth;
                }
                else if (winner == 2)
                {
                    return 10 - Depth;
                }
                else
                {
                    return 0;
                }
            }

            if (IsMaximizing)
            {
                int bestScoreMin = int.MinValue;
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (boardArr[i, j] == 0)
                        {
                            boardArr[i, j] = AIVal;
                            int score = MiniMaxBoardNoABP(boardArr, Depth + 1, false);
                            boardArr[i, j] = Empty;

                            if (score > bestScoreMin)
                            {
                                bestScoreMin = score;
                            }
                        }
                    }
                }
                return bestScoreMin;
            }
            else
            {
                int bestScoreMax = int.MaxValue;
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (boardArr[i, j] == Empty)
                        {
                            boardArr[i, j] = PlayerVal;
                            int score = MiniMaxBoardNoABP(boardArr, Depth + 1, true);
                            boardArr[i, j] = Empty;

                            if (score < bestScoreMax)
                            {
                                bestScoreMax = score;
                            }
                        }
                    }
                }
                return bestScoreMax;
            }
        }

        #region copy
        /*
         *         public int[] AITurn(int[,] boardArr, int Depth)
        {
            int bestScore = int.MinValue;
            int[] bestMove = new int[2];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (boardArr[i, j] == Empty)
                    {
                        boardArr[i, j] = AIVal;
                        int score = MiniMaxBoard(boardArr, Depth, IsMaximizing);
                        boardArr[i, j] = Empty;

                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove[0] = i;
                            bestMove[1] = j;
                        }
                    }
                }
            }
            return bestMove;
        }

        public int MiniMaxBoard(int[,] boardArr, int Depth, bool IsMaximizing)
        {
            Move move = new Move();
            int winner = move.WinningMove(boardArr, Rows, Columns);
            if (winner != -1)
            {
                if(winner == 1)
                {
                    return -10 + Depth;
                }else if(winner == 2)
                {
                    return 10 - Depth;
                }else
                {
                    return 0;
                }
            }

            if (IsMaximizing)
            {
                int bestScoreMin = int.MinValue;
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (boardArr[i, j] == 0)
                        {
                            boardArr[i, j] = AIVal;
                            int score = MiniMaxBoard(boardArr, Depth + 1, false);
                            boardArr[i, j] = Empty;

                            if (score > bestScoreMin)
                            {
                                bestScoreMin = score;
                            }
                        }
                    }
                }
                return bestScoreMin;
            }else
            {
                int bestScoreMax = int.MaxValue;
                for (int i = 0; i < Rows; i++)
                {
                    for (int j = 0; j < Columns; j++)
                    {
                        if (boardArr[i, j] == Empty)
                        {
                            boardArr[i, j] = PlayerVal;
                            int score = MiniMaxBoard(boardArr, Depth + 1, true);
                            boardArr[i, j] = Empty;

                            if (score < bestScoreMax)
                            {
                                bestScoreMax = score;
                            }
                        }
                    }
                }
                return bestScoreMax;
            }
        }
        */
        #endregion
    }
}
