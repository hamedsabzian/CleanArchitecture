using Microsoft.EntityFrameworkCore;
using Todo.Application.Abstraction.Dtos;
using Todo.Application.Abstraction.Interfaces;

namespace Todo.Infrastructure.Data.Readers;

public class ToDoReader(ToDoDbContext dbContext) : IToDoReader
{
    public Task<GetToDoDto?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return dbContext.Todos.Select(a => new GetToDoDto(a.Id, a.Title, a.Description, a.Status, a.CreatedAt))
            .SingleOrDefaultAsync(a => a.Id == id, cancellationToken: cancellationToken);
    }

    public Task<PaginatedList<GetToDoListDto>> GetList(int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
