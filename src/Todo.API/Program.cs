using Todo.API.DB;
using Todo.API.Services;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Todo.API.Models.Requests;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoDbContext>(options =>
{ options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")); });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo.API", Description = "Your trusted todo application.", Version = "v1"} );
});

builder.Services.AddTransient<TodoService>();
builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/todolist", (TodoService ts) => ts.GetToDoList());
app.MapGet("/todo", (TodoService ts, int id) => ts.GetToDo(id));
app.MapPost("/todo", (TodoService ts, string task) => ts.CreateToDo(task));
app.MapPut("/todo", (TodoService ts, TodoRequest toDo) => ts.UpdateToDo(toDo));
app.MapDelete("/todo", (TodoService ts, int id) => ts.DeleteToDo(id));

app.Run();
