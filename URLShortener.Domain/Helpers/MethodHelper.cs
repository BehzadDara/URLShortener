namespace URLShortener.Domain.Helpers;

public static class MethodHelper
{
    public static string GenerateShortened(List<string> shorteneds)
    {
        string result;

        do
        {
            result = GenerateRandomString(5);
        } while (shorteneds.Contains(result));

        return result;
    }

    private static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var random = new Random();
        var stringChars = new char[length];

        for(int i = 0; i < length; i++)
        {
            stringChars[i] = chars[Random.Shared.Next(chars.Length)];
        }

        Random.Shared.Shuffle(stringChars);

        //for (int i = 0; i < stringChars.Length; i++)
        //{
        //    stringChars[i] = chars[random.Next(chars.Length)];
        //}

        return new string(stringChars);
    }
}
