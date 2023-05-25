using AutoMapper;
using DemoApp.Application.TodoItems.Commands;
using DemoApp.Domain.Models;

namespace DemoApp.Application.TodoItems
{
    public sealed class TodoItemProfiles : Profile
    {
        public TodoItemProfiles()
        {
            CreateMap<CreateTodoItemCommand, TodoItem>();
            CreateMap<UpdateTodoItemCommand, TodoItem>();
            CreateMap<TodoItem, TodoItemDto>();
        }
    }
}
