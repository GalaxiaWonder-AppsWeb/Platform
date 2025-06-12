namespace Platform.API.IAM.Domain.Model.ValueObjects;

public record UserName(string Username)
{
    public UserName() : this(string.Empty){}
}