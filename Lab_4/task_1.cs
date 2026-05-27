using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;

public class Task1{
    private const double multiplier = 10.5;
    public static void Run(int n, int m ){
        double[] vector = new double[n];
        for (int i = 0; i <n; i++){
            vector[i] = i+1;
        }

        Thread[] threads = new Thread[m];
        int chunSize = n / m;

        Stopwatch sw = Stopwatch.StartNew();

        for(int i = 0; i < m; i++){
            int threadIndex = i;
            int localStart = threadIndex * chunSize;
            int localEnd = (threadIndex == m - 1) ? n:(threadIndex + 1) * chunSize;

            threads[i] = new Thread(() => {
                for (int j = localStart; j < localEnd; j++){
                    vector[j] *= multiplier;
                }
            });
            threads[i].Start();
        }

        foreach( Thread t in threads){
            t.Join();
        }

        sw.Stop();

        Console.WriteLine($"⏱ Час виконання (паралельно): {sw.Elapsed.TotalMilliseconds:F4} мс");
        if (n > 10) Console.WriteLine($"Контроль [10]: {vector[10]} (очікувано 115.5)");
    }
    }
