namespace ApiTaskManagement.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiTaskManagement.BL.Interfaces;
using ApiTaskManagement.DTOs;
using ApiTaskManagement.Entities;
using ApiTaskManagement.Utils;
using ApiTaskManagement.Services.Interfaces;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskBL _taskBL;
    private readonly ITaskService _taskService;
    private readonly IMapper _mapper;

    public TaskController(ITaskBL taskBL,ITaskService taskService, IMapper mapper)
    {
        _taskBL = taskBL;
        _taskService = taskService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? from, [FromQuery] string? end)
    {
        var tasks = await _taskService.GetTasks(from, end);
        return Ok(ResponseHandler.Success(tasks));
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task == null)
            return NotFound(ResponseHandler.Error("Task not found", 404));

        return Ok(ResponseHandler.Success(task));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskCreateDTO dto)
    {
        var entity = _mapper.Map<TaskEntity>(dto);

        var success = await _taskBL.CreateAsync(entity);
        return Ok(ResponseHandler.Success(message: success ? "Created" : "Failed"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TaskUpdateDTO dto)
    {
        var entity = _mapper.Map<TaskEntity>(dto);
        entity.Id = id;
        var success = await _taskBL.UpdateAsync(entity);
        return Ok(ResponseHandler.Success(message: success ? "Updated" : "Failed"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _taskBL.DeleteAsync(id);
        if (!success)
            return NotFound(ResponseHandler.Error("Task not found or unauthorized", 404));

        return Ok(ResponseHandler.Success(message: "Deleted"));
    }
}
