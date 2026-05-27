using System;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

class Program
{
    static void Main()
    {
        List<Task> tasks = new List<Task>();
        Random rnd = new Random();
        long total = 0;
        int n = 0;

        for(int taskCntr = 0; taskCntr < 10; taskCntr++){
            tasks.Add(Task.Run( () => {
                int[] values = new int[10000];
                int taskTotal = 0;
                int taskN = 0;
                int cntr = 0;
                Monitor.Enter(rnd);
                for(cntr = 0; cntr < 10000; cntr++){
                    values[cntr] = rnd.Next(0, 1001);
                }
                Monitor.Exit(rnd);
                taskN = cntr;
                foreach(var value in values)
                  taskTotal +=value;

                Console.WriteLine("Mean for task {0,2}: {1:N2} (N={2:N0})",
                                    Task.CurrentId, (taskTotal * 1.0)/taskN, taskN);
                Interlocked.Add(ref n, taskN);
                Interlocked.Add(ref total, taskTotal);
            }));
        } 
        try{
            Task.WaitAll(tasks.ToArray());
            Console.WriteLine("\nMean for all tasks: {0:N2} (N={1:N0})", (total * 1.0)/n, n);
        }
        catch(AggregateException e){
            foreach(var ie in e.InnerExceptions)
              Console.WriteLine("{0}: {1}", ie.GetType().Name, ie.Message);
        }
    }

}
