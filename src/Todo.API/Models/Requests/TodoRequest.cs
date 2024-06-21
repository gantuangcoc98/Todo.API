namespace Todo.API.Models.Requests;

public record TodoRequest
{
    public int Id { get; set; }
    public string? Task { get; set; }
}
