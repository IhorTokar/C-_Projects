using System;
using System.Threading;
public class Programm{
    static void Main (){
      Console.WriteLine("=== Лабораторна робота №2: Робота з потоками ===\n");

            // Завдання 1: Демонстрація черговості та Join()
            Task1.Run();
            Console.WriteLine("\n\nНатисніть Enter для переходу до наступного завдання...");
            Console.ReadLine();

            // Завдання 2: Передача параметрів та замикання (Closures)
            Task2.Run();
            Console.WriteLine("\n\nНатисніть Enter для переходу до наступного завдання...");
            Console.ReadLine();

            // Завдання 3: Демонстрація паралельності з Sleep(0)
            Task3.Run();

            Console.WriteLine("\n=== Роботу завершено ===");
            Console.ReadLine();
    }
}
