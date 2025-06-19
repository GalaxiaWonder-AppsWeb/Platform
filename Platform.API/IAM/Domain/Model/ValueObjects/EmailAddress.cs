namespace Platform.API.IAM.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing an email address.
/// </summary>
/// <param name="Address">The string value of the email address.</param>
public record EmailAddress(string Address)
{
    public EmailAddress() : this(string.Empty){}
};