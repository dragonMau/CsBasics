using System.Text;

class Program
{
    static void Main()
    {
        Game game = new();
        game.Play();
    }
}


class Game
{
    private static string[] disks = ["         ", "    #    ", "   ###   ", "  #####  ", " ####### "];

    Pile<int>[] poles = [new([4, 3, 2, 1]), new(4), new(4)];

    static int[,] moveOrder = { { 0, 1 }, { 0, 2 }, { 1, 0 }, { 1, 2 }, { 2, 0 }, { 2, 1 } };
    bool[] moves = [false, false, false, false, false, false];

    const int w = 32;
    const int h = 6;
    const int s = 10;
    StringBuilder field = new(w * h);
    public void Play()
    {
        string? input;
        int move;

        while (true)
        {
            updateField();
            updateMoves();

            Console.Write(field.ToString());
            Console.WriteLine("moves:");

            for (int i = 0; i < 6; i++)
            {
                if (moves[i])
                {
                    Console.WriteLine("    " + i + ": " + moveOrder[i, 0] + " to " + moveOrder[i, 1] + ".");
                }
            }

            Console.Write("Your move: ");
            while ((input = Console.ReadLine()) == null || !int.TryParse(input, out move) || !moves.ElementAtOrDefault(move))
            {
                Console.Write("move '" + input + "' is not valid move.\nYour move: ");
            }

            poles[moveOrder[move, 1]].Push(
                poles[moveOrder[move, 0]].Pop()
            );

            Console.WriteLine();
        }
    }

    bool isMove(int poleA, int poleB) // pole indexes
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
    void updateMoves()
    {
        for (int i = 0; i < 6; i++)
        {
            moves[i] = isMove(moveOrder[i, 0], moveOrder[i, 1]);
        }
    }
    void updateField()
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

class Pile<T>
{
    private T[] _array;
    private int _height;
    public Pile()
    {
        _array = [];
    }

    // Create a pile with a specific initial capacity.  The initial capacity
    // must be a non-negative number.
    public Pile(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);
        _array = new T[capacity];
        _height = 0;
    }

    // Fills a Pile with the contents of a particular collection.
    public Pile(IEnumerable<T> collection)
    {
        ArgumentNullException.ThrowIfNull(collection);
        _array = collection.ToArray();
        _height = _array.Length;
    }

    public Pile(IEnumerable<T> collection, int capacity)
    {
        ArgumentNullException.ThrowIfNull(collection);
        _array = collection.ToArray();
        if (capacity < _array.Length) throw new ArgumentOutOfRangeException();
        Array.Resize(ref _array, capacity);
    }

    // Returns the top object on the pile without removing it.  If the pile
    // is empty, returns default
    public T? Get()
    {
        return Get(_height-1);
    }

    // Returns object at index from the pile without removing it.  If the pile
    // is index is out of bounds or pile is empty, returns default
    public T? Get(int index)
    {
        if (index < 0) index = _array.Length + index;
        if (index < 0 || index >= _array.Length) return default;
        else return _array[index];
    }

    public T Pop()
    {
        T item = _array[_height - 1];
        _array[_height - 1] = default!;
        --_height;
        return item;
    }
    public void Push(T item)
    {
        _array[_height] = item;
        ++_height;
    }
    public T[] Values { get { return _array; } }
}