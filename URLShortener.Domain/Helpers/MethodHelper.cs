namespace URLShortener.Domain.Helpers;

public static class MethodHelper
{
    public static string GenerateShortened()
    {
        return GenerateRandomString(5);
    }

    private static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[length];

        for (int i = 0; i < length; i++)
        {
            stringChars[i] = chars[Random.Shared.Next(chars.Length)];
        }
        Random.Shared.Shuffle(stringChars);

        return new string(stringChars);
    }
}
