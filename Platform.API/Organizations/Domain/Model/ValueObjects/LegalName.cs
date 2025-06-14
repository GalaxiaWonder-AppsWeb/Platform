namespace Platform.API.Organizations.Domain.Model.ValueObjects;

public record LegalName
{
    public string Name { get; }

    public LegalName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name is required", nameof(name));
        if (name.Length > 500)
            throw new ArgumentException("Name must be less than 500 characters", nameof(name));
        Name = name.Trim();
    }
    
    public override string ToString() => Name;
}