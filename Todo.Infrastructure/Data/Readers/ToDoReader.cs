using Microsoft.EntityFrameworkCore;
using Todo.Application.Shared.Dtos;
using Todo.Application.Shared.Interfaces;

namespace Todo.Infrastructure.Data.Readers;

public class ToDoReader(ToDoDbContext dbContext) : IToDoReader
{
    public Task<GetToDoDto?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return dbContext.Todos.AsNoTracking()
            .Where(a => a.Id == id)
            .Select(a => new GetToDoDto(a.Id, a.Title, a.Description, a.Status, a.CreatedAt))
            .SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<PaginatedList<GetToDoListDto>> GetListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var totalCount = await dbContext.Todos.CountAsync(cancellationToken);
        var items = await dbContext.Todos.AsNoTracking()
            .OrderBy(a => a.CreatedAt)
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .Select(a => new GetToDoListDto(a.Id, a.Title, a.Status, a.CreatedAt))
            .ToListAsync(cancellationToken: cancellationToken);

        return new PaginatedList<GetToDoListDto>(items, totalCount, pageNumber, pageSize);
    }
}
