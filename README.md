# Tic-Tac-Toe

A game of tic-tac-toe (level 24)from the book "The C# Player's Guide 5th Edition" by RB Whitaker.

The game involves a player and the computer.  The computer has AI with levels of easy, medium, and hard.

## Setting up .NET 8

To set up .NET 8, follow these steps:

1. Download the .NET 8 SDK from the official Microsoft website: [https://dotnet.microsoft.com/en-us/download](https://dotnet.microsoft.com/en-us/download)
2. Install the SDK by following the on-screen instructions.
3. Verify the installation by opening a command prompt or terminal and running the command `dotnet --version`. This should display the installed .NET 8 version.

## Features

* Player vs. Computer gameplay
* AI with three difficulty levels: Easy, Medium, and Hard

## How to Play

1. Run the game.
   '''bash
   dotnet run --project PlayGame/PlayGame.csproj
   '''
2. Select your difficulty level.
3. The game board will be displayed, with numbers 1-9 representing the available moves.
4. Enter the number corresponding to the position where you want to place your mark (X).
5. The computer will make its move (O).
6. The game continues until there is a winner or a draw.
