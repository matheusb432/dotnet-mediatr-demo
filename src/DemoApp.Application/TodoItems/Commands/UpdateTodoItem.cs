using AutoMapper;
using DemoApp.Application.Common.Exceptions;
using DemoApp.Domain.Enums;
using DemoApp.Domain.Models;
using DemoApp.Infra.Repositories;
using FluentValidation;
using MediatR;

namespace DemoApp.Application.TodoItems.Commands
{
    public record UpdateTodoItemCommand : IRequest
    {
        public int Id { get; init; }

        public int ListId { get; init; }

        public PriorityLevel Priority { get; init; }

        public string? Note { get; init; }
    }

    public sealed class UpdateTodoItemCommandValidator : AbstractValidator<UpdateTodoItemCommand>
    {
        public UpdateTodoItemCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.ListId).NotEmpty();
        }
    }

    public sealed class UpdateTodoItemCommandHandler : IRequestHandler<UpdateTodoItemCommand>
    {
        // TODO to base injector?
        private readonly ITodoItemRepository _repo;
        private readonly IMapper _mapper;

        public UpdateTodoItemCommandHandler(ITodoItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task Handle(UpdateTodoItemCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repo.GetByIdAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(TodoItem), request.Id);
            }

            entity.ListId = request.ListId;
            entity.Note = request.Note;
            entity.Priority = request.Priority;

            await _repo.ModifyAsync(entity);
        }
    }
}
