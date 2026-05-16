namespace DatabaseSecurityDemo.Services;

public class SecurityAssessmentService(IConfiguration configuration)
{
    public object GetAssessmentSummary()
    {
        return new
        {
            encryptedConnection = configuration.GetConnectionString("SecureDb"),
            secureCodingChecklist = new[]
            {
                "Use parameterized queries or EF Core for all data access.",
                "Never log passwords, tokens, or decrypted sensitive fields.",
                "Run static analysis and security review before deployment.",
                "Include penetration testing for SQL injection and XSS scenarios.",
                "Restrict database permissions using least privilege and RBAC."
            },
            sdlcStages = new[]
            {
                "Design: threat modeling and encryption planning.",
                "Development: secure coding standards and peer review.",
                "Testing: automated scans and penetration testing.",
                "Deployment: secrets management, auditing, and monitoring."
            }
        };
    }
}
