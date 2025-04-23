using Microsoft.EntityFrameworkCore;
using Todo.Application.Abstraction.Dtos;
using Todo.Application.Abstraction.Interfaces;

namespace Todo.Infrastructure.Data.Readers;

public class ToDoReader(ToDoDbContext dbContext) : IToDoReader
{
    public Task<GetToDoDto?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return dbContext.Todos.AsNoTracking()
            .Select(a => new GetToDoDto(a.Id, a.Title, a.Description, a.Status, a.CreatedAt))
            .SingleOrDefaultAsync(a => a.Id == id, cancellationToken: cancellationToken);
    }

    public async Task<PaginatedList<GetToDoListDto>> GetList(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var countTask = dbContext.Todos.CountAsync(cancellationToken);
        var listTask = dbContext.Todos.AsNoTracking()
            .OrderBy(a => a.CreatedAt)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .Select(a => new GetToDoListDto(a.Id, a.Title, a.Status, a.CreatedAt))
            .ToListAsync(cancellationToken: cancellationToken);

        await Task.WhenAll([countTask, listTask]);

        return new PaginatedList<GetToDoListDto>(listTask.Result, countTask.Result, pageNumber, pageSize);
    }
}
