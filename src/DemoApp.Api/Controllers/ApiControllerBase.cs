using DemoApp.Api.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.API.Controllers
{
    [ApiController]
    [ApiExceptionFilter]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender? _mediator;

        protected ISender Mediator =>
            _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
