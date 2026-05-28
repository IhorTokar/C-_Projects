using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        // Налаштування кодування для коректного виводу в Arch Linux
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== СИСТЕМНА ІНФОРМАЦІЯ ===");
        Console.WriteLine($"Процесор: Intel Core i7-1165G7 (Логічних ядер: {Environment.ProcessorCount})");
        Console.WriteLine("ОС: Arch Linux | Платформа: .NET 8.0\n");

        // Попередній розігрів JIT-компілятора
        Console.WriteLine("Прогрів системи та JIT-компіляція...");
        Task1.Run(100000, 1);
        Task1.Run(100000, 4);
        Console.WriteLine("Прогрів завершено. Початок експериментів.\n");

        int[] nCases = { 1000000, 10000000, 100000000 };
        int[] mCases = { 1, 2, 3, 4 };

        // Словник для збереження результатів (ключ: "N_M", значення: час в мс)
        Dictionary<string, double> results = new Dictionary<string, double>();

        foreach (int n in nCases)
        {
            foreach (int m in mCases)
            {
                double time = Task1.Run(n, m);
                results[$"{n}_{m}"] = time;
            }
        }

        // Виведення готової таблиці для звіту
        Console.WriteLine("\n=========================================================================================");
        Console.WriteLine("📊 ГЕНЕРАЦІЯ ДАНИХ ДЛЯ ТАБЛИЦІ 5.1");
        Console.WriteLine("=========================================================================================");
        Console.WriteLine("N\t\tM=1 (посл)\tM=2\t\tM=3\t\tM=4\t\tS(M=2)\tS(M=4)\tЕфект.(M=4)");
        Console.WriteLine("-----------------------------------------------------------------------------------------");

        foreach (int n in nCases)
        {
            double t1 = results[$"{n}_1"];
            double t2 = results[$"{n}_2"];
            double t3 = results[$"{n}_3"];
            double t4 = results[$"{n}_4"];

            double s2 = t1 / t2;
            double s4 = t1 / t4;
            double e4 = s4 / 4.0;

            Console.WriteLine($"{n:N0}\t{t1:F2} мс\t{t2:F2} мс\t{t3:F2} мс\t{t4:F2} мс\t{s2:F2}x\t{s4:F2}x\t{e4:F2}");
        }
        Console.WriteLine("=========================================================================================");
    }
}