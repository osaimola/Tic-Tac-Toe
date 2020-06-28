using System;

namespace CSharpAssignment
{
    class Program
    {
        static string currentPlayer = "X";
        static string tilePosition = "";
        static int playCount;

        static string gameWinner;
        static string[,] gameState = { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } };
        static string[,] validPositions = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", "9" } };
        static void Main(string[] args)
        {
            string letsPlay = "Y";
            while (letsPlay == "Y")
            {
                // switch between HUMAN v HUMAN and HUMAN v MACHINE mode here.
                Console.WriteLine("\n\nChose a Game Mode \n[1] Human vs Human \n[2] Human vs Computer");
                string userChoice = Console.ReadLine();
                while (userChoice != "1" && userChoice != "2")
                {
                    Console.WriteLine("Please Enter a Valid Choice [ 1 or 2 ]");
                    userChoice = Console.ReadLine();
                }
                Program.PlayGame(userChoice);

                Console.Clear();
                Console.WriteLine("Game board status:");
                Program.PrintGameBoard(gameState);
                Console.WriteLine($"!!!Game over!!!\nResult: {gameWinner}");

                // Reset game and ask play if they want to play again
                Program.ResetGame();
                letsPlay = "";
                while (letsPlay != "Y" && letsPlay != "N")
                {
                    Console.WriteLine("Would you like to play again? [Enter Y or N]");
                    letsPlay = Console.ReadLine().ToUpper();
                }
            }

        }

        static void PlayGame(string gameMode)
        //begin a new game
        // TODO: consider taking a user input to determine if single player vs AI or multiplayer
        {

            // set playcount to 0 for each new game. if 9 turns are played without a winner then game is a draw
            playCount = 0;
            while (!IsGameEnded(Program.gameState))
            // run a player turn as long as the game is not ended
            {

                Console.Clear(); // only clears the visible portion of the console...https://docs.microsoft.com/en-us/dotnet/api/system.console.clear?view=netcore-3.1


                Console.WriteLine("Game board Positions:");
                Program.PrintGameBoard(validPositions);
                Console.WriteLine("Game board status:");
                Program.PrintGameBoard(gameState);

                //EXPERIMENTAL CODE
                if (gameMode == "2" && currentPlayer == "O")
                {
                    // get best play and update tile
                    (int a, int x, int y) = Program.AlphaBetaPlay(1, -2, 2);
                    Program.UpdateTile(validPositions[x, y]);

                }
                //END EXPERIMENTAL CODE
                // only run this for human turns
                else
                {
                    Console.WriteLine($"Player {currentPlayer}, please enter a square number to place your token in...");

                    // if user input between 1-9 and tile is empty, then update the tile. else ask for input again
                    tilePosition = Console.ReadLine();
                    while (!Program.IsValidChoice(tilePosition) || !Program.IsValidTile(tilePosition))
                    {
                        Console.WriteLine("Unavailable or invalid square choice.\nPlease enter a valid square number [1 through 9]");
                        tilePosition = Console.ReadLine();
                    }
                    Program.UpdateTile(tilePosition);
                }
            }

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

            playCount++;
        }


        static bool IsValidTile(string tile)
        // Check if the selected tile is allowed for play (only empty tiles are valid)
        // return true if selected tile is valid
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
        // return false if input is not valid
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

        static bool IsGameEnded(string[,] currentState)
        // checks if game is ended (optionally updates winner)
        // returns true if game should end
        {
            // diagonal wins
            if ((currentState[0, 0] != " " && currentState[0, 0] == currentState[1, 1] && currentState[1, 1] == currentState[2, 2])
                 ||
                 (currentState[0, 2] != " " && currentState[0, 2] == currentState[1, 1] && currentState[1, 1] == currentState[2, 0]))
            {
                gameWinner = currentState[1, 1];
                // set current winner to the position 5 tile as that tile will have winner regardless of which direction diagonal is in.
                return true;
            }

            // loop through to check for wins in columns and rows
            for (int i = 0; i < 3; i++)
            {
                // horizontal wins
                if ((currentState[i, 0] != " ") && (currentState[i, 0] == currentState[i, 1]) && (currentState[i, 1] == currentState[i, 2]))
                {
                    gameWinner = currentState[i, 0];
                    return true;
                }

                // vertical wins
                else if ((currentState[0, i] != " ") && (currentState[0, i] == currentState[1, i]) && (currentState[1, i] == currentState[2, i]))
                {
                    gameWinner = currentState[0, i];
                    return true;
                }
            }

            // if no wins detected and game board is full 
            if (playCount > 8)
            {
                gameWinner = "Draw";
                return true;
            }

            // handle checking for draws when our Alpha Beta Scenario is running
            bool isDraw = true;
            for (int i = 0; i < 3; i++)
            {
                // if any tiles are empty, if statement becomes true and isDraw is set as false.
                if (currentState[i, 0] == " " || currentState[i, 1] == " " || currentState[i, 2] == " ")
                {
                    isDraw = false;
                }
            }

            if (isDraw)
            {
                gameWinner = "Draw";
                return true;
            }

            // returns false if no wins or end conditions detected
            return false;
        }

        static void ResetGame()
        // Return game to its initial state
        {
            Console.WriteLine("\n\nResetting game...");

            currentPlayer = " ";

            for (int i = 1; i < 10; i++)
            {
                // concatenate empty string with i to pass string into method.
                Program.UpdateTile("" + i);
            }

            currentPlayer = "X";
        }

        static (int, int, int) AlphaBetaPlay(int mx, int alpha, int beta)
        // this method takes the current game state and uses the Alpha Beta strategy (Watch this video https://youtu.be/STjW3eH0Cik?t=1503)
        // to determine the best course of action for AI
        // returns a tuple (scenario outcone, row, column)
        {
            /* mx is a multiplier. We will use it to control if our alpha-beta is optimizing for a max value
             or optimizing for a minimum value (one player desires max while the other desires min)
             That way we can set mx to -1 for player X and to 1 for player O, for example
             --- Possible outcomes for a player desiring max:  1 => win, 0 => tie, -1 => loss ---

             we begin by setting optimalValue to worse than the worst case.
             2 for a player that desires minimum
             -2 for a player that desires maximum
             */
            int optimalValue = (-2 * mx);
            int xPos = -1; // row position in our 2d array
            int yPos = -1; // column position in our 2d array
            int m;
            int optimalI;
            int optimalJ;

            string result = "";
            if (Program.IsGameEnded(gameState)) { result = gameWinner; }


            // if game is ended, return to escape recursive loop
            if (result == "X") { return (-1, 0, 0); }
            else if (result == "O") { return (1, 0, 0); }
            else if (result == "Draw") { return (0, 0, 0); }

            // we can loop through all possible tiles in the game.
            // we will only explore valid play tiles to find the best game scenarios
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // if this tile is empty, it is valid. lets explore this option!
                    if (gameState[i, j] == " ")
                    {
                        // we can tell whose turn it is based on if they are optimizing for a max or a min, and play accordingly
                        if (mx < 0) { gameState[i, j] = "X"; }
                        if (mx > 0) { gameState[i, j] = "O"; }

                        // after playing a turn, we recursively call AlphaBetaPlay with a negative mx so we can continue exploring what the 
                        // outcome of this choice will be
                        (m, optimalI, optimalJ) = Program.AlphaBetaPlay(-mx, alpha, beta);

                        // we will only update our optimalValue (initialized as worst case scenario) and row, column indices on 2 conditions
                        // 1. we are maximizing and the returned m is greater than optimalValue
                        // 2. we are minimizing and the rturned m is less than the optimalValue
                        if ((mx == 1 && m > optimalValue) || (mx == -1 && m < optimalValue))
                        {
                            optimalValue = m;
                            xPos = i;
                            yPos = j;
                        }
                        // lets reset the tile so human doesn't notice what we are doing. shhhh!
                        gameState[i, j] = " ";

                        // now for the alpha beta magic.
                        // if the scenario we are exploring is worse than the outcome we want / the best outcome we have seen elsewhere, lets stop exploring it entirely
                        // if not, if this scenario looks good then lets update the alpha/beta value so we know we saw something good
                        if ((mx == 1 && optimalValue >= beta) || (mx == -1 && optimalValue <= alpha))
                        {
                            return (optimalValue, xPos, yPos);
                        }

                        if (mx == 1 && optimalValue > alpha) { alpha = optimalValue; }
                        if (mx == -1 && optimalValue < beta) { beta = optimalValue; }
                    } // end if check for valid tiles
                } // end for looping through columns (j values)
            } // end for looping through rows (i values)

            return (optimalValue, xPos, yPos);

        }
    }
}
