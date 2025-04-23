using Todo.Application.Abstraction.Dtos;
using Todo.Application.Abstraction.Interfaces;

namespace Todo.Application.Queries.GetToDoList;

public class GetToDoListQueryHandler(IToDoReader reader) : IRequestHandler<GetToDoListQuery, Response<PaginatedList<GetToDoListDto>>>
{
    public async ValueTask<Response<PaginatedList<GetToDoListDto>>> Handle(GetToDoListQuery request, CancellationToken cancellationToken)
    {
        var result = await reader.GetList(request.PageNumber, request.PageSize, cancellationToken);
        return new Response<PaginatedList<GetToDoListDto>>().Success(result);
    }
}
