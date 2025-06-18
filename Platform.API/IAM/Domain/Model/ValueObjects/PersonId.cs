namespace Platform.API.IAM.Domain.Model.ValueObjects;

public class PersonId
{
    public long personId { get; private set; }

    public PersonId(long value)
    {
        personId = value;
    }

    // Constructor vacío requerido por EF Core
    private PersonId() { }

    public override string ToString() => personId.ToString();
}

