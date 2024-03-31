using System;
using System.Text.RegularExpressions;

namespace Trivia;

public class PlayGame
{
    public void Main(string[] args)
    {
        Console.WriteLine("*** Welcome to Trivia Game ***\n");
        Console.WriteLine("Enter number of players: 1-4");
        int playerCount = int.Parse(Console.ReadLine());
        if (playerCount < 1 || playerCount > 4) throw new ArgumentException("No player 1..4");
        Console.WriteLine("Reading names for " + playerCount + " players:");

        IGame aGame = new Game();

        for (int i = 1; i <= playerCount; i++)
        {
            Console.WriteLine("Player " + i + " name: ");
            string playerName = Console.ReadLine();
            aGame.Add(playerName);
        }

        Console.WriteLine("\n\n--Starting game--");


        bool notAWinner;
        do
        {
            var roll = ReadRoll();
            aGame.Roll(roll);

            Console.WriteLine(">> Was the answer correct? [y/n] ");
            var correct = ReadYesNo();
            if (correct)
            {
                notAWinner = aGame.WasCorrectlyAnswered();
            }
            else
            {
                notAWinner = aGame.WrongAnswer();
            }

        } while (notAWinner);
        Console.WriteLine(">> Game won!");
    }

    private static bool ReadYesNo()
    {
        string yn = Console.ReadLine()?.ToUpper();
        if (!yn.Contains("[YN]"))
        {
            Console.WriteLine("y or n please");
            return ReadYesNo();
        }
        return yn.Equals("Y", StringComparison.CurrentCultureIgnoreCase);
    }

    private static int ReadRoll()
    {
        Console.WriteLine(">> Throw a die and input roll, or [ENTER] to generate a random roll: ");
        string rollStr = Console.ReadLine()?.Trim();
        int roll;
        if (string.IsNullOrEmpty(rollStr))
        {
            roll = new Random().Next(6) + 1;
            Console.WriteLine(">> Random roll: " + roll);
            return roll;
        }

        if (!Regex.IsMatch(rollStr, "\\d+"))
        {
            Console.WriteLine("Not a number: '" + rollStr + "'");
            return ReadRoll();
        }
        roll = int.Parse(rollStr);
        if (roll < 1 || roll > 6)
        {
            Console.WriteLine("Invalid roll");
            return ReadRoll();
        }
        return roll;
    }
}