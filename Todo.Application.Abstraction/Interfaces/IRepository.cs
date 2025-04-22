namespace Todo.Application.Abstraction.Interfaces;

public interface IRepository<T>
{
    ValueTask<T?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
