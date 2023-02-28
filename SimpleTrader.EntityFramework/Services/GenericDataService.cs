using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.EntityFramework.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T:DomainObject
    {
        private readonly SimpleTraderDbContextFactory _contextFactory;
        private readonly NonQueryDataService<T> nonQueryDataService;

        public GenericDataService(SimpleTraderDbContextFactory contextFactory)
        {
           _contextFactory = contextFactory;
            nonQueryDataService = new NonQueryDataService<T>(contextFactory); 
        }

        public async Task<T> Create(T entity)
        {
            return await nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await nonQueryDataService.Delete(id);
        }

        public async Task<T> Get(int id)
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (SimpleTraderDbContext context = _contextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = await context.Set<T>().ToListAsync<T>();
                return entities;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            return await nonQueryDataService.Update(id, entity);
        }
    }
}
