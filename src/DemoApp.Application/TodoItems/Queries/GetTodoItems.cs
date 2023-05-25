using AutoMapper;
using DemoApp.Infra.Repositories;
using MediatR;

namespace DemoApp.Application.TodoItems.Queries
{
    public class GetTodoItemsQuery : IRequest<IQueryable<TodoItemDto>> { }

    public sealed class GetTodoItemsCommandHandler
        : IRequestHandler<GetTodoItemsQuery, IQueryable<TodoItemDto>>
    {
        private readonly ITodoItemRepository _repo;
        private readonly IMapper _mapper;

        public GetTodoItemsCommandHandler(ITodoItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<IQueryable<TodoItemDto>> Handle(
            GetTodoItemsQuery request,
            CancellationToken cancellationToken
        )
        {
            var todoItems = _mapper.ProjectTo<TodoItemDto>(_repo.Query());
            return Task.FromResult(todoItems);
        }
    }
}
