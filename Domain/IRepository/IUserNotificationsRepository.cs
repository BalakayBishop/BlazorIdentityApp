using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IUserNotificationsRepository
    {
        Task<UserNotificationsViewModel> CreateAsync(UserNotificationsViewModel userNotification);
        Task<UserNotificationsViewModel> ReadAsync(int id);
        Task<List<UserNotificationsViewModel>> ReadAllAsync();
        Task<UserNotificationsViewModel> UpdateAsync(UserNotificationsViewModel userNotification);
        Task<List<UserNotificationsViewModel>> UpdateRangeAsync(List<UserNotificationsViewModel> userNotifications);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(UserNotificationsViewModel userNotification);
        Task<bool> DeleteRangeAsync(List<UserNotificationsViewModel> userNotifications);
    }
}
