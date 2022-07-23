using System.Threading.Tasks;

namespace BlazorForms.Services
{
    public interface ILocalStorageService
    {
        Task Clear();
        Task DeleteValue(string key);
        Task<string> GetStringValue(string key);
        Task SetValue(string key, string value);
    }
}