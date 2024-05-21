using BCrypt.Net;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Services;

public class PasswordHasherService : IPasswordHasherService
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA512);
    }

    public bool EnhancedHash(string value, string hash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(value, hash, HashType.SHA512);
    }
}