using System;

public class Program
{
    public static void Main()
    {
        // Встановлюємо кодування UTF-8 для коректного відображення символів в Arch Linux
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        
        Console.WriteLine($"=== СИСТЕМНА ІНФОРМАЦІЯ ===");
        Console.WriteLine($"Кількість логічних ядер (ProcessorCount): {Environment.ProcessorCount}");
        Console.WriteLine($"Очікувана кількість фізичних ядер: {Environment.ProcessorCount / 2} (для i7-1165G7)\n");

        // ------------------------------------------------------------------------
        // РОЗІГРІВ ЦП (щоб JIT-компіляція не спотворювала час перших тестів)
        // ------------------------------------------------------------------------
        Console.WriteLine("Прогрів системи та JIT-компіляція...");
        Task4_5.RunSequentialUneven(1000);
        Task4_5.RoundRobinDecomposition(1000, 4);
        Console.WriteLine("Прогрів завершено.\n");

        // ------------------------------------------------------------------------
        // ТЕСТ 1: ЗБІР ДАНИХ ДЛЯ ТАБЛИЦІ 4.1 (Базова діапазонна обробка)
        // ------------------------------------------------------------------------
        Console.WriteLine("=======================================================================");
        Console.WriteLine("📊 ЗАПУСК ТЕСТІВ ДЛЯ ТАБЛИЦІ 4.1 (Базове множення, Діапазонний поділ)");
        Console.WriteLine("=======================================================================");
        
        int[] nCasesTable1 = { 10, 100, 1000, 100000 };
        int[] mCases = { 2, 3, 4, 5, 10 };

        foreach (int n in nCasesTable1)
        {
            Console.WriteLine($"\n--- Розмірність вектора N = {n} ---");
            
            // Послідовне виконання викликається з класу Task1
            double tSeq = Task1.RunSequential(n);
            Console.WriteLine($"Послідовно (1 потік) : {tSeq:F4} мс");

            // Паралельне виконання для різних M викликається з класу Task1
            foreach (int m in mCases)
            {
                double tPar = Task1.RunParallelRange(n, m);
                Console.WriteLine($"Паралельно (M = {m,-2})  : {tPar:F4} мс");
            }
        }

        // ------------------------------------------------------------------------
        // ТЕСТ 2: ЗБІР ДАНИХ ДЛЯ ТАБЛИЦІ 4.2 (Нерівномірне навантаження, N = 100 000)
        // ------------------------------------------------------------------------
        Console.WriteLine("\n=======================================================================");
        Console.WriteLine("📊 ЗАПУСК ТЕСТІВ ДЛЯ ТАБЛИЦІ 4.2 (Нерівномірне навантаження, N = 100 000)");
        Console.WriteLine("=======================================================================");
        
        int nTable2 = 100000;
        double tSeqUneven = Task4_5.RunSequentialUneven(nTable2);
        
        Console.WriteLine($"Послідовний час (базовий T_seq): {tSeqUneven:F2} мс\n");
        Console.WriteLine("M\tДіапазонна (мс)\tКругова (мс)\tПриск. Діап.\tПриск. Круг.");
        Console.WriteLine("-----------------------------------------------------------------------");

        foreach (int m in mCases)
        {
            // Викликаємо методи з класу Task4_5
            double tRange = Task4_5.RangeDecomposition(nTable2, m);
            double tRobin = Task4_5.RoundRobinDecomposition(nTable2, m);
            
            double speedupRange = tSeqUneven / tRange;
            double speedupRobin = tSeqUneven / tRobin;

            Console.WriteLine($"{m}\t{tRange:F2}\t\t{tRobin:F2}\t\t{speedupRange:F2}x\t\t{speedupRobin:F2}x");
        }
        Console.WriteLine("=======================================================================");

        // ------------------------------------------------------------------------
        // ТЕСТ 3: ДЕМОНСТРАЦІЯ ШТУЧНОГО УСКЛАДНЕННЯ (Завдання 4)
        // ------------------------------------------------------------------------
        Console.WriteLine("\n=======================================================================");
        Console.WriteLine("📊 ЗАПУСК ТЕСТУ ДЛЯ ЗАВДАННЯ 4 (Штучне ускладнення, параметр K=5000)");
        Console.WriteLine("=======================================================================");
        Task3.RunComplex(50000, 4, 5000);
        Console.WriteLine("=======================================================================");
    }
}