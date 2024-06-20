using DataAccess.IDbRepository;
using DataAccess.Entities;
using Domain.IRepository;
using Domain.ViewModels;

namespace Domain.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IDbUsersRepository _dbUsersRepository;

        public UsersRepository(IDbUsersRepository dbUsersRepository)
        {
            _dbUsersRepository = dbUsersRepository;
        }

        public async Task<UsersViewModel> CreateAsync(UsersViewModel userToCreate)
        {
            Users newUserEntity = new()
            {
                FirstName = userToCreate.FirstName,
                LastName = userToCreate.LastName,
                Email = userToCreate.Email,
                CreatedDate = DateTime.UtcNow,
            };
            await _dbUsersRepository.Create(newUserEntity);
            userToCreate.Id = newUserEntity.Id;

            return userToCreate;
        }

        public async Task<UsersViewModel> ReadAsync(int id)
        {
            Users dbUser = await _dbUsersRepository.GetById(id);
            return dbUser != null ? new UsersViewModel
            {
                Id = dbUser.Id,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                Email = dbUser.Email,
                CreatedDate = dbUser.CreatedDate,
            } : null!;
        }

        public async Task<UsersViewModel> ReadAsync(string email)
        {
            Users dbUser = await _dbUsersRepository.GetByEmailAsync(email);
            return dbUser != null ? new UsersViewModel
            {
                Id = dbUser.Id,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
            } : null!;
        }

        public async Task<List<UsersViewModel>> ReadAllAsync()
        {
            List<Users> dbUsers = await _dbUsersRepository.GetAll();
            return dbUsers.Select(dbUser => new UsersViewModel
            {
                Id = dbUser.Id,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
            }).ToList();
        }

        public async Task<UsersViewModel> UpdateAsync(UsersViewModel user)
        {
            Users dbUser = await _dbUsersRepository.GetById(user.Id);
            if (dbUser != null)
            {
                dbUser.FirstName = user.FirstName;
                dbUser.LastName = user.LastName;
                await _dbUsersRepository.Update(dbUser);

                return user;
            }
            return null!;
        }

        public async Task<List<UsersViewModel>> UpdateRangeAsync(List<UsersViewModel> users)
        {
            var dbUsers = new List<Users>();
            foreach (var user in users)
            {
                var dbUser = await _dbUsersRepository.GetById(user.Id);
                if (dbUser != null)
                {
                    dbUser.FirstName = user.FirstName;
                    dbUser.LastName = user.LastName;
                    dbUsers.Add(dbUser);
                }
            }
            await _dbUsersRepository.UpdateRange(dbUsers);

            return users;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            await _dbUsersRepository.Delete(id);
            return true;
        }

        public async Task<bool> DeleteRangeAsync(List<int> ids)
        {
            List<Users> dbUsers = await _dbUsersRepository.GetAll();
            var usersToDelete = dbUsers.Where(u => ids.Contains(u.Id)).ToList();
            await _dbUsersRepository.DeleteRange(usersToDelete);
            return true;
        }

        public async Task<bool> DeleteAsync(UsersViewModel user)
        {
            await _dbUsersRepository.Delete(user.Id);
            return true;
        }

        public async Task<bool> DeleteRangeAsync(List<UsersViewModel> users)
        {
            var ids = users.Select(u => u.Id).ToList();
            List<Users> dbUsers = await _dbUsersRepository.GetAll();
            var usersToDelete = dbUsers.Where(u => ids.Contains(u.Id)).ToList();
            await _dbUsersRepository.DeleteRange(usersToDelete);
            return true;
        }
    }
}
