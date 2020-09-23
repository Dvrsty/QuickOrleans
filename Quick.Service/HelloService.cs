using Orleans;
using Quick.Common.Dtos;
using Quick.Common.Extensions;
using Quick.Common.Interface;
using Quick.Dto;
using Quick.IRepositories;
using Quick.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Service
{
    public class HelloService : Grain, IHelloService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public HelloService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<IPagedOutput<HelloOutput>> GetHelloPaged()
        {
            var helloRepo = _repositoryFactory.CreateRepository<IHelloRepository>();
            var data = await helloRepo.GetHelloWorld();
            var output = new PagedOutput<HelloOutput>();
            output.Items = data.Items.MapToList<HelloOutput>();
            output.TotalCount = data.TotalCount;
            return output;
        }
    }
}
