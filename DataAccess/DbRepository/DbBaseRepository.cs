using DataAccess.Entities;
using DataAccess.IDbRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DbRepository
{
    public class DbBaseRepository<TEntity> : IDbBaseRepository<TEntity> where TEntity : class
    {
        private readonly dbContext _db;

        public DbBaseRepository(dbContext db)
        {
            _db = db;
        }

        public async Task Create(TEntity entity)
        {
            _db.Set<TEntity>().Add(entity);
            await _db.SaveChangesAsync();
        }

        public async Task CreateRange(List<TEntity> entity)
        {
            _db.Set<TEntity>().AddRange(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAll()
        {
            List<TEntity> entities = await _db.Set<TEntity>().AsNoTracking().ToListAsync();
            return entities;
        }

        public List<TEntity> GetAll(string[] includes)
        {
            IEnumerable<TEntity> entities = includes.Aggregate(_db.Set<TEntity>().AsQueryable(), (query, path) => query.Include(path));
            return entities.ToList();
        }

        public async Task<TEntity> GetById(int id)
        {
            TEntity entity = await _db.Set<TEntity>().FindAsync(id);
            return entity;
        }

        public async Task Update(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateRange(List<TEntity> entity)
        {
            _db.Set<TEntity>().UpdateRange(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            TEntity entity = await GetById(id);
            _db.Set<TEntity>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteRange(List<TEntity> entities)
        {
            _db.Set<TEntity>().RemoveRange(entities);
            await _db.SaveChangesAsync();
        }
    }
}
