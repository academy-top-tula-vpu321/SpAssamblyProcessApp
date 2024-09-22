using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpAssamblyProcessApp
{
    static class Examples
    {
        public static void GetProcessesInfo()
        {
            var process = Process.GetCurrentProcess();

            Console.WriteLine("Current process:");
            Console.WriteLine($"Id: {process.Id}");
            Console.WriteLine($"Handle: {process.Handle}");
            Console.WriteLine($"ProcessName: {process.ProcessName}");
            Console.WriteLine($"PagedMemorySize64: {process.PagedMemorySize64}");
            Console.WriteLine($"VirtualMemorySize64: {process.VirtualMemorySize64}\n\n");

            Console.WriteLine("Processes in memory:");
            foreach (var proc in Process.GetProcesses())
            {
                Console.WriteLine($"Id: {proc.Id}\tName: {proc.ProcessName}");
            }

            Console.WriteLine("\n\nVisual studio processes:");
            Process[] processes = Process.GetProcessesByName("devenv");
            foreach (var proc in processes)
                Console.WriteLine($"Id: {proc.Id}\tHandle: {proc.Handle}\tName: {proc.ProcessName}");
        }

        public static void GetThreadsInfo()
        {
            //Process process = Process.GetCurrentProcess();
            Process process = Process.GetProcessesByName("devenv")[0];

            ProcessThreadCollection threadsProcess = process.Threads;

            foreach (ProcessThread thread in threadsProcess)
            {
                Console.WriteLine($"Id: {thread.Id}\tPriority:{thread.CurrentPriority}");
            }
        }

        public static void GetModelesInfo()
        {
            Process process = Process.GetCurrentProcess();

            ProcessModuleCollection processModules = process.Modules;

            foreach (ProcessModule module in processModules)
            {
                Console.WriteLine($"Name: {module.ModuleName}\tFile: {module.FileName}");
            }
        }

        public static void ProcessStartKill()
        {
            //Process.Start("notepad.exe");
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = "notepad.exe";
            processStartInfo.Arguments = "file.txt";

            //processStartInfo.FileName = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe";
            //processStartInfo.Arguments = "https://yandex.ru";

            Process? process = Process.Start(processStartInfo);

            Console.ReadKey();

            process?.Kill();
        }

        public static void GetAppDomainInfo()
        {
            AppDomain appDomain = AppDomain.CurrentDomain;

            Console.WriteLine($"{appDomain.FriendlyName}");
            Console.WriteLine($"{appDomain.BaseDirectory}");

            Assembly[] assemblies = appDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies)
                Console.WriteLine($"{assembly.GetName().Name}\t{assembly.GetName().Version}");
        }
    }
}
