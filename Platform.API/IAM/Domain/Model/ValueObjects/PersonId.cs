namespace Platform.API.IAM.Domain.Model.ValueObjects;

public record PersonId(long personId)
{
    public PersonId() : this(0L){}
};