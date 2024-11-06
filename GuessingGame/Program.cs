using System;

class GuessingGame
{
    public static void Main(string[] args)
    {
        Random rand = new Random();
        int number, attempts, guess;
        char playingResponse;
        bool playing = true;
        bool valid;
        int min = 1;
        int max = 100;

        while (playing)
        {
            number = rand.Next(min, max + 1);
            attempts = 0;
            guess = 0;

            Console.WriteLine("Guess random number between " + min + " - " + max + ":");

            do
            {
                do
                {
                    Console.Write("> ");
                    valid = int.TryParse(Console.ReadLine(), out guess);
                } while (!valid);
                attempts++;
                if (guess > number)
                {
                    Console.WriteLine("" + guess + " is too high!");
                }
                else if (guess < number)
                {
                    Console.WriteLine("" + guess + " is too low!");
                }
            } while (guess != number);

            Console.WriteLine("You guessed number number " + number + " in " + attempts + " attempts!!!");
            Console.Write("\nPlay again? (Y/n): ");
            playingResponse = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            if (!"yY \r\n".Contains(playingResponse))
            {
                playing = false;
                Console.WriteLine("Have a nice day!");
            }
        }
    }
}
