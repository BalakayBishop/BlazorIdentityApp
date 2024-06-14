using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IRecordsRepository
    {
        Task<RecordsViewModel> CreateAsync(RecordsViewModel record);
        Task<RecordsViewModel> ReadAsync(int id);
        Task<List<RecordsViewModel>> ReadAllAsync();
        Task<RecordsViewModel> UpdateAsync(RecordsViewModel record);
        Task<List<RecordsViewModel>> UpdateRangeAsync(List<RecordsViewModel> records);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(RecordsViewModel record);
        Task<bool> DeleteRangeAsync(List<RecordsViewModel> records);
    }

}
