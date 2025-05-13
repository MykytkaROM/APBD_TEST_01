namespace APBDTEST_01.model;

public class TeamMemberResponse
{
    public decimal IdTeamMember { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public List<TaskResponse> tasksAssignedTo { get; set; }
    public List<TaskResponse> tasksCreatedBy { get; set; }
}