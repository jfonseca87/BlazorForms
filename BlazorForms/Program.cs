using Blazored.Toast;
using BlazorForms.Auth;
using BlazorForms.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorForms
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000") });
            builder.Services.AddSingleton<ILocalStorageService, LocalStorageService>();
            builder.Services.AddTransient<IAccountService, AccountService>();
            builder.Services.AddTransient<IPersonService, PersonService>();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, FakeAuthStateProvider>();

            builder.Services.AddBlazoredToast();

            await builder.Build().RunAsync();
        }
    }
}
