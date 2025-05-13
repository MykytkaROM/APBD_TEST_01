using APBDTEST_01.model;
using APBDTEST_01.service;
using Microsoft.AspNetCore.Mvc;

namespace APBDTEST_01.controller;
[ApiController]
[Route("api/tasks")]
public class TeamMemberController : ControllerBase
{
    private IService _service;

    public TeamMemberController(IService service)
    {
        _service = service;
    }

    [HttpGet("{id:int}")]
    public IActionResult GetTeamMembersWithTasks(int id)
    {
        var teamMemberWithTasks = _service.GetTeamMemberWithTasks(id);
        if (teamMemberWithTasks == null)
        {
            return NotFound("Team Member with the given id not found");
        }
        return Ok(teamMemberWithTasks);
    }

    public IActionResult AddTask([FromBody] TaskRequest taskRequest)
    {
        if (taskRequest.Name == null)
        {
            return BadRequest("Name cannot be null");
        }

        if (taskRequest.IdCreator < 0)
        {
            return BadRequest("IdCreator cannot be negative");
        }

        if (taskRequest.IdAssignedTo < 0)
        {
            return BadRequest("IdAssignedTo cannot be negative");
        }

        if (taskRequest.IdTaskType < 0)
        {
            return BadRequest("IdTaskType cannot be negative");
        }

        if (taskRequest.Deadline == null)
        {
            return BadRequest("Deadline cannot be null");
        }

        if (taskRequest.IdTeam < 0)
        {
            return BadRequest("IdTeam cannot be negative");
        }

        if (taskRequest.Description == null)
        {
            return BadRequest("Description cannot be null");
        }

        var message = _service.AddTask(taskRequest);
        if (message != null)
        {
            return BadRequest(message);
        }
        return Ok("Task successfully added");
    }
}