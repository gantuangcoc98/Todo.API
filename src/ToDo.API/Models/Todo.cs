namespace Todo.API.Models;

public record ToDo
{
    public int Id { get; set; }
    public string? Task { get; set; }
}
