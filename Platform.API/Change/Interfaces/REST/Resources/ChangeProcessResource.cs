namespace Platform.API.Change.Interfaces.REST.Resources;

public record ChangeProcessResource(long Id, string Origin, string Status, string Justification, string? Response, long ProjectId);