﻿using DataAccess.DbRepository;
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
    public class PushNotificationsRepository : IPushNotificationsRepository
    {
        private readonly IDbUsersRepository _dbUsersRepository;
        private readonly IDbPushNotificationsRepository _dbPushNotificationsRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PushNotificationsRepository"/> class.
        /// </summary>
        /// <param name="dbPushNotificationsRepository">The database push notifications repository.</param>
        /// <param name="dbUsersRepository">The database users repository.</param>
        public PushNotificationsRepository(IDbPushNotificationsRepository dbPushNotificationsRepository, IDbUsersRepository dbUsersRepository)
        {
            _dbPushNotificationsRepository = dbPushNotificationsRepository;
            _dbUsersRepository = dbUsersRepository;
        }

        /// <summary>
        /// Creates a new push notification asynchronously.
        /// </summary>
        /// <param name="pushNotification">The push notification view model.</param>
        /// <returns>The created push notification view model.</returns>
        public async Task<PushNotificationsViewModel> CreateAsync(PushNotificationsViewModel pushNotification)
        {
            try
            {
                PushNotifications dbPushNotification = new()
                {
                    Endpoint = pushNotification.Endpoint,
                    P256DH = pushNotification.P256DH,
                    Auth = pushNotification.Auth,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    UserId = pushNotification.UserId,
                };
                await _dbPushNotificationsRepository.Create(dbPushNotification);
                pushNotification.Id = dbPushNotification.Id;

                return pushNotification;
            }
            catch
            {
                return null!;
            }
        }

        /// <summary>
        /// Retrieves all push notifications asynchronously.
        /// </summary>
        /// <returns>A list of push notification view models.</returns>
        public async Task<List<PushNotificationsViewModel>> ReadAllAsync()
        {
            try
            {
                List<PushNotifications> dbPushNotifications = await _dbPushNotificationsRepository.GetAll();

                if (dbPushNotifications is not null)
                {
                    List<PushNotificationsViewModel> pushNotifications = new();
                    foreach (PushNotifications dbPushNotification in dbPushNotifications)
                    {
                        PushNotificationsViewModel pushNotification = new()
                        {
                            Id = dbPushNotification.Id,
                            UserId = dbPushNotification.UserId,
                            Endpoint = dbPushNotification.Endpoint,
                            P256DH = dbPushNotification.P256DH,
                            Auth = dbPushNotification.Auth,
                            CreatedDate = dbPushNotification.CreatedDate,
                            UpdatedDate = dbPushNotification.UpdatedDate
                        };

                        pushNotifications.Add(pushNotification);
                    }

                    return pushNotifications;
                }

                return null!;
            }
            catch
            {
                return null!;
            }
        }

        /// <summary>
        /// Retrieves a push notification by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the push notification.</param>
        /// <returns>The push notification view model.</returns>
        public async Task<PushNotificationsViewModel> ReadAsync(int id)
        {
            try
            {
                PushNotifications dbPushNotification = await _dbPushNotificationsRepository.GetById(id);

                if (dbPushNotification is not null)
                {
                    PushNotificationsViewModel pushNotification = new()
                    {
                        Id = dbPushNotification.Id,
                        UserId = dbPushNotification.UserId,
                        Endpoint = dbPushNotification.Endpoint,
                        Auth = dbPushNotification.Auth,
                        CreatedDate = dbPushNotification.CreatedDate,
                        UpdatedDate = dbPushNotification.UpdatedDate
                    };

                    return pushNotification;
                }

                return null!;
            }
            catch
            {
                return null!;
            }
        }

        /// <summary>
        /// Retrieves a push notification by user ID asynchronously.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The push notification view model.</returns>
        public async Task<PushNotificationsViewModel> ReadByUserIdAsync(int userId)
        {
            try
            {
                PushNotifications pushNotification = await _dbPushNotificationsRepository.GetByUserId(userId);
                if (pushNotification is not null)
                {
                    PushNotificationsViewModel pushNotificationsViewModel = new()
                    {
                        Id = pushNotification.Id,
                        Endpoint = pushNotification.Endpoint,
                        Auth = pushNotification.Auth,
                        CreatedDate = pushNotification.CreatedDate,
                        UpdatedDate = pushNotification.UpdatedDate,
                        UserId = pushNotification.UserId,
                    };

                    return pushNotificationsViewModel;
                }
                else
                    return null!;
            }
            catch { return null!; }
        }

        /// <summary>
        /// Updates a push notification asynchronously.
        /// </summary>
        /// <param name="pushNotification">The push notification view model.</param>
        /// <returns>The updated push notification view model.</returns>
        public async Task<PushNotificationsViewModel> UpdateAsync(PushNotificationsViewModel pushNotification)
        {
            if (pushNotification is not null)
            {
                try
                {
                    PushNotifications dbPushNotification = await _dbPushNotificationsRepository.GetByUserId(pushNotification.UserId);
                    if (dbPushNotification is not null)
                    {
                        dbPushNotification.Endpoint = pushNotification.Endpoint;
                        dbPushNotification.P256DH = pushNotification.P256DH;
                        dbPushNotification.Auth = pushNotification.Auth;
                        dbPushNotification.UpdatedDate = DateTime.UtcNow;

                        await _dbPushNotificationsRepository.Update(dbPushNotification);

                        return pushNotification;
                    }
                }
                catch
                {
                    return null!;
                }
            }

            return null!;
        }

        /// <summary>
        /// Updates a range of push notifications asynchronously.
        /// </summary>
        /// <param name="pushNotifications">The list of push notification view models.</param>
        /// <returns>The updated list of push notification view models.</returns>
        public async Task<List<PushNotificationsViewModel>> UpdateRangeAsync(List<PushNotificationsViewModel> pushNotifications)
        {
            if (pushNotifications is not null)
            {
                try
                {
                    List<PushNotifications> dbPushNotifications = new();
                    foreach (PushNotificationsViewModel push in pushNotifications)
                    {
                        PushNotifications dbPush = await _dbPushNotificationsRepository.GetById(push.Id);
                        if (dbPush is not null)
                        {
                            dbPush.UserId = 1;
                            dbPush.Endpoint = push.Endpoint;
                            dbPush.P256DH = push.P256DH;
                            dbPush.Auth = push.Auth;
                            dbPush.UpdatedDate = DateTime.UtcNow;

                            dbPushNotifications.Add(dbPush);
                        }
                    }
                    await _dbPushNotificationsRepository.UpdateRange(dbPushNotifications);

                    return pushNotifications;
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

        public Task<bool> DeleteAsync(PushNotificationsViewModel pushNotification)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRangeAsync(List<PushNotificationsViewModel> pushNotifications)
        {
            throw new NotImplementedException();
        }
    }
}
