namespace PlayGame;

/*
        Make a playable TicTacToe game with game board
        Show the game board
        Choose the players
        Create the game board
        Play the game
*/
#region TicTacToe Class

public class TicTacToe
{
        string Player1 { get; } = "Player_1";
        string Player2 { get;} = "Player_2";
        string Winner {get; } = " ";
        char[,] Board { get; } = CreateBoard();

        public TicTacToe(string player1, string player2, char[,] board)
        {
            Player1 = player1;
            Player2 = player2;
            Board = board;
        }
        
        
        #region CreateBoard method
        /// <summary>
            /// Creates a 6x11 character array representing a Tic-Tac-Toe board.
        /// </summary>
            /// <returns>A 6x11 character array representing a Tic-Tac-Toe board.</returns>
        public static char[,] CreateBoard()
        {
            char[,] board = new char[6, 11];

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    if (i == 1 || i == 3)
                    {
                        if (j == 3 || j ==7)
                        {
                            board[i,j] = '|';
                        }
                        else
                        {
                            board[i, j] = '_';
                        }   
                    }
                    if (i == 0 || i == 2 || i == 4 || i == 5)
                    {
                        if (j == 3 || j == 7)
                        {
                            board[i, j] = '|';
                        }
                    
                        else
                        {
                            board[i, j] = ' ';
                        }
                    }
                }
            }

            return board;
        }
    #endregion
        
        #region DisplayBoard method
        public static void DisplayBoard(char[,] board)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    Write(board[i, j]);
                }
                WriteLine();
            }
        }
        #endregion

        #region PlayGame method
        /// <summary>
            /// Plays a game of Tic Tac Toe between two players.
        /// </summary>
            /// <param name="player1">The name of player 1.</param>
            /// <param name="player2">The name of player 2.</param>
            /// <param name="board">The game board.</param>
        public static void PlayGame(string player1, string player2, char[,] board)
        {
            string winner = " ";
            int player1Choice = 0;
            int player2Choice = 0;
            bool noWinner = true;
            bool tie = false;
            while (noWinner)
            {
                WriteLine("Player 1 enter a number between 1 and 9: ");
                player1Choice = Convert.ToInt32(ReadLine());
                UpdateBoardPlayer1(player1Choice, board);
                DisplayBoard(board);
                tie = CheckTie(board);
                if (tie == true)
                {
                    WriteLine("It's a tie!");
                    break;
                }
                noWinner = checkWinner(board);

                if (noWinner == false)
                {
                    winner = player1;
                    WriteLine("Player 1 wins !");
                    break;
                }
                
                WriteLine("Player 2 enter a number between 1 and 9: ");
                player2Choice = Convert.ToInt32(ReadLine());
                UpdateBoardPlayer2(player2Choice, board);
                DisplayBoard(board);
                tie = CheckTie(board);
                if (tie == true)
                {
                    WriteLine("It's a tie!");
                    break;
                }
                noWinner = checkWinner(board);
                if (noWinner == false)
                {
                    winner = player2;
                    WriteLine("Player 2 wins !");
                    break;
                }
                
            }
        }
        #endregion

        #region UpdateBoardPlayer1 method
        /// <summary>
            /// Updates the Tic-Tac-Toe board with the player 1's choice.
        /// </summary>
            /// <param name="player1Choice">The choice made by player 1 (1-9).</param>
            /// <param name="board">The Tic-Tac-Toe board.</param>
        public static void UpdateBoardPlayer1(int player1Choice, char[,] board)
        {
            if (player1Choice == 1 && board[0,1] == ' ')
            {
            board[0, 1] = 'X';
            }
            else if (player1Choice == 2 && board[0, 5] == ' ')
            {
                board[0, 5] = 'X';
            }
            else if (player1Choice == 3 && board[0, 9] == ' ')
            {
                board[0, 9] = 'X';
            }
            else if (player1Choice == 4 && board[2, 1] == ' ')
            {
                board[2, 1] = 'X';
            }
            else if (player1Choice == 5 && board[2, 5] == ' ')
            {
                board[2, 5] = 'X';
            }
            else if (player1Choice == 6 && board[2, 9] == ' ')
            {
                board[2, 9] = 'X';
            }
            else if (player1Choice == 7 && board[4, 1] == ' ')
            {
                board[4, 1] = 'X';
            }
            else if (player1Choice == 8 && board[4, 5] == ' ')
            {
                board[4, 5] = 'X';
            }
            else if (player1Choice == 9 && board[4, 9] == ' ')
            {
                board[4, 9] = 'X';
            }
            else
            {
                WriteLine("Invalid Choice");
                WriteLine("Player 1 enter a number between 1 and 9: ");
                player1Choice = Convert.ToInt32(ReadLine());
                UpdateBoardPlayer1(player1Choice, board);
            }        
        }
        #endregion

        #region UpdateBoardPlayer2 method
        /// <summary>
            /// Updates the Tic-Tac-Toe board with the player 2's choice.
            /// </summary>
            /// <param name="player2Choice">The choice made by player 2 (1-9).</param>
            /// <param name="board">The Tic-Tac-Toe board.</param>
        public static void UpdateBoardPlayer2(int player2Choice, char[,] board)
        {
            if (player2Choice == 1 && board[0, 1] == ' ')
            {
                board[0, 1] = 'O';
            }
            else if (player2Choice == 2 && board[0, 5] == ' ')
            {
                board[0, 5] = 'O';
            }
            else if (player2Choice == 3 && board[0, 9] == ' ')
            {
                board[0, 9] = 'O';
            }
            else if (player2Choice == 4 && board[2, 1] == ' ')
            {
                board[2, 1] = 'O';
            }
            else if (player2Choice == 5 && board[2, 5] == ' ')
            {
                board[2, 5] = 'O';
            }
            else if (player2Choice == 6 && board[2, 9] == ' ')
            {
                board[2, 9] = 'O';
            }
            else if (player2Choice == 7 && board[4, 1] == ' ')
            {
                board[4, 1] = 'O';
            }
            else if (player2Choice == 8 && board[4, 5] == ' ')
            {
                board[4, 5] = 'O';
            }
            else if (player2Choice == 9 && board[4, 9] == ' ')
            {
                board[4, 9] = 'O';
            }
            else
            {
                WriteLine("Invalid choice");
                WriteLine("Player 2 enter a number between 1 and 9: ");
                player2Choice = Convert.ToInt32(ReadLine());
                UpdateBoardPlayer2(player2Choice, board);
            }                     
        }
        #endregion

        #region checkWinner method
        /// <summary>
            /// Checks if there is a winner on the Tic-Tac-Toe board.
        /// </summary>
            /// <param name="board">The Tic-Tac-Toe board represented as a 2D character array.</param>
            /// <returns>True if there is a winner, false otherwise.</returns>
        public static bool checkWinner(char[,] board) // [0,1] [0,5] [0,9]   [2,1] [2,5] [2,9]   [4,1] [4,5] [4,9]
        {
            if ((board[0, 1] == board[0, 5]) && (board[0, 5] == board[0, 9]) && (board[0,9] != ' '))// top row
            {    
                return false;
                
            }
            else if (board[2, 1] == board[2, 5] && board[2, 5] == board[2, 9] && board[2, 9] != ' ') // middle row
            {
                
                return false;
                
            }
            else if (board[4, 1] == board[4, 5] && board[4, 9] == board[4, 5] && board[4, 5] != ' ') // bottom row
            {
                
                return false;
            }
            else if (board[0, 1] == board[2, 1] && board[2, 1] == board[4, 1] && board[4, 1] != ' ') // left column
            {
                
                return false;
            }
            else if (board[0, 5] == board[2, 5] && board[2, 5] == board[4, 5] && board[4, 5] != ' ') // middle column
            {
                
                return false;
            }
            else if (board[0, 9] == board[2, 9] && board[2, 9] == board[4, 9] && board[4, 9] != ' ') // right column
            {
            return false;
            }
            else if (board[0, 1] == board[2, 5] && board[2, 5] == board[4, 9] && board[4, 9] != ' ') // top left to bottom right
            {
                return false;
            }
            else if (board[0, 9] == board[2, 5] && board[2, 5] == board[4, 1] && board[4, 1] != ' ') // top right to bottom left
            {
                return false;
                
            }
            else
            {    
                return true; // no winner
            }
        }
        #endregion

        #region checkTie method
        /// <summary>
        /// Checks if the game board is full, indicating a tie.
        /// </summary>
        /// <param name="board">The game board represented as a 2D char array.</param>
        /// <returns>True if the board is full, false otherwise.</returns>
        public static bool CheckTie(char[,] board) // [0,1] [0,5] [0,9]   [2,1] [2,5] [2,9]   [4,1] [4,5] [4,9]
        {
            if (board[0,1] != ' ' && board[0,5] != ' ' && board[0,9] != ' ' && board[2,1] != ' ' && board[2,5] != ' ' && board[2,9] != ' ' && board[4,1] != ' ' && board[4,5] != ' ' && board[4,9] != ' ')
            {
                return true;
            }

            return false;
        }
        #endregion
}
    #endregion
