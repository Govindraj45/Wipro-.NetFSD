using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;

namespace DatabaseSecurityDemo.Services;

public class CredentialService(IDataProtectionProvider protectionProvider, IConfiguration configuration)
{
    private readonly IDataProtector _protector = protectionProvider.CreateProtector("DatabaseSecurityDemo.CustomerData");
    private readonly PasswordHasher<object> _hasher = new();

    public string HashPassword(string password)
    {
        return _hasher.HashPassword(new object(), password);
    }

    public string Encrypt(string value) => _protector.Protect(value);

    public string Decrypt(string value) => _protector.Unprotect(value);

    public string CreateHmac(string value)
    {
        var key = Encoding.UTF8.GetBytes(configuration["Security:HmacKey"] ?? "fallback-key");
        using var hmac = new HMACSHA256(key);
        return Convert.ToHexString(hmac.ComputeHash(Encoding.UTF8.GetBytes(value)));
    }
}
