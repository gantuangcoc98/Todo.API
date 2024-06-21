using System.ComponentModel.DataAnnotations;

namespace Todo.API.Models.Responses;

public record TodoResponse(
    [Required]
    [StringLength(250)]
    string? Task
);
