using AutoMapper;
using DemoApp.Application.Common.ViewModels;
using DemoApp.Domain.Models;
using DemoApp.Infra.Repositories;
using FluentValidation;
using MediatR;

namespace DemoApp.Application.TodoItems.Commands
{
    public record CreateTodoItemCommand : IRequest<PostReturnViewModel>
    {
        public CreateTodoItemCommand(TodoItemCreateDto todoItem)
        {
            TodoItem = todoItem;
        }

        public TodoItemCreateDto TodoItem { get; set; } = new();
    }

    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        public CreateTodoItemCommandValidator(IValidator<TodoItemCreateDto> validator)
        {
            RuleFor(x => x.TodoItem).SetValidator(validator);
            RuleFor(x => x.TodoItem.TodoListId).NotEmpty();
        }
    }

    public class CreateTodoItemCommandHandler
        : IRequestHandler<CreateTodoItemCommand, PostReturnViewModel>
    {
        private readonly ITodoItemRepository _repo;
        private readonly IMapper _mapper;

        public CreateTodoItemCommandHandler(ITodoItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PostReturnViewModel> Handle(
            CreateTodoItemCommand request,
            CancellationToken cancellationToken
        )
        {
            var entity = _mapper.Map<TodoItem>(request.TodoItem);

            await _repo.InsertAsync(entity);

            return new PostReturnViewModel(entity.Id);
        }
    }
}
