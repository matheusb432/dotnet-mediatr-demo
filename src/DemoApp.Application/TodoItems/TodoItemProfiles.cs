using AutoMapper;
using DemoApp.Application.TodoItems.Commands;
using DemoApp.Domain.Models;

namespace DemoApp.Application.TodoItems
{
    public sealed class TodoListProfiles : Profile
    {
        public TodoListProfiles()
        {
            CreateMap<CreateTodoItemCommand, TodoItem>();
            CreateMap<UpdateTodoItemCommand, TodoItem>();
            CreateMap<TodoItem, TodoItemDto>().ReverseMap();
        }
    }
}
