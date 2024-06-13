using Microsoft.OpenApi.Models;
using LocalDB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo", Version = "v1", Description = "Your trusted to do list."});
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo v1");
    });
}

app.MapGet("/todolist", () => ToDoDB.GetToDoList());
app.MapGet("/todo/{id}", (int id) => ToDoDB.GetToDo(id));
app.MapPost("/todo", (ToDo toDo) => ToDoDB.CreateToDo(toDo));
app.MapPut("/todo", (ToDo toDo) => ToDoDB.UpdateToDo(toDo));
app.MapDelete("/todo", (int id) => ToDoDB.DeleteToDo(id));

app.Run();
