namespace ApiTaskManagement.Controllers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ApiTaskManagement.BL.Interfaces;
using ApiTaskManagement.DTOs;
using ApiTaskManagement.Entities;
using ApiTaskManagement.Utils;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskBL _taskBL;
    private readonly IMapper _mapper;

    public TaskController(ITaskBL taskBL, IMapper mapper)
    {
        _taskBL = taskBL;
        _mapper = mapper;
    }

    private string GetUserId() => User.FindFirstValue("user_id") ?? throw new UnauthorizedAccessException();

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = GetUserId();
        var tasks = await _taskBL.GetAllAsync(userId);
        return Ok(ResponseHandler.Success(tasks));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = GetUserId();
        var task = await _taskBL.GetByIdAsync(id, userId);
        if (task == null)
            return NotFound(ResponseHandler.Error("Task not found", 404));

        return Ok(ResponseHandler.Success(task));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskCreateDTO dto)
    {
        var userId = GetUserId();
        var entity = _mapper.Map<TaskEntity>(dto);
        entity.UserId = userId;

        var success = await _taskBL.CreateAsync(entity);
        return Ok(ResponseHandler.Success(message: success ? "Created" : "Failed"));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] TaskUpdateDTO dto)
    {
        var userId = GetUserId();
        //var existing = await _taskBL.GetByIdAsync(id, userId);
        //if (existing == null)
        //    return NotFound(ResponseHandler.Error("Task not found", 404));

        //_mapper.Map(dto, existing);
        //var success = await _taskBL.UpdateAsync(existing);
        var success = true;
        return Ok(ResponseHandler.Success(message: success ? "Updated" : "Failed"));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = GetUserId();
        var success = await _taskBL.DeleteAsync(id, userId);
        if (!success)
            return NotFound(ResponseHandler.Error("Task not found or unauthorized", 404));

        return Ok(ResponseHandler.Success(message: "Deleted"));
    }
}
