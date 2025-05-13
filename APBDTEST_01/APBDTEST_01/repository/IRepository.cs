using APBDTEST_01.model;

namespace APBDTEST_01.repository;

public interface IRepository
{
    public TeamMemberResponse GetTeamMember(decimal id);
    public List<TaskResponse> GetTasksCreatedBy(decimal id);
    public List<TaskResponse> GetTasksAssignedTo(decimal id);
    public bool CheckIfProjectExists(decimal id);
    public bool CheckIfTaskTypeExists(decimal id);

    public bool CheckIfAssignedToExists(decimal id);
    public bool CheckIfCreatorExists(decimal id);
    public void AddTask(TaskRequest taskRequest);


}