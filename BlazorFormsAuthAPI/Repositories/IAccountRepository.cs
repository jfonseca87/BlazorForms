using Shared.Models;

namespace BlazorFormsAuthAPI.Repositories
{
    public interface IAccountRepository
    {
        Task<User> GetUserByName(string name);
    }
}