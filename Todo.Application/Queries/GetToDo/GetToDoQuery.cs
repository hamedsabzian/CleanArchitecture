using Todo.Application.Shared.Dtos;

namespace Todo.Application.Queries.GetToDo;

public record GetToDoQuery(Guid Id) : IRequest<Response<GetToDoDto>>;
