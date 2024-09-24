using ConsoleTables;
using System;
using System.Collections.Generic;

namespace GameLogic
{
    public class HelpTable
    {
        public void GenerateHelpTable(List<string> moves)
        {
            var table = new ConsoleTable(new[] { "v PC\\User >" }.Concat(moves).ToArray());

            for (int i = 0; i < moves.Count; i++)
            {
                var row = new List<string> { moves[i] };

                for (int j = 0; j < moves.Count; j++)
                {
                    if (i == j)
                    {
                        row.Add("Draw");
                    }
                    else if ((j - i + moves.Count) % moves.Count <= moves.Count / 2)
                    {
                        row.Add("Win");
                    }
                    else
                    {
                        row.Add("Lose");
                    }
                }

                table.AddRow(row.ToArray());
            }
            table.Write(Format.Alternative);
        }
    }
}

