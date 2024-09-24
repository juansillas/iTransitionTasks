using System;
using System.Collections.Generic;
using GameLogic;
using UserInterface;

class Program
{
    static void Main(string[] args)
    {

        if (args.Length < 3 || args.Length % 2 == 0 || args.Distinct().Count() != args.Length)
        {
            Console.WriteLine("Error: Provide an odd number (≥3) of non-repeating moves.");
            Console.WriteLine("Example: Rock Paper Scissors");
            return;
        }


        var keyGenerator = new KeyGenerator();
        var hmacGenerator = new HMACGenerator();
        var gameRules = new GameRules();
        var helpTable = new HelpTable();
        var errorHandling = new ErrorHandling();
        var userInterface = new UserInterface.UserInterface();


        var moves = new List<string>(args);
        if (!errorHandling.ValidateMoves(moves))
        {
            return;
        }


        string secretKey = keyGenerator.GenerateKey();


        Random random = new Random();
        int computerMoveIndex = random.Next(moves.Count);
        string computerMove = moves[computerMoveIndex];


        string hmac = hmacGenerator.GenerateHMAC(computerMove, secretKey);
        Console.WriteLine($"HMAC: {hmac}");


        userInterface.DisplayMenu(moves);

        int playerMoveNumber;


while (true)
{
    playerMoveNumber = userInterface.GetUserInput(moves.Count);

    
    if (playerMoveNumber == -1)
    {
        helpTable.GenerateHelpTable(moves);
        userInterface.DisplayMenu(moves);  
        continue;  
    }

    
    if (playerMoveNumber == -2)
    {
        continue;  
    }

    
    if (playerMoveNumber == 0)
    {
        Console.WriteLine("Goodbye!");
        return;
    }

    
    //break;
    if(playerMoveNumber > 0 && playerMoveNumber <= moves.Count)
            {
               break;
            }
            Console.WriteLine("Invalid Input. Please try again.");
}


        int playerMoveIndex = playerMoveNumber - 1;
        string result = gameRules.DetermineWinner(playerMoveIndex, computerMoveIndex, moves.Count);


        Console.WriteLine($"Your move: {moves[playerMoveIndex]}");
        Console.WriteLine($"Computer move: {computerMove}");
        userInterface.DisplayResult(result);


        Console.WriteLine($"HMAC key: {secretKey}");
    }
}
