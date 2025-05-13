using APBDTEST_01.model;
using Microsoft.Data.SqlClient;

namespace APBDTEST_01.repository;

public class TeamMemberRepository : IRepository
{
    private readonly IConfiguration configuration;

    public TeamMemberRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public List<TaskResponse> GetTasksAssignedTo(decimal id)
    {
        using (var conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            conn.Open();
            var select = new SqlCommand("SELECT * FROM TASK WHERE IdAssignedTo = @id", conn);
            select.Parameters.AddWithValue("@id", id);
            var reader = select.ExecuteReader();
            List<TaskResponse> taskResponses = new List<TaskResponse>();
            while (reader.Read())
            {
                taskResponses.Add(new TaskResponse(reader["Name"].ToString(),reader["Description"].ToString()
                    ,reader["Deadline"].ToString()));
            }
            reader.Close();
            taskResponses = taskResponses.OrderByDescending(x => x.Deadline).ToList();
            var getProjectName = new SqlCommand("SELECT p.NAME FROM Project p JOIN Task t ON t.IdProject = p.IdProject ORDER BY p.Deadline DESC ",conn);
            getProjectName.ExecuteReader();
            while (reader.Read())
            {
                foreach (var taskResponse in taskResponses)
                {
                    taskResponse.ProjectName = reader["Name"].ToString();
                }
            }
            return taskResponses;
        }
    }

    public List<TaskResponse> GetTasksCreatedBy(decimal id)
    {
        using (var conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            conn.Open();
            var select = new SqlCommand("SELECT * FROM TASK WHERE idCreator = @id", conn);
            select.Parameters.AddWithValue("@id", id);
            var reader = select.ExecuteReader();
            List<TaskResponse> taskResponses = new List<TaskResponse>();
            while (reader.Read())
            {
                taskResponses.Add(new TaskResponse(reader["Name"].ToString(),reader["Description"].ToString()
                    ,reader["Deadline"].ToString()));
            }
            reader.Close();
            taskResponses = taskResponses.OrderByDescending(x => x.Deadline).ToList();
            var getProjectName = new SqlCommand("SELECT p.NAME FROM Project p JOIN Task t ON t.IdProject = p.IdProject ORDER BY p.Deadline DESC ",conn);
            getProjectName.ExecuteReader();
            while (reader.Read())
            {
                foreach (var taskResponse in taskResponses)
                {
                    taskResponse.ProjectName = reader["Name"].ToString();
                }
            }
            return taskResponses;
        }
    }

    public TeamMemberResponse GetTeamMember(decimal id)
    {
        using (var conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            conn.Open();
            var select = new SqlCommand("SELECT * FROM TeamMember WHERE IdTeamMember = @id", conn);
            select.Parameters.AddWithValue("@id", id);
            var reader = select.ExecuteReader();
            var teamMemberResponse = new TeamMemberResponse();
            if (reader.Read())
            {
                teamMemberResponse.IdTeamMember = (decimal)reader["IdTeamMember"];
                teamMemberResponse.FirstName = reader["FirstName"].ToString();
                teamMemberResponse.LastName = reader["LastName"].ToString();
                teamMemberResponse.Email = (string)reader["Email"];
            }
            reader.Close();
            
            return teamMemberResponse;
        }
    }

    public bool CheckIfProjectExists(decimal id)
    {
        using (var conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            conn.Open();
            var select = new SqlCommand("SELECT * FROM Project WHERE IdProject = @id", conn);
            select.Parameters.AddWithValue("@id", id);
            return select.ExecuteScalar() != null;
        }
    }

    public bool CheckIfTaskTypeExists(decimal id)
    {
        using (var conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            conn.Open();
            var select = new SqlCommand("SELECT * FROM TaskType WHERE IdTaskType = @id", conn);
            select.Parameters.AddWithValue("@id", id);
            return select.ExecuteScalar() != null;
        }
    }

    public bool CheckIfAssignedToExists(decimal id)
    {
        using (var conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            conn.Open();
            var select = new SqlCommand("SELECT * FROM TeamMember WHERE IdTeamMember = @id", conn);
            select.Parameters.AddWithValue("@id", id);
            return select.ExecuteScalar() != null;
        }
    }

    public bool CheckIfCreatorExists(decimal id)
    {
        using (var conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
            conn.Open();
            var select = new SqlCommand("SELECT * FROM TeamMember WHERE IdTeamMember = @id", conn);
            select.Parameters.AddWithValue("@id", id);
            return select.ExecuteScalar() != null;
        }
    }

    public void AddTask(TaskRequest taskRequest)
    {
        using (var conn = new SqlConnection(configuration.GetConnectionString("DefaultConnection")))
        {
           conn.Open();
           var insertIntoTask = new SqlCommand("INSERT INTO Task(Name,Description,Deadline,IdProject,IdTaskType,IdAssignedTo,IdCreator) VALUES(@Name,@Description,@Deadline,@IdProject,@IdTaskType,@IdAssignedTo,@IdCreator)", conn);
           insertIntoTask.Parameters.AddWithValue("@Name", taskRequest.Name);
           insertIntoTask.Parameters.AddWithValue("@Description", taskRequest.Description);
           insertIntoTask.Parameters.AddWithValue("@Deadline", taskRequest.Deadline);
           insertIntoTask.Parameters.AddWithValue("IdProject", taskRequest.IdTeam);
           insertIntoTask.Parameters.AddWithValue("IdTaskType", taskRequest.IdTaskType);
           insertIntoTask.Parameters.AddWithValue("IdAssignedTo", taskRequest.IdAssignedTo);
           insertIntoTask.Parameters.AddWithValue("IdCreator", taskRequest.IdCreator);
           insertIntoTask.ExecuteNonQuery();
        }
    }
}