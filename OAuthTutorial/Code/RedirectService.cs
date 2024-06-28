namespace OAuthTutorial.Code;

public record RedirectResponse(string AuthorizationCode, string State);

public class RedirectService
{
    private TaskCompletionSource<RedirectResponse> _awaitNextRedirectResponse = new();

    public void SetRedirectResponse(RedirectResponse response)
    {
        _awaitNextRedirectResponse.SetResult(response);
        _awaitNextRedirectResponse = new();
    }

    public Task<RedirectResponse> GetNextRedirectResponseAsync() => _awaitNextRedirectResponse.Task;
}