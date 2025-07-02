using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Projects.Domain.Services;
using Platform.API.Shared.Domain.Repositories;
using Task = Platform.API.Projects.Domain.Model.Aggregates.Task;

namespace Platform.API.Projects.Application.Internal.CommandServices;

public class TaskCommandService(
    ITaskRepository taskRepository,
    ITaskStatusRepository taskStatusRepository,
    IMilestoneRepository milestoneRepository,
    ISpecialtyRepository specialtyRepository,
    IUnitOfWork unitOfWork) : ITaskCommandService
{
    public async Task<Task?> Handle(CreateTaskCommand command)
    {
        var milestone = await milestoneRepository.FindById(command.MilestoneId.Value);
        var status = await taskStatusRepository.FindByName(command.Status.Name.ToString());
        var specialty = await specialtyRepository.FindByName(command.Specialty.Name.ToString());
        if (specialty is null)
        {
            throw new Exception($"Specialty {command.Specialty.Name} not found");
        }
        if (status is null)
        {
            throw new Exception($"Task status {command.Status.Name} not found");
        }
        if (milestone is null)
        {
            throw new Exception($"Milestone {command.MilestoneId.Value} not found");
        }
        if (command.DateRange.StartDate < milestone.DateRange.StartDate ||
            command.DateRange.EndDate > milestone.DateRange.EndDate)
        {
            throw new Exception($"Task date range {command.DateRange} is outside of milestone date range {milestone.DateRange}");
        }

        
        var task = new Task(command);
        task.SetSpecialty(specialty);
        task.ReassignStatus(status);
        
        if (command.PersonId is null || command.Status is null)
        {
            var reassignedStatus = await taskStatusRepository.FindByName("DRAFT");
            if (reassignedStatus is null)
            {
                throw new Exception("Draft status not found");
            }
            task.ToDraft(reassignedStatus);
        }
        
        await taskRepository.AddAsync(task);
        await unitOfWork.CompleteAsync();
        return task;
    }
    
    public async Task<Task?> Handle(UpdateTaskCommand command)
    {
        var task = await taskRepository.FindById(command.Id);
        if (task is null)
        {
            throw new Exception($"Task {command.Id} not found");
        }
        if (command.Name is not null)
        {
            task.ReassignName(command.Name);
        }
        if (command.Description is not null)
        {
            task.ReassignDescription(command.Description);
        }
        if (command.Status is not null)
        {
            var reassignedStatus = await taskStatusRepository.FindByName(command.Status.Name.ToString());
            if (reassignedStatus is null)
            {
                throw new Exception($"Task status {command.Status.Name} not found");
            }
            task.ReassignStatus(reassignedStatus);
        }

        var milestone = await milestoneRepository.FindById(task.MilestoneId.Value);
        if (milestone is null)
        {
            throw new Exception($"Milestone {task.MilestoneId.Value} not found");
        }

        if (command.DateRange is not null)
        {
            if (command.DateRange.StartDate < milestone.DateRange.StartDate ||
                command.DateRange.EndDate > milestone.DateRange.EndDate)
            {
                throw new Exception($"Task date range {command.DateRange} is outside of milestone date range {milestone.DateRange}");
            }
            task.ReassignDateRange(command.DateRange);
        }

        if (command.RemovePerson)
        {
            var reassignedStatus = await taskStatusRepository.FindByName("DRAFT");
            if (reassignedStatus is null)
            {
                throw new Exception("Draft status not found");
            }
            task.ToDraft(reassignedStatus);
        }
        else if (command.PersonId is not null)
        {
            task.ReassignPerson(command.PersonId);
        }
        taskRepository.Update(task);
        await unitOfWork.CompleteAsync();
        return task;
    }
}