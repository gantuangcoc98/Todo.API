using System.Reflection.Metadata.Ecma335;

namespace LocalDB;

public class ToDoDB 
{
     private List<ToDo> ToDoList = new List<ToDo>();

     public List<ToDo> GetToDoList()
     {
        return ToDoList;
     }

     public ToDo? GetToDo(int id)
     {
        return ToDoList.SingleOrDefault(ToDo => ToDo.Id == id);
     }
   
     public ToDo CreateToDo(string task)
     {
         ArgumentException.ThrowIfNullOrEmpty(task);

         int length = ToDoList.Count();

         ToDo toDo = new() { Id = length + 1, Task = task };
         ToDoList.Add(toDo);

         return toDo;
     }

     public ToDo UpdateToDo(ToDo _toDo)
     {
         ArgumentException.ThrowIfNullOrEmpty(_toDo.Task);

         bool updated = false;

         ToDoList = ToDoList.Select(ToDo =>
         {
            if (ToDo.Id == _toDo.Id)
            { 
               ToDo.Task = _toDo.Task; 
               updated = true;
            }

            return ToDo;
         }).ToList();

         if (!updated) { throw new KeyNotFoundException(); }

         return _toDo;
     }

   public void DeleteToDo(int id)
   {
      var todo = ToDoList.FirstOrDefault(todo => todo.Id == id);
      if (todo == null)
      {
         throw new ArgumentException("Task not found.");
      }
      
      ToDoList = ToDoList.FindAll(todo => todo.Id != id);
   }
}