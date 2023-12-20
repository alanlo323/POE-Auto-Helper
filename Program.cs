using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;

namespace AutoClicker
{
    internal class Program
    {
        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] int nCmdShow);

        public static bool IsAdministrator()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        static public void ExecuteAsAdmin(string fileName)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = fileName;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.Start();
        }

        private static void Main(string[] args)
        {
            #region StartUp

            string strExeFilePath = Assembly.GetExecutingAssembly().Location;
            Console.WriteLine(strExeFilePath);
            strExeFilePath = strExeFilePath.Replace(".dll", ".exe");
            FileInfo fileInfo = new FileInfo(strExeFilePath);
            Process[] processes = Process.GetProcessesByName(fileInfo.Name.Replace(fileInfo.Extension, ""));
            if (processes.Length >= 2)
            {
                Console.WriteLine(processes.Length);
                foreach (var proc in processes)
                {
                    proc.Kill();
                }
            }
            if (!IsAdministrator())
            {
                ExecuteAsAdmin(strExeFilePath);
                return;
            }

            #endregion StartUp

            Console.WriteLine("Loading setting...");

            List<MarcoEvent> marcoEvents = LoadSetting(fileInfo);
            if (marcoEvents == null)
            {
                return;
            }

            //Console.WriteLine("Start Marco after 3 sec");

            //Thread.Sleep(3 * 1000);

            Console.WriteLine("Running...");

            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(handle, 6);

            // Run in background thread
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                RunMarco(marcoEvents);
                Environment.Exit(0);
            }).Start();

            Console.WriteLine("All job done");

            Console.ReadKey();
        }

        private static List<MarcoEvent> LoadSetting(FileInfo runingLocation)
        {
            try
            {
                FileInfo file = new FileInfo($@"{runingLocation.Directory}/marco.json");
                if (!file.Exists)
                {
                    List<MarcoEvent> marcoEvents = DemoEvent.GetMabinogiSpiritWeaponEvents();
                    File.WriteAllText(file.FullName, JsonConvert.SerializeObject(marcoEvents, Formatting.Indented));
                }
                string text = File.ReadAllText(file.FullName);
                return JsonConvert.DeserializeObject<List<MarcoEvent>>(text);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static void RunMarco(List<MarcoEvent> marcoEvents)
        {
            foreach (MarcoEvent marcoEvent in marcoEvents)
            {
                try
                {
                    marcoEvent.Excute();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}