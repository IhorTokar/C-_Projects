
using System.Threading;
//Thread — Потік
public class Programm{
public static void Main() {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Clear();
        Console.WriteLine("======================================================");
        Console.WriteLine("📊 ЛАБОРАТОРНА РОБОТА №2: БАГАТОПОТОКОВІСТЬ В .NET");
        Console.WriteLine("======================================================\n");

        // --- Завдання 1 ---
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(">>> Завдання 1: Робота з масивом потоків та Join()");
        Console.ResetColor();
        Task1.Run();
        NextStep();

        // --- Завдання 2 ---
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(">>> Завдання 2: Метадані поточного потоку (Reflection)");
        Console.ResetColor();
        Task2.Run();
        NextStep();

        // --- Завдання 3 ---
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(">>> Завдання 3: Дослідження пріоритетів (10 секунд...)");
        Console.ResetColor();
        Task3.Run();
        NextStep();

        // --- Завдання 4 ---
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(">>> Завдання 4: Ізоляція даних ([ThreadStatic] & ThreadLocal)");
        Console.ResetColor();
        Task4.Run();
        Console.WriteLine("\n--- Версія 2 (ThreadLocal) ---");
        Task4.Run_Ver2();
        NextStep();

        // --- Завдання 5 ---
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(">>> Завдання 5: Локальні слоти даних (NamedDataSlot)");
        Console.ResetColor();
new Thread(() => Task5.Run("Banana")).Start();
new Thread(() => Task5.Run("Cherry")).Start();

// Даємо їм час відпрацювати, перш ніж Main піде далі
Thread.Sleep(2000);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n======================================================");
        Console.WriteLine("✅ ВИКОНАННЯ ПРОГРАМИ ЗАВЕРШЕНО");
        Console.WriteLine("======================================================");
        Console.ResetColor();
    }

    static void NextStep() {
        Console.WriteLine("\n[Натисніть Enter для продовження...]");
        Console.ReadLine();
        Console.Clear();
    }
}
