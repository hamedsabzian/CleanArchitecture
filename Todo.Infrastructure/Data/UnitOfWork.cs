using Todo.Application.Shared.Interfaces;
using Todo.Domain.Interfaces;
using Todo.Infrastructure.Data.Repositories;

namespace Todo.Infrastructure.Data;

public sealed class UnitOfWork(ToDoDbContext dbContext) : IUnitOfWork
{
    public IRepository<T> Repository<T>() where T : class
    {
        return new Repository<T>(dbContext);
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
