using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Platform.API.IAM.Domain.Model.ValueObjects;

public record PhoneNumber
{
    private static readonly Regex E164Pattern = new(@"^\+\d{7,15}$", RegexOptions.Compiled);

    [Column("phone_number", TypeName = "VARCHAR(16)")]
    public string Phone { get; }

    public PhoneNumber(string phone)
    {
        if (phone is null)
            throw new ArgumentNullException(nameof(phone), "Phone number cannot be null");

        if (!E164Pattern.IsMatch(phone))
            throw new ArgumentException("Phone number must be in international E.164 format (e.g., +51987654321)", nameof(phone));

        Phone = phone;
    }

    public override string ToString() => Phone;
}