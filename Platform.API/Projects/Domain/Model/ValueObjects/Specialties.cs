namespace Platform.API.Projects.Domain.Model.ValueObjects;

/// <summary>
/// Enumeration that defines the profesiional specialities available
/// within the context oh technical projects and engineering disciplines.
/// </summary>
public enum Specialties
{
    /// Architectural design and planning.
    ARCHITECTURE,
    /// Structural engineering and load-bearing calculations.
    STRUCTURES,
    /// Health, safety, and accesibility (HSA) compliance and review.
    HSA,
    /// Surveying and topographic analysis.
    TOPOGRAPHY,
    /// Sanitation and water infrastructure design.
    SANITATION,
    /// Electrical systems design and planning.
    ELECTRICITY,
    /// Communication and data network systems.
    COMMUNICATIONS,
    /// Non applicable
    NON_APPLICABLE
}