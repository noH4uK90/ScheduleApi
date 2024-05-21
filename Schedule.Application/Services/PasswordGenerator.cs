using System.Security.Cryptography;
using Schedule.Application.Common.Interfaces;

namespace Schedule.Application.Services;

public class PasswordGenerator : IPasswordGenerator
{
    public string Generate(int length)
    {
        var randomNumber = new byte[length];
        using var numberGenerator = RandomNumberGenerator.Create();
        numberGenerator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}