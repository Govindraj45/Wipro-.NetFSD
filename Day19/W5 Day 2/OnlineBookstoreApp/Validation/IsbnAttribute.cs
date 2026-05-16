using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace OnlineBookstoreApp.Validation;

public partial class IsbnAttribute : ValidationAttribute
{
    public IsbnAttribute()
    {
        ErrorMessage = "ISBN must be 10 or 13 digits, optionally separated by hyphens.";
    }

    public override bool IsValid(object? value)
    {
        if (value is not string isbn)
        {
            return false;
        }

        var normalized = isbn.Replace("-", string.Empty).Trim();
        return IsbnPattern().IsMatch(normalized);
    }

    [GeneratedRegex(@"^(\d{10}|\d{13})$")]
    private static partial Regex IsbnPattern();
}
