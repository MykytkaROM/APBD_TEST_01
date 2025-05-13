namespace APBDTEST_01.model;

public class TaskRequest
{
 public string Name {get; set; }

 public string Description {get; set; }

 public string Deadline { get; set; }

 public decimal IdTeam { get; set; }

 public decimal IdTaskType { get; set; }
 public decimal IdAssignedTo {get; set; }
 public decimal IdCreator{get; set; }

}