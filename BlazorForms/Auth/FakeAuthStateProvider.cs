using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorForms.Auth
{
    public class FakeAuthStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            Console.WriteLine("AuthenticationStateProvider component called first!");
            // await Task.Delay(3000);
            // var identity = new ClaimsIdentity(new List<Claim> 
            // {
            //     new Claim(ClaimTypes.Sid, "1"),
            //     new Claim(ClaimTypes.Name, "Jorge") 
            // }, "Test");
            var identity = new ClaimsIdentity();
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
        }
    }
}
