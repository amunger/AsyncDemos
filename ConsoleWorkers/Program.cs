using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleWorkers
{
    class Program
    {
        static void Main(string[] args)
        {
            ThreadPool.SetMaxThreads(11, 10);
            
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
                tasks[i] = Task.Run(TestConnection);
            }
            return tasks;
        }

        public static void TestConnection()
        {
            //WebRequest request = WebRequest.Create("https://google.com");
            //WebResponse response = request.GetResponse();
            Task.Delay(1000).Wait();
        }
    }
}
