namespace Platform.API.IAM.Domain.Model.ValueObjects;

public record ProfessionalId(long professionalId)
{
    public ProfessionalId() : this(0L){}
};