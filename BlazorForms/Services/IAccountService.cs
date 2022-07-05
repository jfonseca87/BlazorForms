using BlazorForms.Models;
using System.Threading.Tasks;

namespace BlazorForms.Services
{
    public interface IAccountService
    {
        Task<ApiResponse<User>> ValidateUser();
    }
}