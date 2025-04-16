using Domain.Contracts;
using Domain.Entities;
using Persistencies.Data.DbContexts;
using Persistencies.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencies
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _context;
        //private readonly Dictionary<string, object> _Repositories;
        private readonly ConcurrentDictionary<string, object> _Repositories;
        public UnitOfWork(StoreDbContext context)
        {
            _context = context;
            //_Repositories = new Dictionary<string, object>();
            _Repositories = new ConcurrentDictionary<string, object>();
        }
        //public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        //{
        //    var type = typeof(TEntity).Name;
        //    if (!_Repositories.ContainsKey(type))
        //    {
        //        var Repository = new GenericRepository<TEntity, TKey>(_context);
        //        _Repositories.Add(type , Repository);
        //    }
        //    return (IGenericRepository<TEntity, TKey>) _Repositories[type];
        //}
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey> => (IGenericRepository<TEntity, TKey>) _Repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_context));
       
        public async Task<int> SavesChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
