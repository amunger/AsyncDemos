using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleWorkers
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.SetMaxThreads(10, 10);
            
            StartWorkers();
            
            Console.ReadLine();
        }

        private static void StartWorkers()
        {
            while(true)
            {
                Console.WriteLine("How many workers?");
                var workerCount = int.Parse(Console.ReadLine());

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();

                var tasks = CreateWorkerTasks(workerCount);
                Task.WaitAll(tasks);

                stopWatch.Stop();
                Console.WriteLine($"Finished in {stopWatch.ElapsedMilliseconds} ms");
            }
        }

        public static Task[] CreateWorkerTasks(int count)
        {
            var tasks = new Task[count];
            for (int i = 0; i < count; i++)
            {
                tasks[i] = Task.Run(DoWork);
                //tasks[i] = DoWorkAsync(); 
            }
            return tasks;
        }

        public static void DoWork()
        {
            Thread.Sleep(10);  
            //DoWorkAsync().Wait(); 
        }

        public static async Task DoWorkAsync()
        {
            await Task.Delay(10);
        }        
    }
}
