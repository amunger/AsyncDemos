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
        }

        public static void CallAnAsyncMethod()
        {
            Console.WriteLine("Creating task");
            Task task = WorkAsync();
            Console.WriteLine("Task created");
            task.Wait();
            Console.WriteLine("Task Complete");
        }

        public static async Task WorkAsync()
        {
            Console.WriteLine("begin async method");
            await Task.Delay(500);
        }
    }
}
