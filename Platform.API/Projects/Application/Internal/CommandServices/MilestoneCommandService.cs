using Platform.API.Projects.Domain.Model.Aggregates;
using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Projects.Domain.Services;
using Platform.API.Shared.Domain.Repositories;

namespace Platform.API.Projects.Application.Internal.CommandServices;

public class MilestoneCommandService(
    IMilestoneRepository milestoneRepository,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : IMilestoneCommandService
{
    /// <summary>
    /// Handles the creation of a new milestone.
    /// </summary>
    /// <param name="command">
    /// The command containing the details to create the milestone, including the project ID, name, and description.
    /// </param>
    /// <returns>
    /// The newly created <see cref="Milestone"/> entity, or null if the creation failed.
    /// </returns>
    public async Task<Milestone?> Handle(CreateMilestoneCommand command)
    {
        var project = await projectRepository.FindById(command.ProjectId.Value);
        if (project is null)
        {
            throw new Exception($"Project {command.ProjectId.Value} not found");
        }
        if (command.DateRange.StartDate < project.DateRange.StartDate)
        {
            throw new Exception($"Milestone start date {command.DateRange.StartDate} cannot be before project start date {project.DateRange.StartDate}");
        }

        if (command.DateRange.StartDate > project.DateRange.EndDate)
        {
            throw new Exception($"Milestone start date {command.DateRange.StartDate} cannot be after project end date {project.DateRange.EndDate}");
        }

        if (command.DateRange.EndDate < project.DateRange.StartDate)
        {
            throw new Exception($"Milestone end date {command.DateRange.EndDate} cannot be before project start date {project.DateRange.StartDate}");
        }

        if (command.DateRange.EndDate > project.DateRange.EndDate)
        {
            throw new Exception($"Milestone end date {command.DateRange.EndDate} cannot be after project end date {project.DateRange.EndDate}");
        }


        var milestone = new Milestone(command);
        await milestoneRepository.AddAsync(milestone);
        await unitOfWork.CompleteAsync();
        return milestone;
    }
    
    /// <summary>
    /// Handles the update of the milestone name.
    /// </summary>
    /// <param name="command">
    /// The command containing the milestone ID and the new name for the milestone.
    /// </param>
    /// <returns>
    /// The updated <see cref="Milestone"/> entity, or null if the update failed.
    /// </returns>
    public async Task<Milestone?> Handle(UpdateMilestoneNameCommand command)
    {
        var milestone = await milestoneRepository.FindById(command.Id);
        if (milestone is null) throw new Exception($"Milestone with id {command.Id} not found");
        
        milestone.ReassignName(command.MilestoneName);
        milestoneRepository.Update(milestone);
        await unitOfWork.CompleteAsync();
        return milestone;
    }

    /// <summary>
    /// Handles the update of the milestone description.
    /// </summary>
    /// <param name="command">
    /// The command containing the milestone ID and the new description for the milestone.
    /// </param>
    /// <returns>
    /// The updated <see cref="Milestone"/> entity, or null if the update failed.
    /// </returns>
    public async Task<Milestone?> Handle(UpdateMilestoneDescriptionCommand command)
    {
        var milestone = await milestoneRepository.FindById(command.Id);
        if (milestone is null) throw new Exception($"Milestone with id {command.Id} not found");
        
        milestone.ReassignDescription(command.MilestoneDescription);
        milestoneRepository.Update(milestone);
        await unitOfWork.CompleteAsync();
        return milestone;
    }
    
    /// <summary>
    /// Handles the update of the milestone date range.
    /// </summary>
    /// <param name="command">
    /// The command containing the milestone ID and the new start and end dates for the milestone.
    /// </param>
    /// <returns>
    /// The updated <see cref="Milestone"/> entity, or null if the update failed.
    /// </returns>
    public async Task<Milestone?> Handle(UpdateMilestoneDateRangeCommand command)
    {
        var milestone = await milestoneRepository.FindById(command.Id);
        if (milestone is null) throw new Exception($"Milestone with id {command.Id} not found");
        
        milestone.ReassignDateRange(command.DateRange);
        milestoneRepository.Update(milestone);
        await unitOfWork.CompleteAsync();
        return milestone;
    }
}