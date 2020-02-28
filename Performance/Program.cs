using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            //ThreadPool.SetMaxThreads(20, 20);
            Worker worker = new Worker(10);

            //worker.SyncMethod(0);
            //worker.AsyncMethod(0).Wait();
            
            worker.CallAsyncMethods().Wait();
            worker.CallSyncMethods();
            
            Console.ReadLine();
        }
    }

    public class Worker
    {
        private static byte[] someBytes = new UnicodeEncoding().GetBytes("...");
        protected string[] fileNames;
        protected string[] asyncFileNames;

        public Worker(int numJobs)
        {
            fileNames = new string[numJobs];
            asyncFileNames = new string[numJobs];
            for (int i = 0; i < numJobs; i++)
            {
                fileNames[i] = $"file{i}.txt";
                asyncFileNames[i] = $"asyncfile{i}.txt";
            }
        }

        public virtual async Task CallAsyncMethods()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < asyncFileNames.Length; i++)
            {
                await AsyncMethod(i);
            }

            stopwatch.Stop();
            Console.WriteLine($"async complete in {stopwatch.ElapsedMilliseconds} ms");
        }

        public virtual void CallSyncMethods()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < fileNames.Length; i++)
            {
                SyncMethod(i);
            }

            stopwatch.Stop();
            Console.WriteLine($"sync complete in {stopwatch.ElapsedMilliseconds} ms");
        }

        public void SyncMethod(int jobId)
        {
            using (FileStream filestream = File.Open(fileNames[jobId], FileMode.OpenOrCreate))
            {
                Thread.Sleep(10);
                //filestream.Seek(0, SeekOrigin.End);
                //filestream.Write(someBytes, 0, someBytes.Length);
                //Console.WriteLine($"{jobId}:{Thread.CurrentThread.ManagedThreadId}");
            }
        }

        public async Task AsyncMethod(int jobId)
        {
            using (FileStream filestream = File.Open(asyncFileNames[jobId], FileMode.OpenOrCreate))
            {
                //Console.WriteLine($"{jobId}:{Thread.CurrentThread.ManagedThreadId}");
                await Task.Delay(10);
                //filestream.Seek(0, SeekOrigin.End);
                //await filestream.WriteAsync(someBytes, 0, someBytes.Length);
                //Console.WriteLine($"{jobId}:{Thread.CurrentThread.ManagedThreadId}");
            }
        }
    }

    public class ParallelWorker : Worker
    {
        public ParallelWorker(int numJobs): base(numJobs)
        { }

        public override Task CallAsyncMethods()
        {
            var tasks = new Task[asyncFileNames.Length];
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < asyncFileNames.Length; i++)
            {
                var jobId = i;
                tasks[i] = AsyncMethod(jobId);
            }

            Task.WaitAll(tasks);
            stopwatch.Stop();
            Console.WriteLine($"async complete in {stopwatch.ElapsedMilliseconds} ms");
            return Task.FromResult(true);
        }

        public override void CallSyncMethods()
        {
            var tasks = new Task[fileNames.Length];
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < fileNames.Length; i++)
            {
                var jobId = i;
                tasks[i] = Task.Run(() => SyncMethod(jobId));
            }

            Task.WaitAll(tasks);
            stopwatch.Stop();
            Console.WriteLine($"sync complete in {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
