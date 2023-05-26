using AutoMapper;
using DemoApp.Application.TodoLists.Commands;
using DemoApp.Domain.Models;

namespace DemoApp.Application.TodoLists
{
    public sealed class TodoListProfiles : Profile
    {
        public TodoListProfiles()
        {
            CreateMap<CreateTodoListCommand, TodoList>();
            CreateMap<TodoList, TodoListDto>()
                .ForMember(x => x.TodoItems, src => src.MapFrom(opt => opt.TodoItems));
        }
    }
}
