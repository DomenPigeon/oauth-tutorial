global using CodeVerifier = string;
global using CodeChallenge = string;
using System.Security.Cryptography;
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

        Span<byte> bytes = stackalloc byte[size];
        Span<char> codeVerifier = stackalloc char[(size + 2) / 3 * 4];
        Random.Shared.NextBytes(bytes);
        var written = Base64UrlEncoder.Encode(bytes, codeVerifier);

        codeVerifier = codeVerifier[..written];

        var challengeBytes = SHA256.HashData(bytes);
        var challenge = Base64UrlEncoder.Encode(challengeBytes);

        return (codeVerifier.ToString(), challenge);
    }
}