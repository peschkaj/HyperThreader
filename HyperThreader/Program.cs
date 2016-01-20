using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace HyperThreader
{
    class Program
    {
        const double MAX_COUNTER = 10E8;

        static void Main()
        {
            ExecuteTasks(1);
            Console.WriteLine("--------------------------------------------------------------------------------");
            ExecuteTasks(Environment.ProcessorCount / 2);
            Console.WriteLine("--------------------------------------------------------------------------------");
            ExecuteTasks(Environment.ProcessorCount);
            Console.WriteLine("--------------------------------------------------------------------------------");
            Console.WriteLine("\n\n");

            return;
        }

        static void ExecuteTasks(int dop)
        {
            Func<object, int> action = (object obj) =>
            {
                var sw = Stopwatch.StartNew();
                var nothing = long.MinValue + 1;

                for (var i = 0D; i <= MAX_COUNTER; i++)
                {
                    Math.Sqrt(++nothing);
                }

                sw.Stop();

                Console.WriteLine("Task {0,2}, Thread {1,2}, ManagedThread {2,2}, Elapsed ms {3,2}", Task.CurrentId, (int)obj, Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds);
                return (int)sw.ElapsedTicks;
            };

            var tasks = new List<Task<int>>();
            for (var i = 0; i < dop; i++)
            {
                tasks.Add(Task<int>.Factory.StartNew(action, i));
            }

            try {
                Task.WaitAll(tasks.ToArray());
            }
            catch {
                Console.WriteLine("well, that's novel...");
            }
        }
    }
}
