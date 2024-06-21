using Todo.API.DB;
using Todo.API.Models;
using Todo.API.DTO;

namespace Todo.API.Services;

public class TodoService(Database db)
{
    public IEnumerable<ToDo> GetToDoList() => db.ToDoList.Values;

   public IResult GetToDo(int id) 
   {
      if (db.ToDoList.TryGetValue(id, out ToDo? toDo)) 
      { 
         TodoDTO _todoDTO = new(toDo.Task);
         return Results.Ok(_todoDTO);
      }
      else { return Results.BadRequest("Todo item not found."); }
   }

   public IResult CreateToDo(string task)
   {
      if (string.IsNullOrEmpty(task)) { return Results.BadRequest("Cannot create Todo. Task should not be null or empty."); }

      int newId;

      if (db.ToDoList.Count > 0) { newId = db.ToDoList.Keys.Max() + 1; }
      else { newId = 0; }

      ToDo toDo = new() { Id = newId, Task = task };
      
      db.ToDoList[toDo.Id] = toDo;

      TodoDTO todoDTO = new(toDo.Task);

      return Results.Ok(todoDTO);
   }

   public IResult UpdateToDo(ToDo toDo)
   {
      ArgumentException.ThrowIfNullOrEmpty(toDo.Task);

      if (db.ToDoList.ContainsKey(toDo.Id)) 
      { 
         db.ToDoList[toDo.Id].Task = toDo.Task; 

         TodoDTO todoDTO = new(toDo.Task);

         return Results.Ok(todoDTO);
      }
      else { throw new KeyNotFoundException("Cannot update, ToDo item not found."); }
   }

   public IResult DeleteToDo(int id)
   {
      if (!db.ToDoList.Remove(id)) { return Results.BadRequest("Cannot delete, todo item not found."); }

      return Results.Ok("Todo item deleted successfully.");
   }
}