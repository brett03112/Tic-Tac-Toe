namespace TicTacToeGame;

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
        /// <summary>
        /// Displays the Tic-Tac-Toe board.
        /// </summary>
        /// <param name="board"></param> 
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
                if (IsValidChoice(player1Choice))
                {
                    UpdateBoardPlayer1(player1Choice, board);
                }
                else
                {
                    InvalidChoicePlayer1(player1Choice, board);
                }
                
                DisplayBoard(board);
                tie = CheckTie(board);
                if (tie)
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
                if (IsValidChoice(player2Choice))
                {
                    UpdateBoardPlayer2(player2Choice, board);
                }
                else
                {
                    InvalidChoicePlayer2(player2Choice, board);
                }
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
            char x = 'X';
            #pragma warning disable CS8846 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
            char update = board switch
            {
                var letter when player1Choice == 1 && board[0, 1] == ' ' => board[0, 1] = x,
                var letter when player1Choice == 2 && board[0, 5] == ' ' => board[0, 5] = x,
                var letter when player1Choice == 3 && board[0, 9] == ' ' => board[0, 9] = x,
                var letter when player1Choice == 4 && board[2, 1] == ' ' => board[2, 1] = x,
                var letter when player1Choice == 5 && board[2, 5] == ' ' => board[2, 5] = x,
                var letter when player1Choice == 6 && board[2, 9] == ' ' => board[2, 9] = x,
                var letter when player1Choice == 7 && board[4, 1] == ' ' => board[4, 1] = x,
                var letter when player1Choice == 8 && board[4, 5] == ' ' => board[4, 5] = x,
                var letter when player1Choice == 9 && board[4, 9] == ' ' => board[4, 9] = x,
                _ => throw new ArgumentException("Invalid choice. Space is already taken.", nameof(player1Choice)),
            };
       
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
            char o = 'O';
            #pragma warning disable CS8846 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
            char update = board switch
            {
                var letter when player2Choice == 1 && board[0, 1] == ' ' => board[0, 1] = o,
                var letter when player2Choice == 2 && board[0, 5] == ' ' => board[0, 5] = o,
                var letter when player2Choice == 3 && board[0, 9] == ' ' => board[0, 9] = o,
                var letter when player2Choice == 4 && board[2, 1] == ' ' => board[2, 1] = o,
                var letter when player2Choice == 5 && board[2, 5] == ' ' => board[2, 5] = o,
                var letter when player2Choice == 6 && board[2, 9] == ' ' => board[2, 9] = o,
                var letter when player2Choice == 7 && board[4, 1] == ' ' => board[4, 1] = o,
                var letter when player2Choice == 8 && board[4, 5] == ' ' => board[4, 5] = o,
                var letter when player2Choice == 9 && board[4, 9] == ' ' => board[4, 9] = o,
                _ => throw new ArgumentException("Invalid choice. Space is already taken.", nameof(player2Choice)),
            };
            
                                 
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
            bool winner = true;
            return winner switch
            {
                var letter when board[0, 1] == board[0, 5] && board[0, 5] == board[0, 9] && board[0, 9] != ' ' => winner = false,
                var letter when board[2, 1] == board[2, 5] && board[2, 5] == board[2, 9] && board[2, 9] != ' ' => winner = false,
                var letter when board[4, 1] == board[4, 5] && board[4, 9] == board[4, 5] && board[4, 5] != ' ' => winner = false,
                var letter when board[0, 1] == board[2, 1] && board[2, 1] == board[4, 1] && board[4, 1] != ' ' => winner = false,
                var letter when board[0, 5] == board[2, 5] && board[2, 5] == board[4, 5] && board[4, 5] != ' ' => winner = false,
                var letter when board[0, 9] == board[2, 9] && board[2, 9] == board[4, 9] && board[4, 9] != ' ' => winner = false,
                var letter when board[0, 1] == board[2, 5] && board[2, 5] == board[4, 9] && board[4, 9] != ' ' => winner = false,
                var letter when board[0, 9] == board[2, 5] && board[2, 5] == board[4, 1] && board[4, 1] != ' ' => winner = false,
                _ => winner,
            };
            
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

        #region InvalidChoicePlayer1 InvalidChoicePlayer2 IsValidChoice method
        /// <summary>
        /// logic for choices outside of the range of 1-9
        /// </summary>
        /// <param name="player1Choice"></param>
        /// <param name="board"></param>
        public static void InvalidChoicePlayer1(int player1Choice, char[,] board)
        {
            WriteLine("Invalid Choice");
            WriteLine("Player 1 enter a number between 1 and 9: ");
            player1Choice = Convert.ToInt32(ReadLine());
            if (IsValidChoice(player1Choice))
            {
                UpdateBoardPlayer1(player1Choice, board);
            }
            else
            {
                InvalidChoicePlayer1(player1Choice, board);
            }
        }
        /// <summary>
        /// logic for choices outside of the range of 1-9
        /// </summary>
        /// <param name="player2Choice"></param>
        /// <param name="board"></param>
        public static void InvalidChoicePlayer2(int player2Choice, char[,] board)
        {
            WriteLine("Invalid Choice");
            WriteLine("Player 2 enter a number between 1 and 9: ");
            player2Choice = Convert.ToInt32(ReadLine());
            if (IsValidChoice(player2Choice))
            {
                UpdateBoardPlayer2(player2Choice, board);
            }
            else
            {
                InvalidChoicePlayer2(player2Choice, board);
            }
        }
        /// <summary>
        /// checks to see if the choice is within the range of 1-9
        /// </summary>
        /// <param name="playerChoice"></param>
        /// <returns></returns>
        public static bool IsValidChoice(int playerChoice)
        {
            if (playerChoice < 1 || playerChoice > 9)
            {
                return false;
            }
            return true;
        }
        #endregion
}
    #endregion
