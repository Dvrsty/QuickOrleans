using Microsoft.EntityFrameworkCore;
using Quick.Common.Interface;
using Quick.Core.Extensions;
using Quick.Interface;
using Quick.IRepositories;
using Quick.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Repositories
{
    public class HelloRepository : Repository<HelloModel>, IHelloRepository
    {
        public HelloRepository(IQuickContext quickContext) : base(quickContext)
        {

        }

        public async Task<IPagedOutput<HelloModel>> GetHelloWorld()
        {
            return await Entities.Paged(s => s.Id, 1, int.MaxValue);
        }
    }
}
