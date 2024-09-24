namespace GameLogic
{
    public class GameRules
    {
        public string DetermineWinner(int playerMove, int computerMove, int totalMoves)
        {
            int half = totalMoves / 2;
            int result = (playerMove - computerMove + totalMoves) % totalMoves;

            if (result == 0)
            {
                return "It's a draw!";
            }
            else if (result <= half)
            {
                return "You win!";
            }
            else
            {
                return "You lose!";
            }
        }
    }
}

