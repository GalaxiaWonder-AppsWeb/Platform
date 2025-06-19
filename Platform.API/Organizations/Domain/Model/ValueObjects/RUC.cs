namespace Platform.API.Organizations.Domain.Model.ValueObjects;

/// <summary>
/// Represents a Peruvian RUC (Registro Único de Contribuyentes).
/// </summary>
public record RUC
{
    /// <summary>
    /// Gets the RUC number as a string.
    /// </summary>
    public string Number { get; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="RUC"/> record with the specified RUC number.
    /// </summary>
    /// <param name="number">The RUC number, which must be exactly 11 digits and start with "10" or "20".</param>
    /// <exception cref="ArgumentException">
    /// Thrown when the RUC number is null, empty, not 11 digits, or does not start with "10" or "20".
    /// </exception>
    public RUC(string number)
    {
        if (string.IsNullOrWhiteSpace(number))
        {
            throw new ArgumentException("RUC number is required");
        }
        if (number.Length != 11)
        {
            throw new ArgumentException("RUC number must be 11 digits");
        }
        if (!number.StartsWith("10") && !number.StartsWith("20"))
        {
            throw new ArgumentException("RUC number must start with 10 or 20");
        }
        Number = number;
    }
    
    /// <summary>
    /// Returns the string representation of the RUC number.
    /// </summary>
    /// <returns>The RUC number as a string.</returns>
    public override string ToString() => Number;
}