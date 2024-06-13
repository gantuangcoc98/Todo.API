namespace LocalDB;

public record ToDo
{
    public int Id { get; set; }
    public string? Task { get; set; }
}

public class ToDoDB 
{
     public static List<ToDo> ToDoList = new List<ToDo>();

     public static List<ToDo> GetToDoList()
     {
        return ToDoList;
     }

     public static ToDo? GetToDo(int Id)
     {
        return ToDoList.SingleOrDefault(ToDo => ToDo.Id == Id);
     }

     public static ToDo CreateToDo(ToDo toDo)
     {
        ToDoList.Add(toDo);
        return toDo;
     }

     public static ToDo UpdateToDo(ToDo toDo)
     {
        ToDoList = ToDoList.Select(ToDo =>
        {
            if (ToDo.Id == toDo.Id)
            {
                ToDo.Task = toDo.Task;
            }

            return ToDo;
        }).ToList();

        return toDo;
     }

     public static void DeleteToDo(int Id)
     {
        ToDoList = ToDoList.FindAll(ToDo => ToDo.Id != Id);
     }
}