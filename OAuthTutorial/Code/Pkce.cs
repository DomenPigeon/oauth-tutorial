using Microsoft.IdentityModel.Tokens;

namespace OAuthTutorial.Code;

public static class Pkce
{
    // https://datatracker.ietf.org/doc/html/rfc7636#section-4.1
    public static string GenerateCodeVerifier(int length = 43)
    {
        if (length is < 43 or > 128)
        {
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be between 43 and 128 characters.");
        }

        var bytes = new byte[200];
        Random.Shared.NextBytes(bytes);
        return Base64UrlEncoder.Encode(bytes)[..length];
    }
}