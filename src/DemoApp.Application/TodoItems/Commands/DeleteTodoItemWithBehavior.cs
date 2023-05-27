using DemoApp.Application.Common.Interfaces;
using DemoApp.Infra.Repositories;
using MediatR;

namespace DemoApp.Application.TodoItems.Commands
{
    /// <summary>
    /// Implementation of DeleteTodoItemCommand with ExistsBehavior
    /// Since this request implements the IWithId interface, ExistsBehavior will be applied
    ///
    /// This is NOT good practice, it's more to just demonstrate the capability of what can be done
    /// </summary>
    public record DeleteTodoItemWithBehaviorCommand(int Id) : IRequest, IWithId;

    public class DeleteTodoItemWithBehaviorCommandHandler
        : IRequestHandler<DeleteTodoItemWithBehaviorCommand>
    {
        private readonly ITodoItemRepository _repo;

        public DeleteTodoItemWithBehaviorCommandHandler(ITodoItemRepository repo)
        {
            _repo = repo;
        }

        public async Task Handle(
            DeleteTodoItemWithBehaviorCommand request,
            CancellationToken cancellationToken
        )
        {
            await _repo.DeleteAsync((await _repo.GetByIdAsync(request.Id))!);
        }
    }
}
