using OAuthTutorial.Code;
using Xunit.Abstractions;

namespace Tests;

public class PkceTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void WorksForAllRanges()
    {
        for (var i = 32; i < 97; i++)
        {
            var (verifier, challenge) = Pkce.GenerateCodeVerifierAndChallenge(i);
            testOutputHelper.WriteLine($"Bytes: {i:00}, Verifier length: {verifier.Length}, Verifier: {verifier}, Challenge: {challenge}");
        }
    }

    [Fact]
    public void Generate()
    {
        var (verifier, challenge) = Pkce.GenerateCodeVerifierAndChallenge();
        testOutputHelper.WriteLine($"Verifier: {verifier}\nChallenge: {challenge}");
    }
}