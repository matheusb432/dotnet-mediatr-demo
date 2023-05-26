using AutoMapper;
using DemoApp.Infra.Repositories;
using MediatR;

namespace DemoApp.Application.TodoLists.Queries
{
    public class GetTodoListsQuery : IRequest<IQueryable<TodoListDto>> { }

    public sealed class GetTodoItemsCommandHandler
        : IRequestHandler<GetTodoListsQuery, IQueryable<TodoListDto>>
    {
        private readonly ITodoListRepository _repo;
        private readonly IMapper _mapper;

        public GetTodoItemsCommandHandler(ITodoListRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public Task<IQueryable<TodoListDto>> Handle(
            GetTodoListsQuery request,
            CancellationToken cancellationToken
        )
        {
            var todoLists = _mapper.ProjectTo<TodoListDto>(_repo.Query());
            return Task.FromResult(todoLists);
        }
    }
}
