using System;

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
                    if (board[i, j] == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        Console.Write(board[i, j]);
                        Console.ResetColor();
                    }
                    else if (board[i, j] == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(board[i, j]);
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(board[i, j]);
                    }
                }
                Console.WriteLine();
            }
        }
        #endregion

        #region CoinFlip method
        /// <summary>
        /// Simulates a coin flip and returns true if the player wins the toss, false otherwise.
        /// </summary>
        /// <returns>True if the player wins the toss, false if the computer wins.</returns>
        private bool CoinFlip()
        {
            WriteLineWithColor($"{Player}, choose Heads or Tails: ", ConsoleColor.Cyan);
            string? choice = Console.ReadLine()?.ToLower();
            while (choice != "heads" && choice != "tails")
            {
                WriteLineWithColor("Invalid choice. Please enter Heads or Tails: ", ConsoleColor.Red);
                choice = Console.ReadLine()?.ToLower();
            }

            Random random = new Random();
            bool isHeads = random.Next(2) == 0;
            string result = isHeads ? "Heads" : "Tails";

            WriteLineWithColor($"The coin flip result is: {result}", ConsoleColor.Yellow);

            return (choice == "heads" && isHeads) || (choice == "tails" && !isHeads);
        }
        #endregion

        #region PlayGame method
        /// <summary>
        /// Plays a game of Tic Tac Toe between the player and the computer.
        /// </summary>
        /// <param name="player1">The name of the player.</param>
        public void PlayGame(string player1)
        {
            bool playerTurn = CoinFlip();
            if (playerTurn)
            {
                WriteLineWithColor($"{Player} won the toss! You go first.", ConsoleColor.Green);
            }
            else
            {
                WriteLineWithColor("Computer won the toss! Computer goes first.", ConsoleColor.Yellow);
            }

            bool gameOver = false;
            bool tie = false;

            while (!gameOver)
            {
                if (playerTurn)
                {
                    PlayerMove();
                }
                else
                {
                    ComputerMove();
                }

                DisplayBoard(Board);
                gameOver = CheckWinner(Board);
                if (gameOver)
                {
                    Winner = playerTurn ? Player : Computer;
                    WriteLineWithColor($"{Winner} wins!", playerTurn ? ConsoleColor.Green : ConsoleColor.Yellow);
                    break;
                }

                tie = CheckTie(Board);
                if (tie)
                {
                    WriteLineWithColor("It's a tie!", ConsoleColor.Magenta);
                    break;
                }

                playerTurn = !playerTurn;
            }
        }

        private void PlayerMove()
        {
            bool validMove = false;
            while (!validMove)
            {
                WriteLineWithColor($"{Player} enter a number between 1 and 9: ", ConsoleColor.Cyan);
                if (int.TryParse(Console.ReadLine(), out int playerChoice) && IsValidChoice(playerChoice))
                {
                    try
                    {
                        UpdateBoardPlayer(playerChoice, Board);
                        validMove = true;
                    }
                    catch (ArgumentException)
                    {
                        WriteLineWithColor("That space is already taken. Please choose another.", ConsoleColor.Red);
                    }
                }
                else
                {
                    WriteLineWithColor("Invalid choice. Please enter a number between 1 and 9.", ConsoleColor.Red);
                }
            }
        }

        private void ComputerMove()
        {
            WriteLineWithColor("Computer is making a move...", ConsoleColor.Yellow);
            Thread.Sleep(1000); // Add a small delay to simulate thinking

            int computerChoice = MakeComputerMove();
            UpdateBoardComputer(computerChoice, Board);
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
                    if (CheckWinner(tempBoard))
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
                    if (CheckWinner(tempBoard))
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
            if (CheckWinner(board))
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

        #region UpdateBoardPlayer method
        /// <summary>
        /// Updates the Tic-Tac-Toe board with the player's choice.
        /// </summary>
        /// <param name="playerChoice">The choice made by the player (1-9).</param>
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
        public bool CheckWinner(char[,] board)
        {
            // Define winning combinations
            int[][] winCombos = new int[][]
            {
                new int[] {0, 1, 0, 5, 0, 9}, // Top row
                new int[] {2, 1, 2, 5, 2, 9}, // Middle row
                new int[] {4, 1, 4, 5, 4, 9}, // Bottom row
                new int[] {0, 1, 2, 1, 4, 1}, // Left column
                new int[] {0, 5, 2, 5, 4, 5}, // Middle column
                new int[] {0, 9, 2, 9, 4, 9}, // Right column
                new int[] {0, 1, 2, 5, 4, 9}, // Diagonal from top-left to bottom-right
                new int[] {0, 9, 2, 5, 4, 1}  // Diagonal from top-right to bottom-left
            };

            foreach (var combo in winCombos)
            {
                if (board[combo[0], combo[1]] != ' ' &&
                    board[combo[0], combo[1]] == board[combo[2], combo[3]] &&
                    board[combo[0], combo[1]] == board[combo[4], combo[5]])
                {
                    return true; // We have a winner
                }
            }

            return false; // No winner found
        }
        #endregion

        #region checkTie method
        /// <summary>
        /// Checks if the game board is full, indicating a tie.
        /// </summary>
        /// <param name="board">The game board represented as a 2D char array.</param>
        /// <returns>True if the board is full, false otherwise.</returns>
        public bool CheckTie(char[,] board)
        {
            for (int i = 0; i <= 4; i += 2)
            {
                for (int j = 1; j <= 9; j += 4)
                {
                    if (board[i, j] == ' ')
                    {
                        return false; // Found an empty space, game is not tied
                    }
                }
            }
            return true; // All spaces are filled, game is tied
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
            WriteLineWithColor("Invalid Choice", ConsoleColor.Red);
            WriteLineWithColor("Player 1 enter a number between 1 and 9: ", ConsoleColor.Cyan);
            player1Choice = Convert.ToInt32(Console.ReadLine());
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
                WriteLineWithColor("Welcome to Tic-Tac-Toe!", ConsoleColor.Magenta);
                WriteLineWithColor("1. Start New Game", ConsoleColor.Cyan);
                WriteLineWithColor("2. How to Play", ConsoleColor.Cyan);
                WriteLineWithColor("3. Exit", ConsoleColor.Cyan);
                WriteLineWithColor("\nPlease enter your choice (1-3):", ConsoleColor.White);

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        StartNewGame();
                        break;
                    case "2":
                        DisplayHowToPlay();
                        break;
                    case "3":
                        WriteLineWithColor("Thank you for playing. Goodbye!", ConsoleColor.Green);
                        return;
                    default:
                        WriteLineWithColor("Invalid choice. Press any key to try again.", ConsoleColor.Red);
                        Console.ReadKey();
                        break;
                }
            }
        }

        private static void StartNewGame()
        {
            Console.Clear();
            WriteLineWithColor("Enter your name:", ConsoleColor.Cyan);
            string? playerName = Console.ReadLine();

            WriteLineWithColor("Select AI difficulty:", ConsoleColor.Yellow);
            WriteLineWithColor("1. Easy", ConsoleColor.Green);
            WriteLineWithColor("2. Medium", ConsoleColor.Yellow);
            WriteLineWithColor("3. Hard", ConsoleColor.Red);
            
            AIDifficulty difficulty = AIDifficulty.Easy;
            while (true)
            {
                string? difficultyChoice = Console.ReadLine();
                if (difficultyChoice == "1") { difficulty = AIDifficulty.Easy; break; }
                if (difficultyChoice == "2") { difficulty = AIDifficulty.Medium; break; }
                if (difficultyChoice == "3") { difficulty = AIDifficulty.Hard; break; }
                WriteLineWithColor("Invalid choice. Please enter 1, 2, or 3.", ConsoleColor.Red);
            }

            char[,] board = CreateBoard();
            
            // Display the initial game board with numbered positions
            Console.Clear();
            WriteLineWithColor("Here's the game board:", ConsoleColor.Magenta);
            DisplayInitialBoard();
            WriteLineWithColor("Remember these positions for your moves!", ConsoleColor.Yellow);
            Console.WriteLine();

            TicTacToe game = new TicTacToe(playerName!, board, difficulty);
            game.PlayGame(playerName!);

            WriteLineWithColor("Press any key to return to the main menu.", ConsoleColor.White);
            Console.ReadKey();
        }

        private static void DisplayHowToPlay()
        {
            Console.Clear();
            WriteLineWithColor("How to Play Tic-Tac-Toe:", ConsoleColor.Magenta);
            WriteLineWithColor("1. The game is played on a 3x3 grid.", ConsoleColor.White);
            WriteLineWithColor("2. You are X, the computer is O.", ConsoleColor.White);
            WriteLineWithColor("3. Players take turns putting their marks in empty squares.", ConsoleColor.White);
            WriteLineWithColor("4. The first player to get 3 of their marks in a row (up, down, across, or diagonally) is the winner.", ConsoleColor.White);
            WriteLineWithColor("5. When all 9 squares are full, the game is over. If no player has 3 marks in a row, the game ends in a tie.", ConsoleColor.White);
            WriteLineWithColor("6. Before the game starts, you'll participate in a coin flip to decide who goes first.", ConsoleColor.White);
            WriteLineWithColor("\nHere's how the positions are numbered on the board:", ConsoleColor.Yellow);
            DisplayInitialBoard();
            WriteLineWithColor("\nPress any key to return to the main menu.", ConsoleColor.White);
            Console.ReadKey();
        }
        #endregion

        #region Helper Methods
        private static void WriteLineWithColor(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void DisplayInitialBoard()
        {
            WriteLineWithColor(" 1 | 2 | 3 ", ConsoleColor.Cyan);
            WriteLineWithColor("___|___|___", ConsoleColor.Cyan);
            WriteLineWithColor(" 4 | 5 | 6 ", ConsoleColor.Cyan);
            WriteLineWithColor("___|___|___", ConsoleColor.Cyan);
            WriteLineWithColor(" 7 | 8 | 9 ", ConsoleColor.Cyan);
            WriteLineWithColor("   |   |   ", ConsoleColor.Cyan);
        }
        #endregion
}
#endregion
