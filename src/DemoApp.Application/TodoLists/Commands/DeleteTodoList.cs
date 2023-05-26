using DemoApp.Application.Common.Exceptions;
using DemoApp.Domain.Models;
using DemoApp.Infra.Repositories;
using MediatR;

namespace DemoApp.Application.TodoLists.Commands
{
    public record DeleteTodoListCommand(int Id) : IRequest;

    public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand>
    {
        private readonly ITodoListRepository _repo;

        public DeleteTodoListCommandHandler(ITodoListRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoList), request.Id);
            }

            await _repo.DeleteAsync(entity);
        }
    }
}
