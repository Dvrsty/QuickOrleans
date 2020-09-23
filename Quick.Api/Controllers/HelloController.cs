using Microsoft.AspNetCore.Mvc;
using Orleans;
using Quick.Common.Interface;
using Quick.Dto;
using Quick.IService;
using System.Threading.Tasks;

namespace Quick.Api.Controllers
{
    /// <summary>
    /// Hello world
    /// </summary>
    public class HelloController
    {
        private readonly IGrainFactory _grainFactory;
        public HelloController(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }

        [HttpGet, Route("helloWorld")]
        public async Task<IPagedOutput<HelloOutput>> HelloWorld()
        {
            var service = _grainFactory.GetGrain<IHelloService>("0");
            return await service.GetHelloPaged();
        }
    }
}
