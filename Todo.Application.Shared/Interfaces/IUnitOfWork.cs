using Todo.Domain.Interfaces;

namespace Todo.Application.Shared.Interfaces;

internal interface IUnitOfWork
{
    IRepository<T> Repository<T>() where T : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
