using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

public class MyAuthenticationStateProvider : AuthenticationStateProvider {
    public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
        return await Task.FromResult(new AuthenticationState(AnonymousUser));
    }

    //Anonymous user can has no claims but requires an empty identity
    private ClaimsPrincipal AnonymousUser => new(new ClaimsIdentity(Array.Empty<Claim>()));


    public void SignOut() {
        var result = Task.FromResult(new AuthenticationState(AnonymousUser));
        NotifyAuthenticationStateChanged(result);
    }


    public void SignIn( string email, string role)
    {
        var claims = new[] {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, role),
            };
        var identity = new ClaimsIdentity(claims);

        var principal = new ClaimsPrincipal(identity);

        var result = Task.FromResult(new AuthenticationState(principal));
        NotifyAuthenticationStateChanged(result);
    }

    public class TokenProvider
    {
        public string? accessToken { get; set; }

        public string? refreshToken { get; set; }
    }


}