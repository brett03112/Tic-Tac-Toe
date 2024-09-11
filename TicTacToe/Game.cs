namespace TicTacToeGame;

/*
        Make a playable TicTacToe game with game board
        Show the game board
        Choose the players
        Create the game board
        Play the game
*/
#region TicTacToe Class

public enum AIDifficulty
{
    Easy,
    Medium,
    Hard,
    None
}

public class TicTacToe
{
        string Player { get; }
        string Computer { get; } = "Computer";
        string Winner { get; set; } = " ";
        char[,] Board { get; }
        AIDifficulty Difficulty { get; } = AIDifficulty.Easy;

        public TicTacToe(string player, char[,] board, AIDifficulty difficulty)
        {
            Player = player;
            Board = board;
            Difficulty = difficulty;
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
        public void PlayGame(string player1)
        {
            int playerChoice = 0;
            bool noWinner = true;
            bool tie = false;

            while (noWinner)
            {
                bool validMove = false;
                while (!validMove)
                {
                    WriteLine($"{Player} enter a number between 1 and 9: ");
                    if (int.TryParse(ReadLine(), out playerChoice) && IsValidChoice(playerChoice))
                    {
                        try
                        {
                            UpdateBoardPlayer(playerChoice, Board);
                            validMove = true;
                        }
                        catch (ArgumentException)
                        {
                            WriteLine("That space is already taken. Please choose another.");
                        }
                    }
                    else
                    {
                        WriteLine("Invalid choice. Please enter a number between 1 and 9.");
                    }
                }
                
                DisplayBoard(Board);
                tie = CheckTie(Board);
                if (tie)
                {
                    WriteLine("It's a tie!");
                    break;
                }
                noWinner = CheckWinner(Board);

                if (noWinner == false)
                {
                    Winner = Player;
                    WriteLine($"{Player} wins!");
                    break;
                }
                
                // Computer's turn
                WriteLine("Computer is making a move...");
                Thread.Sleep(1000); // Add a small delay to simulate thinking

                int computerChoice = MakeComputerMove();
                UpdateBoardComputer(computerChoice, Board);

                DisplayBoard(Board);
                tie = CheckTie(Board);
                if (tie)
                {
                    WriteLine("It's a tie!");
                    break;
                }
                noWinner = CheckWinner(Board);
                if (noWinner == false)
                {
                    Winner = Computer;
                    WriteLine("Computer wins!");
                    break;
                }
            }
        }

        private int MakeComputerMove()
        {
            return Difficulty switch
            {
                AIDifficulty.Easy => MakeEasyMove(),
                AIDifficulty.Medium => MakeMediumMove(),
                AIDifficulty.Hard => MakeHardMove(),
                AIDifficulty.None => MakeEasyMove(),
                _ => throw new ArgumentException("Invalid difficulty level"),
            };
        }

        private int MakeEasyMove()
        {
            Random random = new Random();
            int choice;
            do
            {
                choice = random.Next(1, 10);
            } while (!IsValidMove(choice));
            return choice;
        }

        private int MakeMediumMove()
        {
            // First, check if we can win
            for (int i = 1; i <= 9; i++)
            {
                if (IsValidMove(i))
                {
                    char[,] tempBoard = (char[,])Board.Clone();
                    UpdateBoardComputer(i, tempBoard);
                    if (!CheckWinner(tempBoard))
                    {
                        return i;
                    }
                }
            }

            // Then, block the player from winning
            for (int i = 1; i <= 9; i++)
            {
                if (IsValidMove(i))
                {
                    char[,] tempBoard = (char[,])Board.Clone();
                    UpdateBoardPlayer(i, tempBoard);
                    if (!CheckWinner(tempBoard))
                    {
                        return i;
                    }
                }
            }

            // If no winning or blocking move, make a random move
            return MakeEasyMove();
        }

        private int MakeHardMove()
        {
            // Implement minimax algorithm for unbeatable AI
            int bestScore = int.MinValue;
            int bestMove = 0;

            for (int i = 1; i <= 9; i++)
            {
                if (IsValidMove(i))
                {
                    char[,] tempBoard = (char[,])Board.Clone();
                    UpdateBoardComputer(i, tempBoard);
                    int score = Minimax(tempBoard, 0, false);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = i;
                    }
                }
            }

            return bestMove;
        }

        private int Minimax(char[,] board, int depth, bool isMaximizing)
        {
            if (!CheckWinner(board))
            {
                return isMaximizing ? -1 : 1;
            }

            if (CheckTie(board))
            {
                return 0;
            }

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 1; i <= 9; i++)
                {
                    if (IsValidMove(i, board))
                    {
                        char[,] tempBoard = (char[,])board.Clone();
                        UpdateBoardComputer(i, tempBoard);
                        int score = Minimax(tempBoard, depth + 1, false);
                        bestScore = Math.Max(score, bestScore);
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 1; i <= 9; i++)
                {
                    if (IsValidMove(i, board))
                    {
                        char[,] tempBoard = (char[,])board.Clone();
                        UpdateBoardPlayer(i, tempBoard);
                        int score = Minimax(tempBoard, depth + 1, true);
                        bestScore = Math.Min(score, bestScore);
                    }
                }
                return bestScore;
            }
        }

        private bool IsValidMove(int choice, char[,] board = null!)
        {
            board = board ?? Board;
            int row = (choice - 1) / 3 * 2;
            int col = ((choice - 1) % 3) * 4 + 1;
            return board[row, col] == ' ';
        }
        #endregion

        #region UpdateBoardPlayer1 method
        /// <summary>
        /// Updates the Tic-Tac-Toe board with the player 1's choice.
        /// </summary>
        /// <param name="player1Choice">The choice made by player 1 (1-9).</param>
        /// <param name="board">The Tic-Tac-Toe board.</param>
        private void UpdateBoardPlayer(int playerChoice, char[,] board)
        {
            const char x = 'X';
            int row = (playerChoice - 1) / 3 * 2;
            int col = ((playerChoice - 1) % 3) * 4 + 1;

            if (board[row, col] == ' ')
            {
                board[row, col] = x;
            }
            else
            {
                throw new ArgumentException("Invalid choice. Space is already taken.", nameof(playerChoice));
            }
        }
        #endregion

        #region UpdateBoardComputer method
        /// <summary>
        /// Updates the Tic-Tac-Toe board with the computer's choice.
        /// </summary>
        /// <param name="computerChoice">The choice made by the computer (1-9).</param>
        /// <param name="board">The Tic-Tac-Toe board.</param>
        private void UpdateBoardComputer(int computerChoice, char[,] board)
        {
            const char o = 'O';
            int row = (computerChoice - 1) / 3 * 2;
            int col = ((computerChoice - 1) % 3) * 4 + 1;

            if (board[row, col] == ' ')
            {
                board[row, col] = o;
            }
            else
            {
                throw new ArgumentException("Invalid choice. Space is already taken.", nameof(computerChoice));
            }
        }
        #endregion

        #region checkWinner method
        /// <summary>
        /// Checks if there is a winner on the Tic-Tac-Toe board.
        /// </summary>
        /// <param name="board">The Tic-Tac-Toe board represented as a 2D character array.</param>
        /// <returns>True if there is a winner, false otherwise.</returns>
        public bool CheckWinner(char[,] board) // [0,1] [0,5] [0,9]   [2,1] [2,5] [2,9]   [4,1] [4,5] [4,9]
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
        public bool CheckTie(char[,] board) // [0,1] [0,5] [0,9]   [2,1] [2,5] [2,9]   [4,1] [4,5] [4,9]
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
        public void InvalidChoicePlayer1(int player1Choice, char[,] board)
        {
            WriteLine("Invalid Choice");
            WriteLine("Player 1 enter a number between 1 and 9: ");
            player1Choice = Convert.ToInt32(ReadLine());
            if (IsValidChoice(player1Choice))
            {
                UpdateBoardPlayer(player1Choice, board);
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
        /// <summary>
        /// checks to see if the choice is within the range of 1-9
        /// </summary>
        /// <param name="playerChoice"></param>
        /// <returns></returns>
        public bool IsValidChoice(int playerChoice)
        {
            if (playerChoice < 1 || playerChoice > 9)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region StartMenu method
        public static void StartMenu()
        {
            while (true)
            {
                Console.Clear();
                WriteLine("Welcome to Tic-Tac-Toe!");
                WriteLine("1. Start New Game");
                WriteLine("2. How to Play");
                WriteLine("3. Exit");
                WriteLine("\nPlease enter your choice (1-3):");

                string? choice = ReadLine();

                switch (choice)
                {
                    case "1":
                        StartNewGame();
                        break;
                    case "2":
                        DisplayHowToPlay();
                        break;
                    case "3":
                        WriteLine("Thank you for playing. Goodbye!");
                        return;
                    default:
                        WriteLine("Invalid choice. Press any key to try again.");
                        ReadKey();
                        break;
                }
            }
        }

        private static void StartNewGame()
        {
            Console.Clear();
            WriteLine("Enter your name:");
            string? playerName = ReadLine();

            WriteLine("Select AI difficulty:");
            WriteLine("1. Easy");
            WriteLine("2. Medium");
            WriteLine("3. Hard");
            
            AIDifficulty difficulty = AIDifficulty.Easy;
            while (true)
            {
                string? difficultyChoice = ReadLine();
                if (difficultyChoice == "1") { difficulty = AIDifficulty.Easy; break; }
                if (difficultyChoice == "2") { difficulty = AIDifficulty.Medium; break; }
                if (difficultyChoice == "3") { difficulty = AIDifficulty.Hard; break; }
                WriteLine("Invalid choice. Please enter 1, 2, or 3.");
            }

            char[,] board = CreateBoard();
            TicTacToe game = new TicTacToe(playerName!, board, difficulty);
            game.PlayGame(playerName!);

            WriteLine("Press any key to return to the main menu.");
            ReadKey();
        }

        private static void DisplayHowToPlay()
        {
            Console.Clear();
            WriteLine("How to Play Tic-Tac-Toe:");
            WriteLine("1. The game is played on a 3x3 grid.");
            WriteLine("2. You are X, the computer is O.");
            WriteLine("3. Players take turns putting their marks in empty squares.");
            WriteLine("4. The first player to get 3 of their marks in a row (up, down, across, or diagonally) is the winner.");
            WriteLine("5. When all 9 squares are full, the game is over. If no player has 3 marks in a row, the game ends in a tie.");
            WriteLine("\nPress any key to return to the main menu.");
            ReadKey();
        }
        #endregion
}
    #endregion
