using Domain.Contracts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistencies.Data.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencies.Repositories
{
    public class GenericRepository<TEntity , TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;
        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false)
        {
            if(typeof (TEntity) == typeof(Product))
            {
                return trackChanges ?
                await _context.Products.Include(P => P.ProductType).Include(P => P.ProductBrand).ToListAsync() as IEnumerable<TEntity>
               : await _context.Products.Include(P => P.ProductType).Include(P => P.ProductBrand).AsNoTracking().ToListAsync() as IEnumerable<TEntity>;
            }
            return trackChanges?
                   await _context.Set<TEntity>().ToListAsync()
                  :await _context.Set<TEntity>().AsNoTracking().ToListAsync();
           
        }
        public async Task<TEntity> GetByIdAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
            {
                return await _context.Products.Include(P => P.ProductType).Include(P => P.ProductBrand).FirstOrDefaultAsync(p => p.Id.Equals(id)) as TEntity;
            }
            return await _context.Set<TEntity>().FindAsync(id);
        }
        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public void Update(TEntity entity)
        {
             _context.Update(entity);
        }
        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> spec, bool trackChanges = false)
        {
             return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<int> ContAsync(ISpecifications<TEntity, TKey> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }
        private IQueryable<TEntity> ApplySpecification(ISpecifications<TEntity, TKey> spec)
        {
            return SpecificationEvaluator.GetQuery<TEntity, TKey>(_context.Set<TEntity>(), spec);
        }
    }
}
