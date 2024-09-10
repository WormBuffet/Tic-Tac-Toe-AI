using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI;
using Connect4Lib.AI;
using System.Diagnostics;

namespace Connect4Lib
{
    public class Game
    {
        // number of rows and columns for game board; do not change values
        private const int Columns = 3;
        private const int Rows = 3;

        // variables which define what number denotes who's turn it is
        private const int Player = 1;
        private const int AI = 2;
        private const int Empty = 0;

        // variable denoting that the game has not been won
        private const int NotWon = -1;

        // 2D array for storing the state of the board
        private readonly int[,] boardArr;

        // denotes whether it is the player's turn to play
        private int playerTurn;

        // boolean denoting whether the game has been won
        private bool isDone;

        // minimax algorithm for the ai
        private readonly MiniMax miniMax;

        // boolean denoting whether the AI is trying to maximize or minimize their score; do not change value
        private const bool isMaximizing = false;

        // depth of AI
        private const int depth = 0;

        // stopwatch for testing
        Stopwatch s;

        public Game()
        {
            Debug.WriteLine(" ");
            boardArr = new int[Rows, Columns];
            s = new Stopwatch();

            isDone = false;
            playerTurn = Player;

            miniMax = new MiniMax(Rows, Columns, isMaximizing, Player, AI, Empty);
            //move = new Move();
        }

        public int[] PlayerSelected(StackPanel p)
        {
            int[] sendDisplay = new int[2];
            if (!isDone && playerTurn == Player)
            {
                StackPanel panel = p;

                // retrieve column and row of space tapped
                int column = Grid.GetColumn(panel);
                int row = Grid.GetRow(panel);

                if (boardArr[row, column] == Empty)
                {
                    boardArr[row, column] = Player;
                    sendDisplay[0] = row;
                    sendDisplay[1] = column;
                }
            }
            return sendDisplay;
        }

        public int[] AIPlayPiece()
        {
            s.Start();
            int[] bestMove = miniMax.AITurn(boardArr, depth);
            boardArr[bestMove[0], bestMove[1]] = AI;
            s.Stop();
            Debug.WriteLine(s.ElapsedMilliseconds);
            return bestMove;
        }

        public string PiecePlayed()
        {
            Move move = new Move();
            string newDisplay;
            int check = move.WinningMove(boardArr, Rows, Columns);

            if (check == NotWon)
            {
                if (playerTurn == AI)
                {
                    playerTurn = Player;
                    newDisplay = "Player 1's Turn!";
                }
                else
                {
                    playerTurn = AI;
                    newDisplay = "Player 2's Turn!";
                }
            }
            else if(check == Empty)
            {
                isDone = true;
                newDisplay = "Game is a Draw!";
            }
            else
            {
                isDone = true;

                if (playerTurn == AI)
                {
                    newDisplay = "Player 2 Wins!";
                    //t.Foreground = brush.getYellow();
                }
                else
                {
                    newDisplay = "Player 1 Wins!";
                    //t.Foreground = brush.getRed();
                }
            }
            return newDisplay;
        }

        public void createBoard(int row, int column)
        {
            boardArr[row, column] = Empty;
        }

        public bool IsComplete()
        {
            return isDone;
        }

        public int getTurn()
        {
            return playerTurn;
        }

        public int getRows()
        {
            return Rows;
        }

        public int getColumns()
        {
            return Columns;
        }

        public int getPlayerValue()
        {
            return Player;
        }

        public int getAIValue()
        {
            return AI;
        }

        public int getEmptyValue()
        {
            return Empty;
        }
    }
}
