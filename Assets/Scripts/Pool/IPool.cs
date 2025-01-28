public interface IPool<T>
{
    void Add(T item);
    bool TryGet(out T item);
}