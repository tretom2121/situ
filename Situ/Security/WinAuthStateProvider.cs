using Microsoft.AspNetCore.Components.Authorization;
using Situ.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Situ.Security
{
    public class WinAuthStateProvider : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            var userName = user.Split('\\')[1];

            #if DEBUG
            userName = "AdminTom";
            #endif

            var userService = new UserService();
            List<string> roles = await userService.GetRoles(userName);

            if (roles?.Count > 0)
            {
                var claims = new List<Claim>() { new Claim(ClaimTypes.Name, userName) };
                foreach (var role in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }

                var WindowsAuth = new ClaimsIdentity(claims, "windows");

                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(WindowsAuth)));
            }
            else
            {
                var anonymous = new ClaimsIdentity();
                return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));
            }
        }
    }
}
