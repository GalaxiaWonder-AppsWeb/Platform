using Platform.API.Billings.Interfaces.ACL;
using Platform.API.IAM.Interfaces.ACL;
using Platform.API.Projects.Domain.Model.Commands;
using Platform.API.Projects.Domain.Repositories;
using Platform.API.Projects.Domain.Services;
using Platform.API.Shared.Domain.Repositories;
using TaskD = Platform.API.Projects.Domain.Model.Aggregates.Task;

namespace Platform.API.Projects.Application.Internal.CommandServices;

public class TaskCommandService(
    ITaskRepository taskRepository,
    ITaskStatusRepository taskStatusRepository,
    IMilestoneRepository milestoneRepository,
    ISpecialtyRepository specialtyRepository,
    IIAMContextFacade iamFacade,
    ITaskBudgetFacade taskBudgetFacade,
    IUnitOfWork unitOfWork) : ITaskCommandService
{
    public async Task<TaskD?> Handle(CreateTaskCommand command)
    {
        var milestone = await milestoneRepository.FindById(command.MilestoneId.Value);
        if (milestone is null)
        {
            throw new Exception($"Milestone {command.MilestoneId.Value} not found");
        }

        if (command.DateRange.StartDate < milestone.DateRange.StartDate ||
            command.DateRange.EndDate > milestone.DateRange.EndDate)
        {
            throw new Exception($"Task date range {command.DateRange} is outside of milestone date range {milestone.DateRange}");
        }

        var task = new TaskD(command);

        var specialty = await specialtyRepository.FindByName(command.Specialty.Name.ToString());
        if (specialty is null)
        {
            throw new Exception($"Specialty {command.Specialty?.Name} not found");
        }
        task.SetSpecialty(specialty);

        if (command.Status == null || command.PersonId == null)
        {
            var draftStatus = await taskStatusRepository.FindByName("DRAFT");
            if (draftStatus is null)
            {
                throw new Exception("Draft status not found");
            }
            task.ToDraft(draftStatus);
        }
        else
        {
            var status = await taskStatusRepository.FindByName(command.Status.Name.ToString());
            if (status is null)
            {
                throw new Exception($"Task status {command.Status.Name} not found");
            }
            task.ReassignStatus(status);
        }
        
        if (command.Money.Amount <= 0)
        {
            throw new Exception("Task budget must be greater than zero");
        }
        
        
        await taskRepository.AddAsync(task);
        
        await unitOfWork.CompleteAsync();
        
        await taskBudgetFacade.CreateTaskBudget(task.Id, command.Money.Amount, command.Money.Currency);
        
        return task;
    }
    
    public async Task<TaskD?> Handle(UpdateTaskCommand command)
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
            var personId = await iamFacade.GetProfileDetailsByIdAsync(command.PersonId.personId);
            if (personId is null)
            {
                throw new Exception($"Person {command.PersonId.personId} not found");
            }
            task.ReassignPerson(command.PersonId);
        }
        
        
        taskRepository.Update(task);
        await unitOfWork.CompleteAsync();
        return task;
    }

    public async Task Handle(DeleteTaskCommand command)
    {
        var task = await taskRepository.FindById(command.TaskId);
        if (task is null) 
        {
            throw new Exception($"Task {command.TaskId} not found");
        }
        
        taskRepository.Remove(task);
        await unitOfWork.CompleteAsync();
    }
}