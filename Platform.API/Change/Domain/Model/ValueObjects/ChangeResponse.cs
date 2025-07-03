namespace Platform.API.Change.Domain.Model.ValueObjects;

/// <summary>
/// Represents a response to a change request or technical query.
/// </summary>
public record ChangeResponse
{
    public string Value { get; init; }

    public ChangeResponse(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Response cannot be null or whitespace.", nameof(value));
        }
        if (value.Length > 100)
        {
            throw new ArgumentException("Response cannot exceed 100 characters.", nameof(value));
        }
        
        Value = value;
    }
}