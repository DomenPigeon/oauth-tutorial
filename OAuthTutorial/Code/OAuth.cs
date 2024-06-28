using System.Text;

namespace OAuthTutorial.Code;

public static class OAuth
{
    public static Uri GenerateAuthorizationUri(string authorizationEndpoint, string clientId, string codeVerifier, string redirectUri, string codeChallenge)
    {
        var uriBuilder = new StringBuilder();

        uriBuilder.Append(authorizationEndpoint);
        uriBuilder.Append('?');
        uriBuilder.Append("response_type=code&");
        uriBuilder.Append($"client_id={clientId}&");
        uriBuilder.Append($"state={codeVerifier}&");
        uriBuilder.Append($"redirect_uri={redirectUri}&");
        uriBuilder.Append($"code_challenge={codeChallenge}&");
        uriBuilder.Append("code_challenge_method=S256");

        return new Uri(uriBuilder.ToString());
    }

    public static HttpRequestMessage GenerateTokenPostRequest(string tokenEndpoint, string clientId, string clientSecret, string authorizationCode, string redirectUri, string codeVerifier)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);
        request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "authorization_code" },
            { "client_id", clientId },
            { "client_secret", clientSecret },
            { "code", authorizationCode },
            { "redirect_uri", redirectUri },
            { "code_verifier", codeVerifier }
        });
        
        return request;
    }
}