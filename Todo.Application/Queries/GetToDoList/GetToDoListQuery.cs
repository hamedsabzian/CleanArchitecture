using Todo.Application.Abstraction.Dtos;

namespace Todo.Application.Queries.GetToDoList;

public record GetToDoListQuery(int PageNumber = 1, int PageSize = 10) : IRequest<Response<PaginatedList<GetToDoListDto>>>;
