using APBDTEST_01.model;

namespace APBDTEST_01.service;

public interface IService
{
    public TeamMemberResponse GetTeamMemberWithTasks(decimal id);
    public string? AddTask(TaskRequest taskRequest);
}