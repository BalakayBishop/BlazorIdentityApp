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
    public class DbPushNotificationsRepository : DbBaseRepository<PushNotifications>, IDbPushNotificationsRepository
    {
        private readonly dbContext _db;

        public DbPushNotificationsRepository(dbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<PushNotifications> GetByUserId(int userId)
        {
            PushNotifications pushNotification = await _db.Set<PushNotifications>().FirstOrDefaultAsync(p => p.UserId == userId);
            return pushNotification;
        }
    }
}