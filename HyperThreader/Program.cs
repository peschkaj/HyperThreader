﻿using System;
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
            ExecuteTasks(6);
            Console.WriteLine("--------------------------------------------------------------------------------");
            ExecuteTasks(12);
            Console.WriteLine("--------------------------------------------------------------------------------");
            
            Console.WriteLine("Waiting for you to do something...");
            Console.ReadKey();
            return;
        }

        static void ExecuteTasks(int dop)
        {
            Func<object, int> action = (object obj) =>
            {
                var sw = new Stopwatch();
                sw.Start();
                var id = (int)obj;
                var nothing = long.MinValue + 1;

                for (var i = 0D; i <= MAX_COUNTER; i++)
                {
                    nothing++;
                    Math.Sqrt(nothing);
                }

                sw.Stop();

                Console.WriteLine("Task{0}, Thread{1}, ManagedThread{2}, Elapsed ms {3}", Task.CurrentId, id, Thread.CurrentThread.ManagedThreadId, sw.ElapsedMilliseconds);
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