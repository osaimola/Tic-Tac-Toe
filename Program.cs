using System;

namespace CSharpAssignment
{
    class Program
    {
        static string currentPlayer = "X";
        static string[,] gameState = { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } };
        static string[,] validPositions = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
        static void Main(string[] args)
        {
            Program.PlayGame();

        }

        static void PlayGame()
        //begin a new game
        // consider taking a user input to determine if single player vs AI or multiplayer
        {

            Console.Clear(); // only clears the visible portion of the console...https://docs.microsoft.com/en-us/dotnet/api/system.console.clear?view=netcore-3.1

            //TODO: CHECK IF SOMEONE WON, IF NOT PROCEED WITH BELOW
            // if (GameEnded(gameStatus)) { 
            // display winner.
            //Ask to replay.
            //handle choice}

            Console.WriteLine("Game board Positions:");
            Program.PrintGameBoard(validPositions);
            Console.WriteLine("Game board status:");
            Program.PrintGameBoard(gameState);

        }

        static void DrawLine()
        // Draw a new flat line in the Console.
        {
            Console.WriteLine("-------------");
        }

        static void DrawChars(string a, string b, string c)
        // Draw a single line of played characters from the current game state
        // See more about string interpolation https://www.c-sharpcorner.com/article/understanding-string-interpolation-in-c-sharp/#:~:text=C%23%20string%20interpolation%20is%20used%20to%20format%20and,strings.%20This%20feature%20was%20introduced%20in%20C%23%206.
        {
            Console.WriteLine($"| {a} | {b} | {c} |");
        }

        static void PrintGameBoard(string[,] currentState)
        // Print the current game state to the console
        {
            Program.DrawLine();
            for (int i = 0; i < 3; i++)
            {
                Program.DrawChars(currentState[i, 0], currentState[i, 1], currentState[i, 2]);
                Program.DrawLine();
            }
        }

        static void UpdateTile(string tile)
        // Update the chosen tile with the current player
        // switch current player for the next turn
        {
            switch (tile)
            {
                case "1":
                    gameState[0, 0] = currentPlayer;
                    break;
            }

            // switch between players for each turn
            if (currentPlayer == "X") { currentPlayer = "O"; }
            else if (currentPlayer == "O") { currentPlayer = "X"; }
        }

        static void ResetGame()
        // Return game to it's initial state
        {
            // wtf???????
            Console.WriteLine("Resetting game...");
            currentPlayer = " ";

            for (int i = 1; i < 10; i++)
            {
                // concatenate empty string with i to pass string into method.
                Program.UpdateTile("" + i);
            }

            currentPlayer = "X";
        }
    }
}
