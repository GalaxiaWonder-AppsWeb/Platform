namespace Platform.API.Projects.Domain.Model.ValueObjects;

/// <summary>
/// Represents a generic range of time during which somethings is being done or must be done.
/// Internally wraps a pair of <see cref="DateTimeOffset"/>, representing the start and end dates.
/// </summary>
public record DateRange
{
    /// <summary>
    /// The initial starting date
    /// </summary>
    public DateTimeOffset StartDate { get; }
    
    /// <summary>
    /// The finishing date
    /// </summary>
    public DateTimeOffset EndDate { get; }
    
    /// <summary>
    /// Compact constructor that validates the date range
    /// </summary>
    /// <param name="startDate"> Initial starting Date </param>
    /// <param name="endDate"> Finishing date </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="startDate"/> is later than <paramref name="endDate"/>,
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Thrown when the <paramref name="startDate"/> is in the past.
    /// </exception>
    public DateRange(DateTimeOffset startDate, DateTimeOffset endDate)
    {
        if (startDate > endDate)
            throw new ArgumentException("Start date must be earlier than end date", nameof(startDate));
        if (startDate < DateTimeOffset.Now)
            throw new ArgumentException("Start date cannot be in the past", nameof(startDate));
        
        StartDate = startDate;
        EndDate = endDate;
    }
}