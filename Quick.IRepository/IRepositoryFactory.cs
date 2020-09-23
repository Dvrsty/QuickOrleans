using System;
using System.Collections.Generic;
using System.Text;

namespace Quick.IRepositories
{
    public interface IRepositoryFactory
    {
        //IRepositorys<T> CreateRepository<T>(IconcardContext mydbcontext) where T : class;
        T CreateRepository<T>() where T : IRepository;
    }
}
