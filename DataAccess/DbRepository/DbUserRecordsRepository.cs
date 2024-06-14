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
    public class DbUserRecordsRepository : DbBaseRepository<UserRecords>, IDbUserRecordsRepository
    {
        private readonly dbContext _db;

        public DbUserRecordsRepository(dbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<UserRecords> GetByPk(int UserId, int RecordId)
        {
            UserRecords userRecord = await _db.Set<UserRecords>().FindAsync(UserId, RecordId);
            return userRecord;
        }
    }
}
