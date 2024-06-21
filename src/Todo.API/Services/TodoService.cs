using AutoMapper;
using Todo.API.DB;
using Todo.API.Models.Requests;
using Todo.API.Models.Responses;

namespace Todo.API.Services;

public class TodoService
{
   private readonly TodoDbContext dbContext;
   private readonly IMapper _mapper;

   public TodoService(TodoDbContext todoDbContext, IMapper mapper)
   {
      dbContext = todoDbContext;
      _mapper = mapper;
   }

   public IEnumerable<TodoRequest> GetToDoList() => dbContext.TodoList;

   public IResult GetToDo(int id) 
   {  
      var toDo = dbContext.TodoList.Find(id);

      if (toDo is not null) 
      { 
         var _toDo = _mapper.Map<TodoResponse>(toDo);
         return Results.Ok(_toDo);
      }
      else { return Results.BadRequest("Todo item not found."); }
   }

   public IResult CreateToDo(string task)
   {
      
      if (string.IsNullOrEmpty(task)) { return Results.BadRequest("Cannot create todo item. Task should not be null or empty."); }

      var todoRequest = new TodoRequest { Task = task };
      
      dbContext.TodoList.Add(todoRequest);
      dbContext.SaveChanges();
      
      var todoResponse = _mapper.Map<TodoResponse>(todoRequest);

      return Results.Ok(todoResponse);
   }

   public IResult UpdateToDo(TodoRequest toDo)
   {
      if (string.IsNullOrEmpty(toDo.Task)) { return Results.BadRequest("Cannot create todo item. Task should not be null or empty."); }

      var existingElem = dbContext.TodoList.Find(toDo.Id);

      if (existingElem is not null) 
      { 
         existingElem.Task = toDo.Task;

         dbContext.TodoList.Update(existingElem);
         dbContext.SaveChanges();

         var todoResponse = _mapper.Map<TodoResponse>(toDo);

         return Results.Ok(todoResponse);
      }
      else { return Results.BadRequest("Cannot update, todo item not found."); }
   }

   public IResult DeleteToDo(int id)
   {
      var toDo = dbContext.TodoList.Find(id);
      
      if (toDo is null) { return Results.BadRequest("Cannot delete, todo item not found."); }

      dbContext.TodoList.Remove(toDo);
      dbContext.SaveChanges();

      return Results.Ok("Todo item deleted successfully.");
   }
}
