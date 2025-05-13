using APBDTEST_01.model;
using APBDTEST_01.repository;

namespace APBDTEST_01.service;

public class TeamMemberService : IService
{
    private IRepository _repository;

    public TeamMemberService(IRepository repository)
    {
        _repository = repository;
    }

    public TeamMemberResponse GetTeamMemberWithTasks(decimal id)
    {
        var teamMember = _repository.GetTeamMember(id);
        if (teamMember == null)
        {
            return null;
        }
        var tasksCreatedByTeamMember = _repository.GetTasksCreatedBy(id);
        var tasksAssignedToTeamMember = _repository.GetTasksAssignedTo(id);
        teamMember.tasksCreatedBy = tasksCreatedByTeamMember;
        teamMember.tasksAssignedTo = tasksAssignedToTeamMember;
        return teamMember;
    }

    public string? AddTask(TaskRequest taskRequest)
    {
        if (!_repository.CheckIfCreatorExists(taskRequest.IdCreator))
        {
            return "IdCreator does not exist";
        }

        if (!_repository.CheckIfAssignedToExists(taskRequest.IdAssignedTo))
        {
            return "IdAssignedTo does not exist";
        }

        if (!_repository.CheckIfTaskTypeExists(taskRequest.IdTaskType))
        {
            return "IdTaskType does not exist";
        }

        if (!_repository.CheckIfProjectExists(taskRequest.IdTeam))
        {
            return "IdTeam does not exist";
        }
        _repository.AddTask(taskRequest);
        return null;
    }
}