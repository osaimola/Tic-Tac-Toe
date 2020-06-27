using System;

namespace CSharpAssignment
{
    class Program
    {
        static string currentPlayer = "X";
        static string tilePosition = "";
        static string[,] gameState = { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } };
        static string[,] validPositions = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
        static void Main(string[] args)
        {
            //string continueGame;
            //while ((continueGame = (Console.ReadLine()).ToUpper()) != "N")
            //{
            Program.PlayGame();
            //}

        }

        static void PlayGame()
        //begin a new game
        // consider taking a user input to determine if single player vs AI or multiplayer
        {

            Console.Clear(); // only clears the visible portion of the console...https://docs.microsoft.com/en-us/dotnet/api/system.console.clear?view=netcore-3.1

            //TODO: CHECK IF SOMEONE WON OR THE BOARD IS FULL, IF NOT PROCEED WITH BELOW
            // if (GameEnded(gameStatus)) { 
            // display winner.
            //Ask to replay.
            //handle choice}

            Console.WriteLine("Game board Positions:");
            Program.PrintGameBoard(validPositions);
            Console.WriteLine("Game board status:");
            Program.PrintGameBoard(gameState);

            Console.WriteLine($"Player {currentPlayer}, please enter a square number to place your token in...");

            //TODO: if user input between 1-9 and tile is empty, then update the tile. else ask for input again
            tilePosition = Console.ReadLine();
            while (!Program.IsValidChoice(tilePosition) || !Program.IsValidTile(tilePosition))
            {
                Console.WriteLine("Please enter a valid square number");
                tilePosition = Console.ReadLine();
            }

            Program.UpdateTile(tilePosition);
            PrintGameBoard(gameState);

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
                case "2":
                    gameState[0, 1] = currentPlayer;
                    break;
                case "3":
                    gameState[0, 2] = currentPlayer;
                    break;
                case "4":
                    gameState[1, 0] = currentPlayer;
                    break;
                case "5":
                    gameState[1, 1] = currentPlayer;
                    break;
                case "6":
                    gameState[1, 2] = currentPlayer;
                    break;
                case "7":
                    gameState[2, 0] = currentPlayer;
                    break;
                case "8":
                    gameState[2, 1] = currentPlayer;
                    break;
                case "9":
                    gameState[2, 2] = currentPlayer;
                    break;
                default:
                    break;
            }

            // switch between players for each turn
            if (currentPlayer == "X") { currentPlayer = "O"; }
            else if (currentPlayer == "O") { currentPlayer = "X"; }
        }


        static bool IsValidTile(string tile)
        // Check if the selected tile is allowed for play (only empty tiles are valid)
        {
            switch (tile)
            {
                case "1":
                    return ((gameState[0, 0] == " ") ? true : false);
                case "2":
                    return ((gameState[0, 1] == " ") ? true : false);
                case "3":
                    return ((gameState[0, 2] == " ") ? true : false);
                case "4":
                    return ((gameState[1, 0] == " ") ? true : false);
                case "5":
                    return ((gameState[1, 1] == " ") ? true : false);
                case "6":
                    return ((gameState[1, 2] == " ") ? true : false);
                case "7":
                    return ((gameState[2, 0] == " ") ? true : false);
                case "8":
                    return ((gameState[2, 1] == " ") ? true : false);
                case "9":
                    return ((gameState[2, 2] == " ") ? true : false);
                default:
                    return false;
            }
        }

        static bool IsValidChoice(string tile)
        // checks if user has entered a valid square option
        {
            int intTile;
            try
            {
                intTile = Int32.Parse(tile);

            }
            catch (Exception)
            {
                return false;
            }
            return (0 < intTile && intTile < 10);

        }

        static void ResetGame()
        // Return game to it's initial state
        {
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
