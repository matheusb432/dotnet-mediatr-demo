using DemoApp.API.Configurations;
using DemoApp.API.Controllers;
using DemoApp.Application.TodoItems;
using DemoApp.Application.TodoItems.Commands;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Api.Controllers
{
    public class TodoItemsController : ApiControllerBase
    {
        [HttpGet]
        [ODataQuery]
        public async Task<IQueryable<TodoItemDto>> GetFromQuery() =>
            await Mediator.Send(new GetTodoItemsCommand());

        [HttpPost]
        public async Task<ActionResult<int>> Create(CreateTodoItemCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
