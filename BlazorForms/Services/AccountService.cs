using Blazored.Toast.Services;
using BlazorForms.Extensions;
using BlazorForms.Models;
using BlazorForms.Utils;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorForms.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly IToastService _toastService;
        private readonly IConfiguration _configuration;

        public AccountService(HttpClient httpClient, IToastService toastService, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _toastService = toastService;
            _configuration = configuration;
        }

        public async Task<ApiResponse<User>> ValidateUser()
        {
            // create request object and pass windows authentication credentials
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_configuration["baseUrlAuth"]}/api/account");
            request.SetBrowserRequestCredentials(BrowserRequestCredentials.Include);

            // send the request and convert the results to a list
            var response = await _httpClient.SendAsync(request).ConfigureAwait(false);
            var (status, data, errors) = response.ToClientResponseV2();

            if (status == ResponseStatus.Error)
            {
                Console.WriteLine(JsonConvert.SerializeObject(errors));
                _toastService.ShowError("An error ocurred getting user account");
            }

            if (status == ResponseStatus.Unauthorized)
            {
                _toastService.ShowError(errors.FirstOrDefault());
            }


            return new ApiResponse<User>
            {
                Status = status,
                Errors = errors,
                Data = status == ResponseStatus.Success
                    ? JsonConvert.DeserializeObject<User>(data)
                    : null
            };
        }
    }
}
