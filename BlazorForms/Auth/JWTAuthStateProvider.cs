using BlazorForms.Models;
using BlazorForms.Services;
using BlazorForms.Utils;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorForms.Auth
{
    public class JWTAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IAccountService _accountService;
        private readonly ILocalStorageService _localStorageService;
        private readonly HttpClient _httpClient;

        public JWTAuthStateProvider(IAccountService accountService, ILocalStorageService localStorageService, HttpClient httpClient)
        {
            _accountService = accountService;
            _localStorageService = localStorageService;
            _httpClient = httpClient;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            (bool result, string token) = await ValidateUser();
            if (!result)
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            ClaimsIdentity identity = SetTokenClaims(token);
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }

        private async Task<(bool response, string token)> ValidateUser()
        {
            ApiResponse<User> result = await _accountService.ValidateUser();
            if (result.Status == ResponseStatus.Unauthorized || result.Status == ResponseStatus.Error)
            {
                return (false, null);
            }

            await _localStorageService.SetValue("token", result.Data.Token);
            return (true, result.Data.Token);
        }

        private ClaimsIdentity SetTokenClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var asd = tokenHandler.ReadJwtToken(token);

            return new ClaimsIdentity(asd.Claims, "JWTAuth");
        }
    }
}
