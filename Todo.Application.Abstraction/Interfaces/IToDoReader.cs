using Todo.Application.Abstraction.Dtos;

namespace Todo.Application.Abstraction.Interfaces;

public interface IToDoReader
{
    Task<GetToDoDto?> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<PaginatedList<GetToDoListDto>> GetList(int pageNumber, int pageSize, CancellationToken cancellationToken);
}
