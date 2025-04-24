using Todo.Application.Shared.Dtos;

namespace Todo.Application.Shared.Interfaces;

public interface IToDoReader
{
    Task<GetToDoDto?> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<PaginatedList<GetToDoListDto>> GetListAsync(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
