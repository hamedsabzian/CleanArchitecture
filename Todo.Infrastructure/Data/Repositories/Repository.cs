using Todo.Application.Shared.Interfaces;
using Todo.Domain.Interfaces;

namespace Todo.Infrastructure.Data.Repositories;

public class Repository<T>(ToDoDbContext dbContext) : IRepository<T> where T : class
{
    public ValueTask<T?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return dbContext.Set<T>().FindAsync(id, cancellationToken);
    }

    public void Add(T entity)
    {
        dbContext.Set<T>().Add(entity);
    }

    public void Update(T entity)
    {
        dbContext.Set<T>().Update(entity);
    }

    public void Delete(T entity)
    {
        dbContext.Set<T>().Remove(entity);
    }
}
