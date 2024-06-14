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
    public class DbUserNotificationsRepository : DbBaseRepository<UserNotifications>, IDbUserNotificationsRepository
    {
        private readonly dbContext _db;

        public DbUserNotificationsRepository(dbContext db) : base(db)
        {
            _db = db;
        }
    }
}
