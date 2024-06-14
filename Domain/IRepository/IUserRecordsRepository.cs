using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IUserRecordsRepository
    {
        Task<UserRecordsViewModel> CreateAsync(UserRecordsViewModel userRecord);
        Task<UserRecordsViewModel> ReadAsync(int UserId, int RecordId);
        Task<List<UserRecordsViewModel>> ReadAllAsync();
        Task<UserRecordsViewModel> UpdateAsync(UserRecordsViewModel userRecord);
        Task<List<UserRecordsViewModel>> UpdateRangeAsync(List<UserRecordsViewModel> userRecords);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(UserRecordsViewModel userRecord);
        Task<bool> DeleteRangeAsync(List<UserRecordsViewModel> userRecords);
    }
}
