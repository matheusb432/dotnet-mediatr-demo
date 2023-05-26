using AutoMapper;

namespace DemoApp.Application.Base
{
    public abstract class BaseRequestHandler
    {
        protected readonly IMapper Mapper;

        public BaseRequestHandler(IMapper mapper) => Mapper = mapper;
    }
}
