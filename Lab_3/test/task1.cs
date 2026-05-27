public class Task1{
    public static void Run(){
    Thread[] arThread = new Thread[10];
        for (int i = 0; i < arThread.Length; i++){
            arThread[i] = new Thread(() => {Console.Write("A");
            Thread.Sleep(1);});
            arThread[i].Start();
        }
        Console.Write("\n");
        arThread[2].Name = "Cool";
        for (int i = 0; i < arThread.Length; i++){
            Console.WriteLine("Id потоку: {0}, ім'я: {1}, IsAllive: {2}", arThread[i].ManagedThreadId, arThread[i].Name, arThread[i].IsAlive);
        }

        foreach(Thread t in arThread){
            t.Join();
        }
    }
}
