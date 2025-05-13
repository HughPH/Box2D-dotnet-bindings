using System.Collections;
using System.Collections.Generic;

namespace Box2D;

class ConcurrentHashSet<T> : IEnumerable<T> where T : notnull
{
    private const int StripeCount = 64; // adjust as needed
    private readonly HashSet<T>[] _sets;
    private readonly object[] _locks;

    public ConcurrentHashSet()
    {
        _sets = new HashSet<T>[StripeCount];
        _locks = new object[StripeCount];
        for (int i = 0; i < StripeCount; i++)
        {
            _sets[i] = new HashSet<T>();
            _locks[i] = new object();
        }
    }

    private int GetStripeIndex(T item)
    {
        return (item.GetHashCode() & 0x7FFFFFFF) % StripeCount;
    }

    public bool Add(T item)
    {
        int index = GetStripeIndex(item);
        lock (_locks[index])
        {
            return _sets[index].Add(item);
        }
    }

    public bool Remove(T item)
    {
        int index = GetStripeIndex(item);
        lock (_locks[index])
        {
            return _sets[index].Remove(item);
        }
    }

    public bool Contains(T item)
    {
        int index = GetStripeIndex(item);
        lock (_locks[index])
        {
            return _sets[index].Contains(item);
        }
    }

    public void Clear()
    {
        for (int i = 0; i < StripeCount; i++)
        {
            lock (_locks[i])
            {
                _sets[i].Clear();
            }
        }
    }

    public int Count
    {
        get
        {
            int total = 0;
            for (int i = 0; i < StripeCount; i++)
            {
                lock (_locks[i])
                {
                    total += _sets[i].Count;
                }
            }
            return total;
        }
    }

    public IEnumerable<T> Items
    {
        get
        {
            for (int i = 0; i < StripeCount; i++)
            {
                lock (_locks[i])
                {
                    foreach (var item in _sets[i])
                        yield return item;
                }
            }
        }
    }

    public IEnumerator<T> GetEnumerator() =>
        Items.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
}