using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IDbRepository
{
    public interface IDbBaseRepository<TEntity> where TEntity : class
    {
        // Create
        Task Create(TEntity entity);
        Task CreateRange(List<TEntity> entity);

        // Read
        Task<List<TEntity>> GetAll();
        List<TEntity> GetAll(string[] includes);
        Task<TEntity> GetById(int id);

        // Update
        Task Update(TEntity entity);
        Task UpdateRange(List<TEntity> entity);

        // Delete
        Task Delete(int id);
        Task Delete(TEntity entity);
        Task DeleteRange(List<TEntity> entities);
    }
}
