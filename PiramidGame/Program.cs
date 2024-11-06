using System.Text;
/*
Solution:
0
1
3
0
4
5
0
1
3
2
4
3
0
1
3
 */
class Program
{
    static void Main()
    {
        bool playing = true;
        Game game = new();

        while (playing)
        {
            game.Play();

            Console.Write("\nPlay again? (Y/n): ");
            char Response = Console.ReadKey().KeyChar;
            Console.WriteLine("\n");
            if (!"yY \r\n".Contains(Response))
            {
                playing = false;
                Console.WriteLine("Have a nice day!");
            }
        }
    }
}


class Game
{
    private static readonly string[] disks = ["         ", "    #    ", "   ###   ", "  #####  ", " ####### "];
    static readonly int[,] moveOrder = { { 0, 1 }, { 0, 2 }, { 1, 0 }, { 1, 2 }, { 2, 0 }, { 2, 1 } };

    Pile<int>[] poles;
    readonly bool[] availble_moves;
    readonly StringBuilder field;

    const int w = 32;
    const int h = 6;
    const int s = 10;

    public Game()
    {
        poles = [new([4, 3, 2, 1]), new(4), new(4)];
        availble_moves = [false, false, false, false, false, false];
        field = new(w * h);
    }
    public void Play()
    {
        string? input;
        int move;
        int n = 0;
        poles = [new([4, 3, 2, 1]), new(4), new(4)];

        while (!CheckWin())
        {
            UpdateField();
            UpdateMoves();

            Console.Write(field.ToString());
            Console.WriteLine("moves:");

            for (int i = 0; i < 6; i++)
            {
                if (availble_moves[i])
                {
                    Console.WriteLine("    " + i + ": " + moveOrder[i, 0] + " to " + moveOrder[i, 1] + ".");
                }
            }

            Console.Write("Your move: ");
            while ((input = Console.ReadLine()) == null || !int.TryParse(input, out move) || !availble_moves.ElementAtOrDefault(move))
            {
                Console.Write("move '" + input + "' is not valid move.\nYour move: ");
            }

            poles[moveOrder[move, 1]].Push(
                poles[moveOrder[move, 0]].Pop()
            );

            Console.WriteLine();
            n++;
        }
        UpdateField();
        Console.Write(field.ToString());
        Console.WriteLine("You won in " + n + " moves!");
    }

    bool CheckWin()
    {
        return poles[2].Values.SequenceEqual([4, 3, 2, 1]);
    }
    bool IsMove(int poleA, int poleB) // pole indexes
    {
        int diskA = poles[poleA].Get();
        int diskB = poles[poleB].Get();

        if (diskA == 0) { return false; } // can't move from empty pole
        else if (diskB == 0) { return true; }  // now can move to empty pole
        else if (diskA > diskB) { return false; } // can't move on smaller disk
        else if (diskA < diskB) { return true; }  // can move on bigger disk
        else
        {
            Console.Error.WriteLine("Not all cases listed!!! A: '" + diskA + "' B: '" + diskB + "'");
            return false;
        }
    }
    void UpdateMoves()
    {
        for (int i = 0; i < 6; i++)
        {
            availble_moves[i] = IsMove(moveOrder[i, 0], moveOrder[i, 1]);
        }
    }
    void UpdateField()
    {
        /*
         * -------------------------------
         * |0:  #    |1:       |2:       |
         * |   ###   |         |         |
         * |  #####  |         |         |
         * | ####### |         |         |
         * -------------------------------
         */
        field.Clear();
        field.Append(
            "-------------------------------\n" +
            "||||\n" +
            "||||\n" +
            "||||\n" +
            "||||\n" +
            "-------------------------------\n"
         );

        for (int layer = 0; layer < 4; layer++)  // 0123 (3 is base)
        {
            for (int pole = 0; pole < 3; pole++) // 012
            {
                // l = layer + 1    : 1 2 3 4
                // c = s * pole + 1 : 1 11 21
                // i = l * w + c
                int fi = (layer + 1) * w + s * pole + 1;
                field.Insert(fi, disks[poles[pole].Get(3 - layer)]);
            }
        }

        for (int i = 0; i < 3; i++) // add poles numbers
        {
            field[1 * w + i * s + 1] = i.ToString()[0];
            field[1 * w + i * s + 2] = ':';
        }
    }
}