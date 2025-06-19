namespace Platform.API.IAM.Domain.Model.Commands;

/**
 * <summary>command object used to trigger the seeding of default user types in the system.
 * This command contains no additional data and serves as a signal to perform the operation.</summary>
 */
public record SeedUserTypeCommand()
{
};