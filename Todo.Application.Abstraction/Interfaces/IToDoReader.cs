using Todo.Application.Abstraction.Dtos;

namespace Todo.Application.Abstraction.Interfaces;

public interface IToDoReader
{
    Task<GetToDoDto?> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<PaginatedList<GetToDoListDto>> GetListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
