using ApiTaskManagement.Constants;
using System.ComponentModel.DataAnnotations;
namespace ApiTaskManagement.DTOs;

public class TaskCreateDTO
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = null!;

    [Required]
    [Range(TaskPriorityConstants.Low, TaskPriorityConstants.High, ErrorMessage = "PriorityId must be a valid value.")]
    public int PriorityId { get; set; }

    [Required]
    [Range(TaskStateConstants.New, TaskStateConstants.Done, ErrorMessage = "StateId must be a valid value.")]
    public int StateId { get; set; }
}