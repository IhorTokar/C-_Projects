public class Task3{
    static long[] counts;
    static volatile bool finish;
    static void ThreadFunc(object iThread){
        while(true){
            if(finish){
                break;
            }
            counts[(int)iThread]++;
        }
    }
    public static void Run(){
        int N = 5;
        counts = new long[N];
        Thread[] threads = new Thread[N];
        for (int i = 0; i < threads.Length; i++){
            threads[i] = new Thread(ThreadFunc);
            threads[i].Priority = (ThreadPriority)i;
        }

        for( int i = 0; i < threads.Length; i++){
            threads[i].Start(i);
        }

        Thread.Sleep(10000);

        finish = true;

        for (int i = 0; i <threads.Length; i++){
            threads[i].Join();
        }

        for(int i = 0; i < threads.Length; i++){
            Console.WriteLine("Пріорітет потоку: {0, 15}, Лічильники {1}",(ThreadPriority)i, counts[i]);
        }
    }
}
