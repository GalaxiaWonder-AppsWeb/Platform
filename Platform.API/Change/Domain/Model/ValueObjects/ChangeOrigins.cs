namespace Platform.API.Change.Domain.Model.ValueObjects;

/// <summary>
/// Represents the origin of a change process.
/// </summary>
public enum ChangeOrigins
{
    /// <summary>
    /// When the change is initiated when the project is not finishes.
    /// </summary>
    CHANGE_REQUEST,
    /// <summary>
    /// When the change is initiated when the project is finished.
    /// </summary>
    TECHNICAL_QUERY
}