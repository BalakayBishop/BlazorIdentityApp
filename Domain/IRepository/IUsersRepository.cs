using Domain.ViewModels;

namespace Domain.IRepository
{
    public interface IUsersRepository
    {
        Task<UsersViewModel> CreateAsync(UsersViewModel user);
        Task<UsersViewModel> ReadAsync(int id);
        Task<UsersViewModel> ReadAsync(string email);
        Task<List<UsersViewModel>> ReadAllAsync();
        Task<UsersViewModel> UpdateAsync(UsersViewModel user);
        Task<List<UsersViewModel>> UpdateRangeAsync(List<UsersViewModel> users);
        Task<bool> DeleteAsync(int id);
        Task<bool> DeleteAsync(UsersViewModel user);
        Task<bool> DeleteRangeAsync(List<UsersViewModel> users);
    }
}
