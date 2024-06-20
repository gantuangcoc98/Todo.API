namespace LocalDB;

public class ToDoDB 
{
   private Dictionary<int, ToDo> ToDoList = new();

   public List<ToDo> GetToDoList() { return ToDoList.Values.ToList(); }

   public ToDo GetToDo(int id) 
   {
      if (ToDoList.TryGetValue(id, out ToDo? toDo)) 
      { 
         return toDo; 
      }
      else 
      { 
         throw new KeyNotFoundException("ToDo item not found."); 
      }
   }

   public ToDo CreateToDo(string task)
   {
      ArgumentException.ThrowIfNullOrEmpty(task);

      int newId;

      if (ToDoList.Count > 0) 
      { 
         newId = ToDoList.Keys.Max() + 1; 
      }
      else 
      { 
         newId = 0; 
      }

      ToDo toDo = new() { Id = newId, Task = task };

      return ToDoList[toDo.Id] = toDo;
   }

   public ToDo UpdateToDo(ToDo toDo)
   {
      ArgumentException.ThrowIfNullOrEmpty(toDo.Task);

      if (ToDoList.ContainsKey(toDo.Id)) 
      { 
         ToDoList[toDo.Id].Task = toDo.Task; 
         return ToDoList[toDo.Id];
      }
      else 
      { 
         throw new KeyNotFoundException("Cannot update, ToDo item not found."); 
      }
   }

   public void DeleteToDo(int id)
   {
      if (!ToDoList.Remove(id)) 
      { 
         throw new KeyNotFoundException("Cannot delete, ToDo item not found."); 
      }
   }
}