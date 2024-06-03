using  PlayGame;

/*
    Make a playable TicTacToe game with game board
    Show the game board
    Choose the players
    Create the game board
    Play the game
*/



WriteLine(" 1 | 2 | 3 "); // [0,1] [0,5] [0,9]
WriteLine("___|___|___");
WriteLine(" 4 | 5 | 6 "); // [2,1] [2,5] [2,9]
WriteLine("___|___|___");
WriteLine(" 7 | 8 | 9 "); // [4,1] [4,5] [4,9]
WriteLine("   |   |   ");

// get player names
WriteLine("Enter a name for player 1: ");
string? player1 = ReadLine()!;
WriteLine("Enter a name for player 2: ");
string? player2 = ReadLine()!;

// create board
char[,] board = PlayGame.TicTacToe.CreateBoard();

// create the game
PlayGame.TicTacToe game = new PlayGame.TicTacToe(player1, player2, board);

// play the game
PlayGame.TicTacToe.PlayGame(player1, player2, board);