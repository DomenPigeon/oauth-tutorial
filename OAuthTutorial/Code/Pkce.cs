global using CodeVerifier = string;
global using CodeChallenge = string;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace OAuthTutorial.Code;

/// <summary>
/// Provides a randomly generated code verifier and challenge of PKCE (Proof Key for Code Exchange) implementation.
/// </summary>
public static class Pkce
{
    /// <summary>
    /// Generates a Code Verifier and a Code Challenge for PKCE (Proof Key for Code Exchange), as described in the RFC 7636
    /// Code Verifier docs: https://datatracker.ietf.org/doc/html/rfc7636#section-4.1
    /// Code Challenge docs: https://datatracker.ietf.org/doc/html/rfc7636#section-4.2
    /// </summary>
    /// <param name="size">Code verifier size in bytes, min 32 corresponds to a final length of 43 chars and max 96 to 128 chars.</param>
    /// <returns>A tuple of two strings (CodeVerifier, CodeChallenge) </returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the size is less than 32 or greater than 96.</exception>
    public static (CodeVerifier, CodeChallenge) GenerateCodeVerifierAndChallenge(int size = 32)
    {
        if (size is < 32 or > 96)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "The length must be between 43 and 128 characters.");
        }

        // 1.) Get random bytes
        Span<byte> randomBytes = stackalloc byte[size];
        Random.Shared.NextBytes(randomBytes);

        // 2.) Base64Url encode the random bytes
        Span<char> codeVerifier = stackalloc char[(size + 2) / 3 * 4];
        var written = Base64UrlEncoder.Encode(randomBytes, codeVerifier);
        codeVerifier = codeVerifier[..written];

        // 3.) Get bytes from the new encoding
        Span<byte> codeVerifierBytes = stackalloc byte[size * 2];
        written = Encoding.UTF8.GetBytes(codeVerifier, codeVerifierBytes);
        codeVerifierBytes = codeVerifierBytes[..written];

        // 4.) SHA256 hash the codeVerifier bytes
        var challengeBytes = SHA256.HashData(codeVerifierBytes);

        // 5.) Base64Url encode hash
        var challenge = Base64UrlEncoder.Encode(challengeBytes);


        return (codeVerifier.ToString(), challenge);
    }
}