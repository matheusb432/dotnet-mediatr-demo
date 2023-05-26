using AutoMapper;
using DemoApp.Application.Base;
using DemoApp.Infra.Repositories;
using MediatR;

namespace DemoApp.Application.TodoLists.Queries
{
    public class GetTodoListsQuery : IRequest<IQueryable<TodoListDto>> { }

    public sealed class GetTodoListsQueryHandler
        : BaseRequestHandler,
            IRequestHandler<GetTodoListsQuery, IQueryable<TodoListDto>>
    {
        private readonly ITodoListRepository _repo;

        public GetTodoListsQueryHandler(ITodoListRepository repo, IMapper mapper) : base(mapper)
        {
            _repo = repo;
        }

        public Task<IQueryable<TodoListDto>> Handle(
            GetTodoListsQuery request,
            CancellationToken cancellationToken
        )
        {
            var todoLists = Mapper.ProjectTo<TodoListDto>(_repo.Query());
            return Task.FromResult(todoLists);
        }
    }
}
