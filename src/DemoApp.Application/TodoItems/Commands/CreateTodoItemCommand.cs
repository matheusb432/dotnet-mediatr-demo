using AutoMapper;
using DemoApp.Domain.Models;
using DemoApp.Infra.Repositories;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Application.TodoItems.Commands
{
    public record CreateTodoItemCommand : IRequest<int>
    {
        public int ListId { get; init; }

        public string? Title { get; init; }
    }

    public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
    {
        public CreateTodoItemCommandValidator()
        {
            RuleFor(v => v.Title).MaximumLength(200).NotEmpty();
        }
    }

    public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
    {
        private readonly ITodoItemRepository _repo;
        private readonly IMapper _mapper;

        public CreateTodoItemCommandHandler(ITodoItemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<int> Handle(
            CreateTodoItemCommand request,
            CancellationToken cancellationToken
        )
        {
            var entity = _mapper.Map<TodoItem>(request);

            await _repo.InsertAsync(entity);

            return entity.Id;
        }
    }
}
