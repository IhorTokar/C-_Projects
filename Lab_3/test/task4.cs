using System.Data;

public class Data{
    public static int sharedVar;
     [ThreadStatic] public static int localVar;
}

public class Task4{
    static void ThreadFunc(object i){
        Console.WriteLine("Потік {0}: До зміни.. Спільний:{1}, Локальний:{2}", i, Data.sharedVar, Data.localVar);
        Data.sharedVar = (int)i;
        Data.localVar = (int)i;
        Console.WriteLine("Потік {0}, Після зміни... Спільний:{1}, Локальний: {2}", i, Data.sharedVar, Data.localVar);
    }

    public static void Run(){
        Thread thread1 = new Thread(ThreadFunc);
        Thread thread2 = new Thread(ThreadFunc);

        Data.sharedVar = 3; Data.localVar = 3;
        thread1.Start(1); 
        thread2.Start(2);

        thread1.Join();
        thread2.Join();
    }

    public static void Run_Ver2(){
        ThreadLocal<int> localSum = new ThreadLocal<int>(() => 0);
        Thread thread1 = new Thread(() => {for(int i = 0; i < 10; i++){
            localSum.Value++;
        }
        Console.WriteLine(localSum.Value);});
        Thread thread2 = new Thread(() => {for(int i = 0; i < 10; i++){
            localSum.Value--;
        }
        Console.WriteLine(localSum.Value);});

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine(localSum.Value);
    }
}
