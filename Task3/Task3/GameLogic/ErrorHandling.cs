using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class ErrorHandling
    {
        public bool ValidateMoves(List<string> moves)
        {
            if (moves.Count < 3 || moves.Count % 2 == 0)
            {
                Console.WriteLine("Error: Provide an odd number (≥3) of non-repeating moves.");
                Console.WriteLine("Example: Rock Paper Scissors");
                return false;
            }

            if (new HashSet<string>(moves).Count != moves.Count)
            {
                Console.WriteLine("Error: All moves must be distinct.");
                return false;
            }

            return true;
        }
    }

}

