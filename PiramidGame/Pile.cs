class Pile<T>
{
    private readonly T[] _array;
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
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, _array.Length);
        Array.Resize(ref _array, capacity);
    }

    // Returns the top object on the pile without removing it.  If the pile
    // is empty, returns default
    public T? Get()
    {
        return Get(_height - 1);
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