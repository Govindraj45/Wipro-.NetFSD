namespace DatabaseSecurityDemo.Models;

public class SecureCustomerRecord
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public string EncryptedSensitiveNote { get; set; } = string.Empty;
    public string DataIntegrityHash { get; set; } = string.Empty;
}
