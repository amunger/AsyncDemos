using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Boot up");
            Worker.DoWork();
            Console.WriteLine("Shut down");
            Console.ReadLine();
        }
    }

    public static class Worker
    {
        public static void DoWork()
        {
            Console.WriteLine("Starting Work");
            WriteSomeFiles();
            Console.WriteLine("Finished Work");
        }

        public static void WriteSomeFiles()
        {
            Directory.CreateDirectory("c:/temp");
            for (int i = 0; i< 1_000; i++)
            {
                string path = $"c:/temp/file{i}.txt";
                using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.OpenOrCreate)))
                {
                    sw.WriteLine("line");
                    sw.Flush();
                }
            }
        }
    }

}
