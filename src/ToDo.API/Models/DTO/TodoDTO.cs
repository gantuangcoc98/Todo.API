using System.ComponentModel.DataAnnotations;

namespace Todo.API.DTO;

public record class TodoDTO(
    [Required]
    [StringLength(250)] 
    string? Task
);
