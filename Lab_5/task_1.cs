using System;
using System.Collections.Generic;
using System.Threading;
using System.Diagnostics;

public class Task1{
    private static bool[] isComposite; // Спільний ресурс: масив міток
    private static List<int> basePrimes; // Спільний ресурс: список фільтрів
    private static int nextPrimeIndex = 0; // Спільний ресурс: індекс наступного числа
    private static object locker = new object(); // Об'єкт синхронізації

    public static void Run(int n, int m)
    {
        Console.WriteLine($"\n--- Завдання 5.4: Паралельний пошук (N={n:N0}, M={m}) ---");
        
        // 1. Етап 1: Знаходимо базові числа до SQRT(N) послідовно
        int limit = (int)Math.Sqrt(n);
        basePrimes = FindBasePrimes(limit);
        
        isComposite = new bool[n + 1];
        nextPrimeIndex = 0;

        // 2. Етап 2: Паралельне викреслювання
        Thread[] threads = new Thread[m];
        Stopwatch sw = Stopwatch.StartNew();

        for (int i = 0; i < m; i++)
        {
            threads[i] = new Thread(() => Worker(n));
            threads[i].Start();
        }

        foreach (var t in threads) t.Join();
        sw.Stop();

        // 3. Результати
        int count = 0;
        for (int i = 2; i <= n; i++) if (!isComposite[i]) count++;

        Console.WriteLine($"✅ Знайдено простих чисел: {count}");
        Console.WriteLine($"⏱ Час виконання: {sw.Elapsed.TotalMilliseconds:F4} мс");
    }

    private static void Worker(int n)
    {
        while (true)
        {
            int p;
            
            // КРИТИЧНА СЕКЦІЯ: беремо наступне просте число безпечно
            lock (locker)
            {
                if (nextPrimeIndex >= basePrimes.Count) break;
                p = basePrimes[nextPrimeIndex];
                nextPrimeIndex++;
            }

            // Викреслювання всіх кратних числу p, починаючи з p*p
            for (long j = (long)p * p; j <= n; j += p)
            {
                isComposite[j] = true;
            }
        }
    }

    private static List<int> FindBasePrimes(int limit)
    {
        bool[] tempSieve = new bool[limit + 1];
        List<int> primes = new List<int>();
        for (int i = 2; i <= limit; i++)
        {
            if (!tempSieve[i])
            {
                primes.Add(i);
                for (int j = i * i; j <= limit; j += i) tempSieve[j] = true;
            }
        }
        return primes;
    }
}
