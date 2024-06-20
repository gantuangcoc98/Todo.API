using LocalDB;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

ToDoDB db = new();

app.MapGet("/", () => "Hello World!");

app.MapGet("/todolist", () => db.GetToDoList());

app.MapGet("/todo", (int id) => 
{ 
    ToDo toDo = new();

    try 
    { 
        toDo = db.GetToDo(id); 
    }
    catch (KeyNotFoundException ex) 
    { 
        return Results.BadRequest(ex.Message); 
    }

    return Results.Ok(toDo);
});

app.MapPost("/todo", (string task) =>
{
    ToDo toDo = new();

    try 
    { 
        toDo = db.CreateToDo(task); 
    }
    catch (ArgumentException) 
    { 
        return Results.BadRequest("Task should not be null or empty."); 
    }

    return Results.Ok(toDo);
});

app.MapPut("/todo", (ToDo toDo) => 
{
    ToDo _toDo = new();

    try 
    { 
        _toDo = db.UpdateToDo(toDo);
    }
    catch (ArgumentException) 
    { 
        return Results.BadRequest("Task should not be null or empty."); 
    }
    catch (KeyNotFoundException ex) 
    { 
        return Results.BadRequest(ex.Message); 
    }

    return Results.Ok(_toDo);
});

app.MapDelete("/todo", (int id) =>
{
    try
    {
        db.DeleteToDo(id);
        return Results.Ok("Task deleted successfully.");
    }
    catch (KeyNotFoundException ex) 
    { 
        return Results.BadRequest(ex.Message); 
    }
});

app.Run();
