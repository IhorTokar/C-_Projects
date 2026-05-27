public static class Task3
    {
        public static void Run()
        {
            Console.WriteLine(">> Завдання 3: Робота з Sleep(0) для паралельності");

            Thread[] threads = new Thread[4];
            char[] letters = { 'A', 'B', 'C', 'D' };

            for (int i = 0; i < 4; i++)
            {
                threads[i] = new Thread(ThreadFunc);
                threads[i].Start(letters[i]); // Передача параметра через Start [cite: 113, 180]
            }

            foreach (var t in threads) 
            t.Join();
        }

        static void ThreadFunc(object obj)
        {
            for (int i = 0; i < 20; i++)
            {
                Console.Write(obj);
                Thread.Sleep(0); // Передача кванта часу іншим потокам [cite: 167, 194]
            }
        }
    }
