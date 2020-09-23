using Orleans;
using Quick.Common.Interface;
using Quick.Dto;
using Quick.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Quick.IService
{
    public interface IHelloService : IGrainWithStringKey
    {
        Task<IPagedOutput<HelloOutput>> GetHelloPaged();
    }
}
