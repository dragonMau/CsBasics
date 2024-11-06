using System;

class Program
{
    public static void Main()
    {
        Random rand = new();
        bool running = true;
        int computerMove;
        int userMove;
        int result;
        char move;
        String? response;
        char cResponse;


        Console.WriteLine("RockPaperScissors\n");

        while (running)
        {
            userMove = -1;

            while (userMove == -1)
            {
                Console.Write("\rChoose your move: ");
                response = Console.ReadLine();
                if (response == null || response.Length == 0) { response = "null"; }
                move = response.ToLower()[0];
                userMove = move switch
                {
                    'r' => 0,
                    'p' => 1,
                    's' => 2,
                    _ => -1
                };
            }
            Console.WriteLine("You chose " + ToMove(userMove) + ".");
            computerMove = rand.Next(0, 3);
            Console.WriteLine("Computer chose " + ToMove(computerMove) + ".");
            result = (3 + userMove - computerMove) % 3;
            Console.WriteLine(result switch
            {
                0 => "It's a tie!",
                1 => ToMove(userMove) + " beats " + ToMove(computerMove) + ". You Win!",
                2 => ToMove(computerMove) + " beats " + ToMove(userMove) + ". You Lost!",
                _ => "Infinity"
            });

            Console.Write("\nDo you want to continue? (Y/n)");
            cResponse = Console.ReadKey().KeyChar;
            if (!"yY \r\n".Contains(cResponse))
            {
                running = false;
                Console.WriteLine("\nHave a nice day!");
            }
            else
            {
                Console.Write("\r                                ");
            }
        }

    }

    public static String ToMove(int move) => move switch
    {
        0 => "Rock",
        1 => "Paper",
        2 => "Scissors",
        _ => "Infinity"
    };
}


