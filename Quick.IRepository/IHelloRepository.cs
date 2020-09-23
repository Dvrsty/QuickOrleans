using Quick.Common.Interface;
using Quick.Model;
using System.Threading.Tasks;

namespace Quick.IRepositories
{
    public interface IHelloRepository : IRepository<HelloModel>
    {
        Task<IPagedOutput<HelloModel>> GetHelloWorld();
    }
}
