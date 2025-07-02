using Platform.API.IAM.Domain.Model.ValueObjects;
using Platform.API.Projects.Domain.Model.Entities;
using Platform.API.Projects.Domain.Model.ValueObjects;
using TaskStatus = Platform.API.Projects.Domain.Model.Entities.TaskStatus;


namespace Platform.API.Projects.Domain.Model.Commands;

public record CreateTaskCommand(MilestoneItemName Name, Description Description, DateRange DateRange, MilestoneId MilestoneId, Specialty Specialty, TaskStatus? Status, PersonId? PersonId);