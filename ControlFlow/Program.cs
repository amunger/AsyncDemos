using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ControlFlow
{
    class Program
    {
        static void Main(string[] args)
        {
            CallAnAsyncMethod();
            Console.ReadLine();

            Console.WriteLine();
            CallAnAsyncMethod();
            Console.ReadLine();
        }

        public static void CallAnAsyncMethod()
        {
            Console.WriteLine("Creating task");
            Task task = WorkAsync();
            Console.WriteLine("Task created");
            // do other stuff
            task.Wait();
            Console.WriteLine("Task Complete");
        }

        public static async Task WorkAsync()
        {
            Console.WriteLine("begin async method");
            await AsyncOperation();
            Console.WriteLine("finished async method");
            Thread.Sleep(500);
            Console.WriteLine("finished sync work");
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
