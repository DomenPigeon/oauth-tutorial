using Microsoft.AspNetCore.Mvc;
using OAuthTutorial.Code;

namespace OAuthTutorial.Controllers;

[Route("redirect")]
public class RedirectCodeController : ControllerBase
{
    private readonly RedirectService _redirectService;

    public RedirectCodeController(RedirectService redirectService)
    {
        _redirectService = redirectService;
    }

    // Example
    // https://localhost:7206/redirect?code=62KyU-ZCyG6-tV9_C6YEOlo1MF8xJ4Afcq62sd80Rv6MJ&state=Qi0kQJ_QHXJzVUdNvlgQGcA3WAwoY05wpm4jmc6nu5U
    [HttpGet]
    public ContentResult Get([FromQuery] string code, [FromQuery] string state)
    {
        var redirectResponse = new RedirectResponse(code, state);
        _redirectService.SetRedirectResponse(redirectResponse);

        return new ContentResult
        {
            ContentType = "text/html",
            Content = $"<pre>You can close this tab now and go back to the main page.\nCode: {code}\nState: {state}</pre>"
        };
    }
}