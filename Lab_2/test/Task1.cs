public static class Task1
    {
        public static void Run()
        {
            Console.WriteLine(">> Завдання 1: Використання Join() для синхронізації");
            
            Thread thrd1 = new Thread(() => { for (int i = 0; i < 5; i++) Console.Write("A"); });
            Thread thrd2 = new Thread(() => { for (int i = 0; i < 5; i++) Console.Write("B"); });
            Thread thrd3 = new Thread(() => { for (int i = 0; i < 5; i++) Console.Write("C"); });

            thrd1.Start();
            thrd2.Start();
            
            // Очікування завершення перших двох потоків [cite: 67, 68]
            thrd1.Join(); 
            thrd2.Join();

            // Потік thrd3 почне роботу ТІЛЬКИ після завершення thrd1 та thrd2 [cite: 71]
            thrd3.Start();
            thrd3.Join();
        }
    }
