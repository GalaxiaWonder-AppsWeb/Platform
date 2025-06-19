namespace Platform.API.IAM.Domain.Model.ValueObjects;

/// <summary>
///     Value object representing a unique professional identifier.
/// </summary>
/// <param name="professionalId">The numeric value of the professional identifier.</param>
public record ProfessionalId(long professionalId)
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ProfessionalId"/> record with a default value of 0.
    /// </summary>
    public ProfessionalId() : this(0L){}
};