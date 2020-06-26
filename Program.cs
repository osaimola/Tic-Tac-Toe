using System;

namespace CSharpAssignment
{
    class Program
    {
        static string[,] gameState = { { " ", " ", " " }, { " ", " ", " " }, { " ", " ", " " } };
        static void Main(string[] args)
        {
            // draw a sample game line
            Program.PrintGameState(gameState);
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

        static void PrintGameState(string[,] currentState)
        // Print the current game state to the console
        {
            Program.DrawLine();
            for (int i = 0; i < 3; i++)
            {
                Program.DrawChars(currentState[i, 0], currentState[i, 1], currentState[i, 2]);
                Program.DrawLine();
            }
        }
    }
}
