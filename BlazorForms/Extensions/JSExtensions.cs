using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorForms.Extensions
{
    public static class JSExtensions
    {
        public static async Task ShowAlert(this JSRuntime context, string message)
        {
            await context.InvokeVoidAsync("alert", message);
        }
        
        public static async Task<bool> Confirm(this JSRuntime context, string message)
        {
            return await context.InvokeAsync<bool>("confirm", message);
        }
    }
}
