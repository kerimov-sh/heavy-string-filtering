namespace HeavyStringFiltering.DataAccess;

public interface IQueue<T>
{
    void Enqueue(T item);

    bool TryDequeue(out T? item);
}