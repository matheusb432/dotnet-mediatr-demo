using AutoMapper;
using DemoApp.Application.TodoItems.Commands;
using DemoApp.Domain.Models;

namespace DemoApp.Application.TodoItems
{
    public sealed class TodoListProfiles : Profile
    {
        public TodoListProfiles()
        {
            CreateMap<TodoItemCreateDto, TodoItem>();
            CreateMap<UpdateTodoItemCommand, TodoItem>();
            CreateMap<TodoItem, TodoItemDto>().ReverseMap();
        }
    }
}
