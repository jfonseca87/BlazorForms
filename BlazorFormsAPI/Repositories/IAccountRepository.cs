using BlazorFormsAPI.Models;
using System.Threading.Tasks;

namespace BlazorFormsAPI.Repositories
{
    public interface IAccountRepository
    {
        Task<User> GetUserByName(string name);
    }
}