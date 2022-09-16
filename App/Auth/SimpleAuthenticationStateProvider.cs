using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace App.Auth;

public class SimpleAuthenticationStateProvider : AuthenticationStateProvider
{
    private const string AuthenticationType = "SimpleAuth";
    private const string UserSessionKey = "UserSession";
    private ClaimsPrincipal _principal = new(new ClaimsIdentity());
    private readonly ProtectedSessionStorage _protectedSessionStorage;

    public SimpleAuthenticationStateProvider(ProtectedSessionStorage protectedSessionStorage)
    {
        _protectedSessionStorage = protectedSessionStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        try
        {
            var storageResult = await _protectedSessionStorage.GetAsync<UserSession>(UserSessionKey);
            var session = storageResult.Success ? storageResult.Value : null;
            if (session is null) return await Task.FromResult(new AuthenticationState(_principal));
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, session.Id.ToString()),
                new Claim(ClaimTypes.Role, session.Role),
            }, AuthenticationType));
            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
        catch (Exception e)
        {
            // Console.WriteLine(e);
            return await Task.FromResult(new AuthenticationState(_principal));
        }
    }

    public async Task UpdateAuthenticationState(UserSession? userSession)
    {
        if (userSession is null)
        {
            await _protectedSessionStorage.DeleteAsync(UserSessionKey);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_principal)));
            return;
        }

        await _protectedSessionStorage.SetAsync(UserSessionKey, userSession);
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, userSession.Id.ToString()),
            new Claim(ClaimTypes.Role, userSession.Role),
        }));
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
    }
}