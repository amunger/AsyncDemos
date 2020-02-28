using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ControlFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeAsyncWork();
            Console.ReadLine();

            Console.WriteLine();
            TimeAsyncWork();
            Console.ReadLine();
        }

        public static void TimeAsyncWork()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            DoAsyncAndSyncWork();

            stopwatch.Stop();
            Console.WriteLine($"Finished in {stopwatch.ElapsedMilliseconds} ms");
        }

        private static void DoAsyncAndSyncWork()
        {
            Console.WriteLine("Creating task");
            Task task = WorkAsync();
            Console.WriteLine("Doing other work while Task is running");
            Thread.Sleep(1000);
            task.Wait();
            Console.WriteLine("Task Complete");
        }

        public static async Task WorkAsync()
        {
            Console.WriteLine("begin async method");
            await AsyncOperation();
            Console.WriteLine("finished async method");
            Thread.Sleep(500);
            Console.WriteLine("finished post-async processing");
        }

        private static async Task AsyncOperation()
        {
            if (!done)
                await Task.Delay(500);
            done = true;
        }

        private static bool done;
    }
}
