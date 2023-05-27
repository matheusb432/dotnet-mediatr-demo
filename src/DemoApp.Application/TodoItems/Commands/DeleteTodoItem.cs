using DemoApp.Application.Common.Exceptions;
using DemoApp.Application.Common.Interfaces;
using DemoApp.Domain.Models;
using DemoApp.Infra.Repositories;
using MediatR;

namespace DemoApp.Application.TodoItems.Commands
{
    public record DeleteTodoItemCommand(int Id) : IRequest;

    public class DeleteTodoItemCommandHandler : IRequestHandler<DeleteTodoItemCommand>
    {
        private readonly ITodoItemRepository _repo;

        public DeleteTodoItemCommandHandler(ITodoItemRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            await _repo.DeleteAsync(entity);
        }
    }
}
