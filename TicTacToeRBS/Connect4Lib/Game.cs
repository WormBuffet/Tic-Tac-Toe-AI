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
using System.Threading;

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

        // rule based system for the ai
        private readonly RBS rbs;

        // counter for the number of moves remaining
        private int movesRemaning;

        // stopwatch for testing
        Stopwatch s;

        public Game()
        {
            Debug.WriteLine(" ");
            boardArr = new int[Rows, Columns];
            s = new Stopwatch();

            isDone = false;
            playerTurn = Player;

            rbs = new RBS(Rows, Columns, Player, AI, Empty);
            movesRemaning = Rows * Columns;
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
                return sendDisplay;
            }
            sendDisplay[0] = -1;
            return sendDisplay;
        }

        public int[] AIPlayPiece()
        {
            s.Start();
            int[] bestMove = rbs.AITurn(boardArr);
            boardArr[bestMove[0], bestMove[1]] = AI;
            s.Stop();
            Debug.WriteLine(s.ElapsedMilliseconds);
            return bestMove;
        }

        public string PiecePlayed()
        {
            Move move = new Move(Rows, Columns);
            string newDisplay;
            int check = move.WinningMove(boardArr);

            movesRemaning--;
            if (check == NotWon && movesRemaning > Empty)
            {
                if (playerTurn == AI)
                {
                    playerTurn = Player;
                    newDisplay = "Player's Turn!";
                }
                else
                {
                    playerTurn = AI;
                    newDisplay = "AI's Turn!";
                }
            }
            else
            {
                isDone = true;
                Debug.WriteLine(movesRemaning);

                if(movesRemaning == Empty)
                {
                    newDisplay = "Game is a Draw!";
                }
                else if (playerTurn == AI)
                {
                    newDisplay = "AI Wins!";
                    //t.Foreground = brush.getYellow();
                }
                else
                {
                    newDisplay = "Player Wins!";
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
    }
}
