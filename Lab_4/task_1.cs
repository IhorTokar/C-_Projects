using System;
using System.Diagnostics;
using System.Threading;

// ==========================================
// ЗАВДАННЯ 1, 2, 3: БАЗОВА ДІАПАЗОННА ОБРОБКА
// ==========================================
public static class Task1 
{
    private const double multiplier = 10.5;

    // Послідовне виконання для порівняльного аналізу
    public static double RunSequential(int n)
    {
        double[] vector = new double[n];
        for (int i = 0; i < n; i++) vector[i] = i + 1;

        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < n; i++)
        {
            vector[i] *= multiplier;
        }
        sw.Stop();
        return sw.Elapsed.TotalMilliseconds;
    }

    // Паралельне виконання (Блоковий / Діапазонний поділ)
    public static double RunParallelRange(int n, int m) 
    {
        double[] vector = new double[n];
        for (int i = 0; i < n; i++) vector[i] = i + 1;

        Thread[] threads = new Thread[m];
        int chunkSize = n / m;

        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < m; i++) 
        {
            int threadIndex = i;
            int localStart = threadIndex * chunkSize;
            int localEnd = (threadIndex == m - 1) ? n : (threadIndex + 1) * chunkSize;

            threads[i] = new Thread(() => {
                for (int j = localStart; j < localEnd; j++)
                {
                    vector[j] *= multiplier;
                }
            });
            threads[i].Start();
        }

        foreach (Thread t in threads) t.Join();
        sw.Stop();
        return sw.Elapsed.TotalMilliseconds;
    }
}

// ==========================================
// ЗАВДАННЯ 4: УСКЛАДНЕНА ОБРОБКА (Параметр K)
// ==========================================
public static class Task3
{
    private const double multiplier = 10.5;

    public static void RunComplex(int n, int m, int K)
    {
        double[] vector = new double[n];
        for (int i = 0; i < n; i++) vector[i] = i + 1;

        // Послідовно
        Stopwatch swSeq = Stopwatch.StartNew();
        for (int i = 0; i < n; i++)
        {
            double val = vector[i];
            for (int k = 0; k < K; k++)
                val = val * multiplier / (multiplier + 1);
            vector[i] = val;
        }
        swSeq.Stop();

        // Паралельно (діапазонна декомпозиція)
        Thread[] threads = new Thread[m];
        int chunkSize = n / m;
        Stopwatch swPar = Stopwatch.StartNew();

        for (int i = 0; i < m; i++)
        {
            int threadIndex = i;
            int localStart = threadIndex * chunkSize;
            int localEnd = (threadIndex == m - 1) ? n : (threadIndex + 1) * chunkSize;

            threads[i] = new Thread(() => {
                for (int j = localStart; j < localEnd; j++)
                {
                    double val = vector[j];
                    for (int k = 0; k < K; k++)
                        val = val * multiplier / (multiplier + 1);
                    vector[j] = val;
                }
            });
            threads[i].Start();
        }

        foreach (Thread t in threads) t.Join();
        swPar.Stop();

        double speedup = swSeq.Elapsed.TotalMilliseconds / swPar.Elapsed.TotalMilliseconds;
        Console.WriteLine($"[K={K}] Послідовно: {swSeq.Elapsed.TotalMilliseconds:F2} мс | Паралельно ({m} потр.): {swPar.Elapsed.TotalMilliseconds:F2} мс -> Прискорення: {speedup:F2}x");
    }
}

// ==========================================
// ЗАВДАННЯ 5, 6: НЕРІВНОМІРНЕ НАВАНТАЖЕННЯ
// ==========================================
public static class Task4_5
{
    private const double multiplier = 10.5;

    // Послідовна нерівномірна обробка для розрахунку прискорення
    public static double RunSequentialUneven(int n)
    {
        double[] vector = new double[n];
        for (int i = 0; i < n; i++) vector[i] = i + 1;

        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < n; i++)
        {
            double val = vector[i];
            for (int k = 0; k < i; k++) // Кількість ітерацій залежить від індексу i
                val = val * multiplier / (multiplier + 1);
            vector[i] = val;
        }
        sw.Stop();
        return sw.Elapsed.TotalMilliseconds;
    }

    // Варіант А: Діапазонна декомпозиція (Блокова)
    public static double RangeDecomposition(int n, int m)
    {
        double[] vector = new double[n];
        for (int i = 0; i < n; i++) vector[i] = i + 1;

        Thread[] threads = new Thread[m];
        int chunkSize = n / m;

        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < m; i++)
        {
            int threadIndex = i;
            int localStart = threadIndex * chunkSize;
            int localEnd = (threadIndex == m - 1) ? n : (threadIndex + 1) * chunkSize;

            threads[i] = new Thread(() => {
                for (int j = localStart; j < localEnd; j++)
                {
                    double val = vector[j];
                    for (int k = 0; k < j; k++) // Навантаження зростає з індексом
                        val = val * multiplier / (multiplier + 1);
                    vector[j] = val;
                }
            });
            threads[i].Start();
        }

        foreach (Thread t in threads) t.Join();
        sw.Stop();
        return sw.Elapsed.TotalMilliseconds;
    }

    // Варіант Б: Кругова декомпозиція (Round-Robin)
    public static double RoundRobinDecomposition(int n, int m)
    {
        double[] vector = new double[n];
        for (int i = 0; i < n; i++) vector[i] = i + 1;

        Thread[] threads = new Thread[m];

        Stopwatch sw = Stopwatch.StartNew();
        for (int i = 0; i < m; i++)
        {
            int threadIndex = i;

            threads[i] = new Thread(() => {
                // Кожен потік бере елементи з кроком m
                for (int j = threadIndex; j < n; j += m)
                {
                    double val = vector[j];
                    for (int k = 0; k < j; k++) // Навантаження зростає з індексом
                        val = val * multiplier / (multiplier + 1);
                    vector[j] = val;
                }
            });
            threads[i].Start();
        }

        foreach (Thread t in threads) t.Join();
        sw.Stop();
        return sw.Elapsed.TotalMilliseconds;
    }
}