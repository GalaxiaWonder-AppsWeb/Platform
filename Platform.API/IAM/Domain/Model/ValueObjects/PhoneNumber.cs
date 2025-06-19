using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace Platform.API.IAM.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a validated international phone number in E.164 format.
/// </summary>
public record PhoneNumber
{
    /// <summary>
    ///     Regular expression pattern used to validate E.164 formatted phone numbers.
    /// </summary>
    private static readonly Regex E164Pattern = new(@"^\+\d{7,15}$", RegexOptions.Compiled);

    /// <summary>
    ///     Gets the validated phone number string in E.164 format (e.g., +51987654321).
    /// </summary>
    [Column("phone_number", TypeName = "VARCHAR(16)")]
    public string Phone { get; }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PhoneNumber"/> record with the specified phone string.
    /// </summary>
    /// <param name="phone">The phone number string to validate and assign.</param>
    /// <exception cref="ArgumentNullException">Thrown when the phone number is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the phone number does not match the E.164 format.</exception>
    public PhoneNumber(string phone)
    {
        if (phone is null)
            throw new ArgumentNullException(nameof(phone), "Phone number cannot be null");

        if (!E164Pattern.IsMatch(phone))
            throw new ArgumentException("Phone number must be in international E.164 format (e.g., +51987654321)", nameof(phone));

        Phone = phone;
    }

    /// <summary>
    ///     Returns the string representation of the phone number.
    /// </summary>
    /// <returns>The phone number string.</returns>
    public override string ToString() => Phone;
}