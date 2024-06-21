using Todo.API.DB;
using Todo.API.Models;
using Todo.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Database>();
builder.Services.AddScoped<TodoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/todolist", (TodoService ts) => ts.GetToDoList());
app.MapGet("/todo", (TodoService ts, int id) => ts.GetToDo(id));
app.MapPost("/todo", (TodoService ts, string task) => ts.CreateToDo(task));
app.MapPut("/todo", (TodoService ts, ToDo toDo) => ts.UpdateToDo(toDo));
app.MapDelete("/todo", (TodoService ts, int id) => ts.DeleteToDo(id));

app.Run();
