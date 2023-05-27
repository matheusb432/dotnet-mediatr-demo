using DemoApp.API.Configurations;
using DemoApp.API.Controllers;
using DemoApp.Application.Common.ViewModels;
using DemoApp.Application.TodoItems;
using DemoApp.Application.TodoItems.Commands;
using DemoApp.Application.TodoItems.Queries;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DemoApp.Api.Controllers
{
    public class TodoItemsController : ApiControllerBase
    {
        [HttpGet]
        [ODataQuery]
        public async Task<IQueryable<TodoItemDto>> GetFromQuery() =>
            await Mediator.Send(new GetTodoItemsQuery());

        [HttpPost]
        public async Task<ActionResult<PostReturnViewModel>> Create(CreateTodoItemCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut]
        public async Task<ActionResult> Update(UpdateTodoItemCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteTodoItemCommand(id));
            return NoContent();
        }

        [HttpDelete("withBehavior/{id}")]
        public async Task<ActionResult> DeleteWithBehavior(int id)
        {
            await Mediator.Send(new DeleteTodoItemWithBehaviorCommand(id));
            return NoContent();
        }
    }
}
