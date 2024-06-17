using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IRepository
{
    public interface IPushNotificationsRepository
    {
        Task<PushNotificationsViewModel> CreateAsync(PushNotificationsViewModel pushNotification);
        Task<PushNotificationsViewModel> ReadAsync(int id);
        Task<PushNotificationsViewModel> ReadByUserIdAsync(int userId);
        Task<List<PushNotificationsViewModel>> ReadAllAsync();
        Task<PushNotificationsViewModel> UpdateAsync(PushNotificationsViewModel pushNotification);
        Task<List<PushNotificationsViewModel>> UpdateRangeAsync(List<PushNotificationsViewModel> pushNotifications);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(PushNotificationsViewModel pushNotification);
        Task<bool> DeleteRangeAsync(List<PushNotificationsViewModel> pushNotifications);
    }
}
