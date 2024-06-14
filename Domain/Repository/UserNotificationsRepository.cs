using DataAccess.DbRepository;
using DataAccess.Entities;
using DataAccess.IDbRepository;
using Domain.IRepository;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class UserNotificationsRepository : IUserNotificationsRepository
    {
        private readonly IDbUserNotificationsRepository _dbUserNotificationsRepository;
        public UserNotificationsRepository(IDbUserNotificationsRepository dbUserNotificationsRepository)
        {
            _dbUserNotificationsRepository = dbUserNotificationsRepository;
        }

        public async Task<UserNotificationsViewModel> CreateAsync(UserNotificationsViewModel userNotification)
        {
            try
            {
                UserNotifications dbUserNotification = new()
                {
                    Title = userNotification.Title,
                    Body = userNotification.Body,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = new DateTime(),
                    UserId = 1,
                    PushId = userNotification.PushId,
                };
                await _dbUserNotificationsRepository.Create(dbUserNotification);
                userNotification.Id = dbUserNotification.Id;

                return userNotification;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<List<UserNotificationsViewModel>> ReadAllAsync()
        {
            try
            {
                List<UserNotifications> dbUserNotifications = await _dbUserNotificationsRepository.GetAll();

                if (dbUserNotifications is not null)
                {
                    List<UserNotificationsViewModel> userNotifications = new();
                    foreach (UserNotifications dbUserNotification in dbUserNotifications)
                    {
                        UserNotificationsViewModel userNotification = new()
                        {
                            Id = dbUserNotification.Id,
                            Title = dbUserNotification.Title,
                            Body = dbUserNotification.Body,
                            CreatedDate = dbUserNotification.CreatedDate,
                            UpdatedDate = dbUserNotification.UpdatedDate,
                            UserId = dbUserNotification.UserId,
                            PushId = dbUserNotification.PushId,
                        };

                        userNotifications.Add(userNotification);
                    }

                    return userNotifications;
                }

                return null!;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<UserNotificationsViewModel> ReadAsync(int id)
        {
            try
            {
                UserNotifications dbUserNotification = await _dbUserNotificationsRepository.GetById(id);

                if (dbUserNotification is not null)
                {
                    UserNotificationsViewModel userNotification = new()
                    {
                        Id = dbUserNotification.Id,
                        Title = dbUserNotification.Title,
                        Body = dbUserNotification.Body,
                        CreatedDate = dbUserNotification.CreatedDate,
                        UpdatedDate = dbUserNotification.UpdatedDate,
                        UserId = dbUserNotification.UserId,
                        PushId = dbUserNotification.PushId,
                    };

                    return userNotification;
                }

                return null!;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<UserNotificationsViewModel> UpdateAsync(UserNotificationsViewModel userNotification)
        {
            if (userNotification is not null)
            {
                try
                {
                    UserNotifications dbUserNotification = await _dbUserNotificationsRepository.GetById(userNotification.Id);
                    if (dbUserNotification is not null)
                    {
                        dbUserNotification.Title = userNotification.Title;
                        dbUserNotification.Body = userNotification.Body;
                        dbUserNotification.UpdatedDate = DateTime.UtcNow;
                        dbUserNotification.UserId = 1;
                        dbUserNotification.PushId = userNotification.PushId;

                        await _dbUserNotificationsRepository.Update(dbUserNotification);

                        return userNotification;
                    }
                }
                catch
                {
                    return null!;
                }
            }

            return null!;
        }

        public async Task<List<UserNotificationsViewModel>> UpdateRangeAsync(List<UserNotificationsViewModel> userNotifications)
        {
            if (userNotifications is not null)
            {
                try
                {
                    List<UserNotifications> dbUserNotifications = new();
                    foreach (UserNotificationsViewModel userNotification in userNotifications)
                    {
                        UserNotifications dbUserNotification = await _dbUserNotificationsRepository.GetById(userNotification.Id);
                        if (dbUserNotification is not null)
                        {
                            dbUserNotification.Title = userNotification.Title;
                            dbUserNotification.Body = userNotification.Body;
                            dbUserNotification.UpdatedDate = DateTime.UtcNow;
                            dbUserNotification.UserId = 1;
                            dbUserNotification.PushId = userNotification.PushId;

                            dbUserNotifications.Add(dbUserNotification);
                        }
                    }
                    await _dbUserNotificationsRepository.UpdateRange(dbUserNotifications);

                    return userNotifications;
                }
                catch
                {
                    return null!;
                }
            }

            return null!;
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(UserNotificationsViewModel userNotification)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRangeAsync(List<UserNotificationsViewModel> userNotifications)
        {
            throw new NotImplementedException();
        }
    }
}
