namespace Platform.API.IAM.Domain.Model.ValueObjects;

public record Password(string HashedPassword)
{
    public Password() : this(string.Empty){}
}