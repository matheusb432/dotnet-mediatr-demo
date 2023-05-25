using DemoApp.API.Configurations;
using DemoApp.API.Controllers;
using DemoApp.Application.TodoItems.Commands;
using DemoApp.Application.TodoItems;
using Microsoft.AspNetCore.Mvc;
using DemoApp.Application.Common.ViewModels;

namespace DemoApp.Api.Controllers
{
    public class TodoListsController : ApiControllerBase
    {
        [HttpGet]
        [ODataQuery]
        public async Task<IQueryable<TodoItemDto>> GetFromQuery() =>
            await Mediator.Send(new GetTodoItemsCommand());

        [HttpPost]
        public async Task<ActionResult<PostReturnViewModel>> Create(CreateTodoItemCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
