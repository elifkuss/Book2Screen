using System;
using Book2Screen.Models;
using Book2Screen.Repository.Abstractions;
using Book2Screen.Repository.Concretes;

namespace Book2Screen.UnitOfWorks
{
	public class UnitOfWorks
	{
        public class UnitOfWork : IUnitOfWork
        {
            private readonly MovieDbContext dbContext;

            public UnitOfWork(MovieDbContext dbContext)
            {
                this.dbContext = dbContext;
            }
            public async ValueTask DisposeAsync()
            {
                await dbContext.DisposeAsync();
            }

            public int Save()
            {
                return dbContext.SaveChanges();
            }

            public async Task<int> SaveAsync()
            {
                return await dbContext.SaveChangesAsync();
            }

            IRepository<T> IUnitOfWork.GetRepository<T>()
            {
                return new Repository<T>(dbContext);
            }
        }
    }
}

