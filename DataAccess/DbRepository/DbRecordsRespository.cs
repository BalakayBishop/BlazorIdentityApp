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
    public class DbRecordsRespository : DbBaseRepository<Records>, IDbRecordsRepository
    {
        private readonly dbContext _db;

        public DbRecordsRespository(dbContext db) : base(db)
        {
            _db = db;
        }
    }
}
