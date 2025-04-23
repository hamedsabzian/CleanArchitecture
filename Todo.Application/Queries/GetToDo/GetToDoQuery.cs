using Todo.Application.Abstraction.Dtos;

namespace Todo.Application.Queries.GetToDo;

public record GetToDoQuery(Guid Id) : IRequest<Response<GetToDoDto>>;
