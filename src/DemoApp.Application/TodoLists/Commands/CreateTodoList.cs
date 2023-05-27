using AutoMapper;
using DemoApp.Application.Common.ViewModels;
using DemoApp.Application.TodoItems;
using DemoApp.Domain.Models;
using DemoApp.Infra.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Application.TodoLists.Commands
{
    public record CreateTodoListCommand : IRequest<PostReturnViewModel>
    {
        public string Title { get; init; } = string.Empty;
        public List<TodoItemCreateDto> TodoItems { get; init; } = new();
    }

    public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
    {
        private readonly ITodoListRepository _repo;

        public CreateTodoListCommandValidator(
            ITodoListRepository repo,
            IValidator<TodoItemCreateDto> todoItemValidator
        )
        {
            _repo = repo;

            RuleFor(v => v.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(200)
                .WithMessage("Title must not exceed 200 characters")
                .MustAsync(BeUniqueListTitle)
                .WithMessage("The title already exists");
            RuleFor(v => v.TodoItems)
                .Must(l => l.Count > 0)
                .WithMessage("Must have at least one todo item");
            RuleForEach(v => v.TodoItems).SetValidator(todoItemValidator);
        }

        public async Task<bool> BeUniqueListTitle(string title, CancellationToken cancellationToken)
        {
            return !await _repo
                .Query()
                .Where(x => !string.IsNullOrEmpty(x.Title))
                .AnyAsync(x => x.Title!.ToLower() == title.ToLower(), cancellationToken);
        }
    }

    public class CreateTodoItemCommandHandler
        : IRequestHandler<CreateTodoListCommand, PostReturnViewModel>
    {
        private readonly ITodoListRepository _repo;
        private readonly IMapper _mapper;

        public CreateTodoItemCommandHandler(ITodoListRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<PostReturnViewModel> Handle(
            CreateTodoListCommand request,
            CancellationToken cancellationToken
        )
        {
            var entity = _mapper.Map<TodoList>(request);

            await _repo.InsertAsync(entity);

            return new PostReturnViewModel(entity.Id);
        }
    }
}
