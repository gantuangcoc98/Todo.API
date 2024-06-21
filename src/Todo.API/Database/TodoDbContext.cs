using Microsoft.EntityFrameworkCore;
using Todo.API.Models.Requests;

namespace Todo.API.DB;

public class TodoDbContext : DbContext 
{ 
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

    public DbSet<TodoRequest> TodoList { get; set; }
}
