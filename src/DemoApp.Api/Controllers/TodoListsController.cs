using DemoApp.API.Configurations;
using DemoApp.API.Controllers;
using DemoApp.Application.Common.ViewModels;
using DemoApp.Application.TodoLists;
using DemoApp.Application.TodoLists.Commands;
using DemoApp.Application.TodoLists.Queries;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Api.Controllers
{
    public class TodoListsController : ApiControllerBase
    {
        [HttpGet("query")]
        [ODataQuery]
        public async Task<IQueryable<TodoListDto>> GetFromQuery() =>
            await Mediator.Send(new GetTodoListsQuery());

        [HttpPost]
        public async Task<ActionResult<PostReturnViewModel>> Create(CreateTodoListCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTodoListCommand(id));
            return NoContent();
        }
    }
}
