
using System;

namespace Task2dot4
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var mutex = new System.Threading.Mutex(false, "Task2dot4"))
            {
                bool isAnotherInstanceOpen = !mutex.WaitOne(TimeSpan.Zero);
                if (isAnotherInstanceOpen)
                {
                    Console.WriteLine("Only one instance of this app is allowed.");
                    return;
                }
                const string JSON_FILE_PATH = @"C:\Users\PauliusRuikis\Desktop/config.json";
                using (Monitor monitor = JSONReader.GetMonitorFromJsonConfig(JSON_FILE_PATH))
                {
                    monitor.InitializeChecking();
                    monitor.StartChecking();
                }
                Console.ReadKey();
                mutex.ReleaseMutex();
            }
           
        }
    }
}