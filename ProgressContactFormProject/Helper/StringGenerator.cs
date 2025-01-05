using System;
using System.Text;

public static class StringGenerator
{
    private static string? _longMessage;

    /// <summary>
    /// Generates a random string of the specified length.
    /// </summary>
    /// <param name="length">The length of the string to generate.</param>
    /// <returns>A random string.</returns>
    public static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        StringBuilder result = new StringBuilder(length);
        Random random = new Random();

        for (int i = 0; i < length; i++)
        {
            result.Append(chars[random.Next(chars.Length)]);
        }

        return result.ToString();
    }

    /// <summary>
    /// Provides a pre-generated long message of 2001 characters.
    /// </summary>
    public static string LongMessage
    {
        get
        {
            if (_longMessage == null)
            {
                _longMessage = GenerateRandomString(2001);
            }
            return _longMessage;
        }
    }
}