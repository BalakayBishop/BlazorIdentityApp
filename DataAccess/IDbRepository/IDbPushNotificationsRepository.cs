﻿using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IDbRepository
{
    public interface IDbPushNotificationsRepository : IDbBaseRepository<PushNotifications>
    {
        Task<PushNotifications> GetByUserId(int userId);
    }
}
