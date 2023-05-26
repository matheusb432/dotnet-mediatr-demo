using AutoMapper;
using DemoApp.Application.Common.ViewModels;
using DemoApp.Domain.Models;
using DemoApp.Infra.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Application.TodoItems.Commands
{
    public record CreateTodoItemCommand : IRequest<PostReturnViewModel>
    {
        public int ListId { get; init; }

        public string Title { get; init; } = string.Empty;
    }

    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        private readonly ITodoItemRepository _repo;

        public CreateTodoItemCommandValidator(ITodoItemRepository repo)
        {
            _repo = repo;
            RuleFor(v => v.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MaximumLength(200)
                .WithMessage("Title must not exceed 200 characters")
                .MustAsync(BeUniqueTitle)
                .WithMessage("The title already exists");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return !await _repo
                .Query()
                .Where(x => !string.IsNullOrEmpty(x.Title))
                .AnyAsync(x => x.Title.ToLower() == title.ToLower());
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
            var entity = _mapper.Map<TodoItem>(request);

            await _repo.InsertAsync(entity);

            return new PostReturnViewModel(entity.Id);
        }
    }
}
