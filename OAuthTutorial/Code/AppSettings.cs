namespace OAuthTutorial.Code;

public class AppSettings
{
    /// <summary>
    /// Should look something like: https://dev-xxxxxxx.us.auth0.com/authorize
    /// </summary>
    public required string? AuthorizationEndpoint { get; set; }

    /// <summary>
    /// Should look something like: tMRjdLmDNMALpzCS8PDNHrjBLZu0vQui
    /// </summary>
    public required string? ClientId { get; set; }

    /// <summary>
    /// Should look something like: FgEzG44O2QsrTtie2PTm4D6BtEBDQa2o_eOMOp_m8J1Q6yQgJ74zwhSmQ5xwtjaZ
    /// </summary>
    public required string? ClientSecret { get; set; }

    public string RedirectUri { get; set; } = "https://localhost:7206/redirect";
}