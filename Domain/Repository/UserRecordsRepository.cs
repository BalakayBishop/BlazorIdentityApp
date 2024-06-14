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
    public class UserRecordsRepository : IUserRecordsRepository
    {
        private readonly IDbUserRecordsRepository _dbUserRecordsRepository;
        public UserRecordsRepository(IDbUserRecordsRepository dbUserRecordsRepository)
        {
            _dbUserRecordsRepository = dbUserRecordsRepository;
        }

        public async Task<UserRecordsViewModel> CreateAsync(UserRecordsViewModel userRecord)
        {
            try
            {
                UserRecords dbUserRecord = new()
                {
                    UserId = userRecord.UserId,
                    RecordId = userRecord.RecordId,
                };
                await _dbUserRecordsRepository.Create(dbUserRecord);

                return userRecord;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<List<UserRecordsViewModel>> ReadAllAsync()
        {
            try
            {
                List<UserRecords> dbUserRecords = await _dbUserRecordsRepository.GetAll();

                if (dbUserRecords is not null)
                {
                    List<UserRecordsViewModel> userRecords = new();
                    foreach (UserRecords dbUserRecord in dbUserRecords)
                    {
                        UserRecordsViewModel userRecord = new()
                        {
                            UserId = dbUserRecord.UserId,
                            RecordId = dbUserRecord.RecordId,
                        };

                        userRecords.Add(userRecord);
                    }

                    return userRecords;
                }

                return null!;
            }
            catch
            {
                return null!;
            }
        }

        public async Task<UserRecordsViewModel> ReadAsync(int UserId, int RecordId)
        {
            try
            {
                UserRecords dbUserRecord = await _dbUserRecordsRepository.GetByPk(UserId, RecordId);

                if (dbUserRecord is not null)
                {
                    UserRecordsViewModel userRecord = new()
                    {
                        UserId = dbUserRecord.UserId,
                        RecordId = dbUserRecord.RecordId,
                    };

                    return userRecord;
                }

                return null!;
            }
            catch
            {
                return null!;
            }
        }

        public Task<UserRecordsViewModel> UpdateAsync(UserRecordsViewModel userRecord)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserRecordsViewModel>> UpdateRangeAsync(List<UserRecordsViewModel> userRecords)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(UserRecordsViewModel userRecord)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteRangeAsync(List<UserRecordsViewModel> userRecords)
        {
            throw new NotImplementedException();
        }
    }
}
