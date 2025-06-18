namespace Platform.API.Organizations.Domain.Model.ValueObjects;

public record CommercialName
{
    public string Name { get; }
    
    public CommercialName(string name)
    {
        if (name.Length > 200)
        {
            throw new ArgumentException("Name must be less than 200 characters", nameof(name));
        }
        Name = name.Trim();
    }
    public override string ToString() => Name;
}