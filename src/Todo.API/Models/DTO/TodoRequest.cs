using System.ComponentModel.DataAnnotations;

namespace Todo.API.DTO;

public record Todorequest(
    [Required]
    [StringLength(250)]
    string? Task
);
