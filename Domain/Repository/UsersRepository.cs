using DataAccess.IDbRepository;
using DataAccess.Entities;
using Domain.IRepository;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbUsersRepository _dbUsersRepository;
        public UsersRepository(IDbUsersRepository dbUsersRepository)
        {
            _dbUsersRepository = dbUsersRepository;
        }

        public async Task<UsersViewModel> CreateAsync(UsersViewModel user)
        {
            try
            {
                Users dbUser = new()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
                await _dbUsersRepository.Create(dbUser);
                user.Id = dbUser.Id;

                return user;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<UsersViewModel> ReadAsync(int id)
        {
            try
            {
                Users dbUser = await _dbUsersRepository.GetById(id);

                if (dbUser is not null)
                {
                    UsersViewModel user = new()
                    {
                        Id = dbUser.Id,
                        FirstName = dbUser.FirstName,
                        LastName = dbUser.LastName,
                    };

                    return user;
                }

                return null!;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<List<UsersViewModel>> ReadAllAsync()
        {
            try
            {
                List<Users> dbUsers = await _dbUsersRepository.GetAll();

                if (dbUsers is not null)
                {
                    List<UsersViewModel> users = new();
                    foreach (Users dbUser in dbUsers)
                    {
                        UsersViewModel user = new()
                        {
                            Id = dbUser.Id,
                            FirstName = dbUser.FirstName,
                            LastName = dbUser.LastName,
                        };

                        users.Add(user);
                    }

                    return users;
                }

                return null!;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<UsersViewModel> UpdateAsync(UsersViewModel user)
        {
            if (user is not null)
            {
                try
                {
                    Users dbUser = await _dbUsersRepository.GetById(user.Id);
                    if (dbUser is not null)
                    {
                        dbUser.FirstName = user.FirstName;
                        dbUser.LastName = user.LastName;
                        await _dbUsersRepository.Update(dbUser);

                        return user;
                    }
                }
                catch
                {
                    return null!;
                }
            }

            return null!;
        }

        public async Task<List<UsersViewModel>> UpdateRangeAsync(List<UsersViewModel> users)
        {
            if (users is not null)
            {
                try
                {
                    List<Users> dbUsers = new();
                    foreach (UsersViewModel user in users)
                    {
                        Users dbUser = await _dbUsersRepository.GetById(user.Id);
                        if (dbUser is not null)
                        {
                            dbUser.FirstName = user.FirstName;
                            dbUser.LastName = user.LastName;
                            dbUsers.Add(dbUser);
                        }
                    }
                    await _dbUsersRepository.UpdateRange(dbUsers);

                    return users;
                }
                catch
                {
                    return null!;
                }
            }

            return null!;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                await _dbUsersRepository.Delete(id);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteRangeAsync(List<int> ids)
        {
            try
            {
                List<Users> dbUsers = await _dbUsersRepository.GetAll();
                dbUsers = dbUsers.Where(u => ids.Contains(u.Id)).ToList();
                await _dbUsersRepository.DeleteRange(dbUsers);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(UsersViewModel user)
        {
            try
            {
                Users dbUser = await _dbUsersRepository.GetById(user.Id);
                await _dbUsersRepository.Delete(dbUser);

                return true;
            }
            catch
            {
                return true;
            }
        }

        public async Task<bool> DeleteRangeAsync(List<UsersViewModel> users)
        {
            try
            {
                List<Users> dbUsers = new();
                foreach (UsersViewModel user in users)
                {
                    Users dbUser = await _dbUsersRepository.GetById(user.Id);
                    dbUsers.Add(dbUser);
                }
                await _dbUsersRepository.DeleteRange(dbUsers);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
