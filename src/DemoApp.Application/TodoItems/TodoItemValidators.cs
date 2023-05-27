using DemoApp.Application.TodoItems.Commands;
using DemoApp.Infra.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.TodoItems
{
    public class TodoItemCreateDtoValidator : AbstractValidator<TodoItemCreateDto>
    {
        private readonly ITodoItemRepository _repo;

        public TodoItemCreateDtoValidator(ITodoItemRepository repo)
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
            return !await _repo.AnyAsync(x => x.Title.ToLower() == title.ToLower());
        }
    }
}
