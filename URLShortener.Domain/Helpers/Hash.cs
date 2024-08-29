using System.Security.Cryptography;
using System.Text;

namespace URLShortener.Domain.Helpers;

public static class Hash
{
    public static string Sha256(string input)
    {
        byte[] urlBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = SHA256.HashData(urlBytes);

        var hashStringBuilder = new StringBuilder();
        foreach (byte b in hashBytes)
        {
            hashStringBuilder.Append(b.ToString("x2"));
        }

        return hashStringBuilder.ToString();
    }
}
