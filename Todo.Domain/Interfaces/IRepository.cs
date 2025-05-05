namespace Todo.Domain.Interfaces;

internal interface IRepository<T>
{
    ValueTask<T?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
