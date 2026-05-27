using System.Reflection;

public class Task2{
    public static void Run(){
        Thread task = Thread.CurrentThread;
        task.Name = "Main Thread ";
        foreach(PropertyInfo property in task.GetType().GetProperties()){
            Console.WriteLine("{0}:{1}", property.Name, property.GetValue(task, null));
        }
    }
}
