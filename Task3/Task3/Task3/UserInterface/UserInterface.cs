using System;
using System.Collections.Generic;

namespace UserInterface
{
    public class UserInterface
    {
        
        public void DisplayMenu(List<string> moves)
        {
            Console.WriteLine("Available moves:");
            for (int i = 0; i < moves.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {moves[i]}");
            }
            Console.WriteLine("0 - exit");
            Console.WriteLine("? - help");
        }

        
        public int GetUserInput(int maxOption)
        {
            Console.Write("Enter your move (or ? for help): ");
            string input = Console.ReadLine();

            
            if (input == "?")
            {
                return -1;  
            }

            
            if (int.TryParse(input, out int userChoice) && userChoice >= 0 && userChoice <= maxOption)
            {
                return userChoice;
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
                return -2;  
            }
        }

        
        public void DisplayResult(string result)
        {
            Console.WriteLine(result);
        }
    }
}
