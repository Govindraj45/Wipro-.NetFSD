using DatabaseSecurityDemo.Models;

namespace DatabaseSecurityDemo.Services;

public class CustomerSecurityRepository(CredentialService credentials)
{
    private readonly List<SecureCustomerRecord> _records = [];
    private int _nextId = 1;

    public SecureCustomerRecord Add(SecureCustomerInput input)
    {
        var normalizedUser = input.UserName.Trim();
        var normalizedEmail = input.Email.Trim();
        var normalizedNote = input.SensitiveNote.Trim();

        var record = new SecureCustomerRecord
        {
            Id = _nextId++,
            UserName = normalizedUser,
            Email = normalizedEmail,
            PasswordHash = credentials.HashPassword(input.Password),
            EncryptedSensitiveNote = credentials.Encrypt(normalizedNote),
            DataIntegrityHash = credentials.CreateHmac($"{normalizedUser}|{normalizedEmail}|{normalizedNote}")
        };

        _records.Add(record);
        return record;
    }

    public IReadOnlyList<object> GetAuditSafeView()
    {
        return _records.Select(record => new
        {
            record.Id,
            record.UserName,
            record.Email,
            SensitivePreview = credentials.Decrypt(record.EncryptedSensitiveNote),
            record.DataIntegrityHash
        }).Cast<object>().ToList();
    }
}
