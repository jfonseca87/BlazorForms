using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorForms.Services
{
    public class LocalStorageService : ILocalStorageService
    {
        private readonly IJSRuntime _js;

        public LocalStorageService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SetValue(string key, string value)
        {
            await _js.InvokeVoidAsync("localStorage.setItem", key, value);
        }

        public async Task DeleteValue(string key)
        {
            await _js.InvokeVoidAsync("localStorage.removeItem", key);
        }

        public async Task Clear()
        {
            await _js.InvokeVoidAsync("localStorage.clear");
        }

        public async Task<string> GetStringValue(string key)
        {
            return await _js.InvokeAsync<string>("localStorage.getItem", key);
        }
    }
}
