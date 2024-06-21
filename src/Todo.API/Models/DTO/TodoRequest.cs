using System.ComponentModel.DataAnnotations;

namespace Todo.API.DTO;

public record TodoRequest(
    [Required]
    [StringLength(250)]
    string? Task
);
