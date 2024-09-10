using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Connect4Lib;
using Windows.UI.Xaml.Shapes;
using Windows.UI;
using System.Diagnostics;
using System.Collections;
using Connect4Lib.AI;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Connect4
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // game object for holding the back-end properties of the game
        Game g;

        // brush object for displaying coloured text and icons
        Brushes brush = new Brushes();

        // 2D array for manipulating the display of the game
        private Ellipse[,] boardDisplayArr;

        public MainPage()
        {
            this.InitializeComponent();
            CreateGame();
        }

        private void CreateGame()
        {
            g = new Game();

            boardDisplayArr = new Ellipse[g.getRows(), g.getColumns()];

            txtStatus.Foreground = brush.getGrey();

            DrawBoard();
            AddElements();

            if (g.getTurn() == g.getPlayerValue())
            {
                txtStatus.Text = "Player's Turn!";
            }
            else
            {
                txtStatus.Text = "AI's Turn!";
                DisplayAIPlay();
            }
        }

        private void DrawBoard()
        {
            // wipes all column and row definitions
            Board.ColumnDefinitions.Clear(); 
            Board.RowDefinitions.Clear();
            Board.Children.Clear();

            for (int i = 0; i < g.getColumns(); i++)
            {
                ColumnDefinition col = new ColumnDefinition{Width = new GridLength(1, GridUnitType.Star)};
                Board.ColumnDefinitions.Add(col);
            }
            for (int j = 0; j < g.getRows(); j++)
            {
                RowDefinition row = new RowDefinition{Height = new GridLength(1, GridUnitType.Star)};
                Board.RowDefinitions.Add(row);
            }
        }

        private void AddElements()
        {
            for(int i = 0; i < g.getRows(); i++)
            {
                for(int j = 0; j < g.getColumns(); j++)
                {
                    StackPanel space = new StackPanel
                    {
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    space.Tapped += new TappedEventHandler(SpaceTapped);

                    Ellipse tokenSpace = new Ellipse
                    {
                        Width = 50,
                        Height = 50,
                        Fill = new SolidColorBrush(Color.FromArgb(255, 250, 249, 248))
                    };

                    g.createBoard(i, j);
                    boardDisplayArr[i, j] = tokenSpace;

                    space.Children.Add(tokenSpace);
                    Grid.SetRow(space, i);
                    Grid.SetColumn(space, j);
                    Board.Children.Add(space);
                }
            }
        }
        private void SpaceTapped(object sender, TappedRoutedEventArgs e)
        {
            StackPanel panel = (StackPanel)sender;
            int[] tempPlayerDisplayArr = g.PlayerSelected(panel);

            if(tempPlayerDisplayArr[0] != -1)
            {
                Ellipse checkerPlayer = boardDisplayArr[tempPlayerDisplayArr[0], tempPlayerDisplayArr[1]];
                UpdateDisplay(checkerPlayer);
                txtStatus.Text = g.PiecePlayed();

                if (!g.IsComplete())
                {
                    DisplayAIPlay();
                }
            }
        }

        private void DisplayAIPlay()
        {
            int[] tempAIDisplayArr = g.AIPlayPiece();
            Ellipse checkerAI = boardDisplayArr[tempAIDisplayArr[0], tempAIDisplayArr[1]];
            UpdateDisplay(checkerAI);
            txtTest.Text = tempAIDisplayArr[0] + " " + tempAIDisplayArr[1];
            txtStatus.Text = g.PiecePlayed();
        }

        private void UpdateDisplay(Ellipse e)
        {
            if (g.getTurn() != g.getPlayerValue())
            {
                e.Fill = brush.getYellow();
            }
            else
            {
                e.Fill = brush.getRed();
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            CreateGame();
        }

        #region testCode
        /*
        private void SpaceTapped(object sender, TappedRoutedEventArgs e)
        {
            StackPanel panel = (StackPanel)sender;

            // retrieve column of space tapped
            int column = Grid.GetColumn(panel);
            Column tapped = columnList[column];

            if (!tapped.getIsFull())
            {
                int row = tapped.addPiece(playerTurn);
                Ellipse checker = boardDisplayArr[row, column];

                if (!playerTurn)
                {
                    checker.Fill = new SolidColorBrush(Colors.Yellow);
                }
                else
                {
                    checker.Fill = new SolidColorBrush(Colors.Red);
                }

                if (!winningMove(column, row))
                {
                    if (!playerTurn)
                    {
                        playerTurn = true;
                    }
                    else
                    {
                        playerTurn = false;
                    }
                }
                else
                {
                    Board.Background = new SolidColorBrush(Colors.Brown);
                }
            }
        }

        private bool winningMove(int column, int row)
        {
            // checks if player has vertical 4 in a row
            for(int i = 0; i < row - 3; i++)
            {
                for(int j = 0; j < column; j++)
                {
                    if(columnList[j].getSlot(i).getIsPlayer() == columnList[j].getSlot(i + 1).getIsPlayer())
                    {
                        Debug.WriteLine(columnList[j].getSlot(i).getIsPlayer());
                        Debug.WriteLine(i);
                        Debug.WriteLine(j);
                        Debug.WriteLine(columnList[j].getSlot(i + 1).getIsPlayer());
                        Debug.WriteLine(columnList[j].getSlot(i + 2).getIsPlayer());
                        Debug.WriteLine(columnList[j].getSlot(i + 3).getIsPlayer());
                        return true;
                    }
                }
            }

            return false;
        }

                private int ScorePosition()
        {
            int score = 0;
            for(int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns - 3; j++)
                {
                    List<int> rowList = new List<int>();

                    for (int k = 0; k < fourInARow; k++)
                    {
                        rowList.Add(boardArr[i, j]);
                    }

                    if (rowList.Where(temp => temp.Equals(playerTurn)).Select(temp => temp).Count() == fourInARow)
                    {
                        score += 100;
                    } else if(rowList.Where(temp => temp.Equals(playerTurn)).Select(temp => temp).Count() == fourInARow - 1 && rowList.Where(temp => temp.Equals(Empty)).Select(temp => temp).Count() == 1)
                    {
                        score += 10;
                    }

                }
            }
            return score;
        }
        */
        #endregion
    }
}

