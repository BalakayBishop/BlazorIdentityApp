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
    public class DbUsersRepository : DbBaseRepository<Users>, IDbUsersRepository
    {
        private readonly dbContext _db;

        public DbUsersRepository(dbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Users> GetByEmailAsync(string email)
        {
            Users user = await _db.Set<Users>().FirstOrDefaultAsync(e => e.Email == email);
            return user;
        }
    }
}
