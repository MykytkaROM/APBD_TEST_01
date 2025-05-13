namespace APBDTEST_01.model;

public class TaskResponse
{
    
    public string Name { get; set; }
    public string Description { get; set; }
    public string Deadline { get; set; }
    public string ProjectName { get; set; }
    
    

    public TaskResponse( string name, string description, 
        string deadline)
    {
        
        Name = name;
        Description = description;
        Deadline = deadline;
        
        
    }
}