using AutoMapper;
using DemoApp.Application.Base;
using DemoApp.Infra.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.TodoLists.Queries
{
    public class GetAllTodoListsQuery : IRequest<IEnumerable<TodoListDto>> { }

    public sealed class GetAllTodoListsCommandHandler
        : BaseRequestHandler,
            IRequestHandler<GetAllTodoListsQuery, IEnumerable<TodoListDto>>
    {
        private readonly ITodoListRepository _repo;

        public GetAllTodoListsCommandHandler(ITodoListRepository repo, IMapper mapper)
            : base(mapper)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<TodoListDto>> Handle(
            GetAllTodoListsQuery request,
            CancellationToken cancellationToken
        )
        {
            var items = await _repo.GetAllAsync();
            return Mapper.Map<IEnumerable<TodoListDto>>(items);
        }
    }
}
