using Todo.Application.Shared.Dtos;
using Todo.Application.Shared.Interfaces;

namespace Todo.Application.Queries.GetToDoList;

internal class GetToDoListQueryHandler(IToDoReader reader) : IRequestHandler<GetToDoListQuery, Response<PaginatedList<GetToDoListDto>>>
{
    public async ValueTask<Response<PaginatedList<GetToDoListDto>>> Handle(GetToDoListQuery request, CancellationToken cancellationToken)
    {
        var result = await reader.GetListAsync(request.PageNumber, request.PageSize, cancellationToken);
        return new Response<PaginatedList<GetToDoListDto>>().Success(result);
    }
}
