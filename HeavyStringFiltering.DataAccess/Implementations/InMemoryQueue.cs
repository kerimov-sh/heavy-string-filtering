using System.Collections.Concurrent;

namespace HeavyStringFiltering.DataAccess.Implementations;

public class InMemoryQueue<T> : IQueue<T>
{
    private readonly ConcurrentQueue<T> _queue = new();

    public void Enqueue(T item)
    {
        _queue.Enqueue(item);
    }

    public bool TryDequeue(out T? item)
    {
        return _queue.TryDequeue(out item);
    }
}