using DemoApp.Application.Common.Exceptions;
using DemoApp.Application.Common.Interfaces;
using DemoApp.Application.TodoItems.Commands;
using DemoApp.Domain.Models;
using DemoApp.Infra.Repositories;
using MediatR;

namespace DemoApp.Application.Common.Behaviors
{
    /// <summary>
    /// This pipeline behavior generically checks if an entity exists in the database based on the requestTypeMap dictionary.
    ///
    /// This is NOT good practice, resolving dependencies at runtime is less performant than constructor injection,
    /// and the amount of configuration necessary creates a serious complexity overhead for something that should be simple.
    ///
    /// However, it's interesting that this is possible nonetheless.
    /// </summary>
    public sealed class ExistsBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IWithId
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly Dictionary<Type, (Type, string)> _requestTypeMap =
            new()
            {
                {
                    typeof(DeleteTodoItemWithBehaviorCommand),
                    (typeof(ITodoItemRepository), nameof(TodoItem))
                },
            };

        public ExistsBehavior(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken
        )
        {
            if (!_requestTypeMap.ContainsKey(typeof(TRequest)))
                return await next();

            var (repoType, entityTypeName) = _requestTypeMap[typeof(TRequest)];

            if (_serviceProvider.GetService(repoType) is not IRepository<TodoItem> repository)
                return await next();

            var exists = await repository.ExistsAsync(request.Id);

            if (!exists)
                throw new NotFoundException(entityTypeName, request.Id);

            return await next();
        }
    }
}
