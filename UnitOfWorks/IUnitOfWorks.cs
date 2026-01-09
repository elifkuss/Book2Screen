using System;
using Book2Screen.Repository.Abstractions;

namespace Book2Screen.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IRepository<T> GetRepository<T>() where T : class; 
        Task<int> SaveAsync();
        int Save();
    }
}

