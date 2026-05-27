public static class Task2
    {
        public static void Run()
        {
            Console.WriteLine(">> Завдання 2: Передача параметрів та копіювання індексу");

           for (int i = 0; i < 10; i++)
    {
        int i_copy = i; // Обов'язково створюємо копію [cite: 152]
        Thread t = new Thread(() => {
            
            // Використовуємо lock і тут, якщо помилка повторюється
            Console.Write("ABCDEFGHIJK"[i_copy]); // [cite: 154]
        });
        t.Start();
    }
        }
    }
