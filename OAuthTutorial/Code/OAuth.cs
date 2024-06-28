using System.Text;

namespace OAuthTutorial.Code;

public static class OAuth
{
    public static Uri GenerateAuthorizationUri(string authorizationServerUri, string clientId, string state, string redirectUri, string codeChallenge)
    {
        var uriBuilder = new StringBuilder();

        uriBuilder.Append(authorizationServerUri);
        uriBuilder.Append('?');
        uriBuilder.Append("response_type=code&");
        uriBuilder.Append($"client_id={clientId}&");
        uriBuilder.Append($"state={state}&");
        uriBuilder.Append($"redirect_uri={redirectUri}&");
        uriBuilder.Append($"code_challenge={codeChallenge}&");
        uriBuilder.Append("code_challenge_method=S256");

        return new Uri(uriBuilder.ToString());
    }
}