using System;
using System.Threading.Tasks;

namespace Exceptional
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var worker = new Worker();
                worker.DoWork();
                //worker.DoWorkAsync().Wait();
                //_ = worker.DoWorkAsync();

                //worker.workEvent += worker.WorkEvent_Handler;
                //worker.TriggerEvent();

                //Action action = async () => await worker.DoWorkAsync();
                //action();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("finished");
            Console.ReadLine();
        }
    }

    public class Worker
    {
        public event EventHandler workEvent;

        public void DoWork()
        {
            throw new InvalidOperationException();
        }

        public async Task DoWorkAsync()
        {
            await Task.Delay(100);
            throw new InvalidOperationException();
        }

        public void TriggerEvent()
        {
            workEvent.Invoke(null, null);
        }

        public async void WorkEvent_Handler(object caller, EventArgs args)
        {
            await DoWorkAsync();
        }
    }
}
