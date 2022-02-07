using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace TaskProcessKiller
{
    class Program
    {
        static void Main(string[] args)
        {
            string processName = args[0];
            int processLifeTime = Convert.ToInt32(args[1]) * 60000;
            int checkTime = Convert.ToInt32(args[2]) * 60000;
            MonitorKeypressAsync();          
            while (true)
            {
                DateTime date = DateTime.Now;
                Process[] process = Process.GetProcessesByName(processName);
                foreach (Process check in process)
                {
                    if ((date - check.StartTime).TotalMilliseconds > processLifeTime)
                    {
                        check.Kill();
                        Console.WriteLine("Процесс " + processName + " был закрыт");
                    }
                }
                Thread.Sleep(checkTime);               
            }
        }
        public static async void MonitorKeypressAsync()
        {
            await Task.Run(() => MonitorKeypress());
            
        }

        static void MonitorKeypress()
        {
            ConsoleKeyInfo cki = new ConsoleKeyInfo();
            do
            {
                cki = Console.ReadKey();
            } while (cki.Key != ConsoleKey.Q);
            Environment.Exit(0);
        }
    }
}
