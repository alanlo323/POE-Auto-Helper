using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Quartz.Impl;
using Quartz;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using WindowsInput.Native;
using System.Security.Principal;
using System.Reflection;

namespace POE_Auto_Helper
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
            Console.WriteLine("Start Marco after 3 sec");

            Thread.Sleep(3 * 1000);

            Console.WriteLine("Running...");

            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(handle, 6);

            RunMarco(marcoEvents);

            Console.ReadKey();
        }

        private static List<MarcoEvent> LoadSetting(FileInfo runingLocation)
        {
            try
            {
                FileInfo file = new FileInfo($@"{runingLocation.Directory}/marco.json");
                if (!file.Exists)
                {
                    List<MarcoEvent> marcoEvents = new List<MarcoEvent>
                    {
                        new MarcoEvent()
                        {
                            EventType = MarcoEvent.MarcoEventType.KeyboardEvent,
                            KeyboardKey = VirtualKeyCode.VK_W,
                            KeyEvent = MarcoEvent.KeyEventType.Press,
                            StartTime = TimeSpan.FromSeconds(1),
                            TimeInterval = TimeSpan.FromSeconds(6.4),
                            Repeat = MarcoEvent.RepeatType.RepeatForever
                        },
                        new MarcoEvent()
                        {
                            EventType = MarcoEvent.MarcoEventType.KeyboardEvent,
                            KeyboardKey = VirtualKeyCode.VK_E,
                            KeyEvent = MarcoEvent.KeyEventType.Press,
                            StartTime = TimeSpan.FromSeconds(1),
                            TimeInterval = TimeSpan.FromSeconds(6.4),
                            Repeat = MarcoEvent.RepeatType.RepeatForever
                        },
                        new MarcoEvent()
                        {
                            EventType = MarcoEvent.MarcoEventType.KeyboardEvent,
                            KeyboardKey = VirtualKeyCode.VK_R,
                            KeyEvent = MarcoEvent.KeyEventType.Press,
                            StartTime = TimeSpan.FromSeconds(1),
                            TimeInterval = TimeSpan.FromSeconds(8.3),
                            Repeat = MarcoEvent.RepeatType.RepeatForever
                        },
                        new MarcoEvent()
                        {
                            EventType = MarcoEvent.MarcoEventType.KeyboardEvent,
                            KeyboardKey = VirtualKeyCode.VK_T,
                            KeyEvent = MarcoEvent.KeyEventType.Press,
                            StartTime = TimeSpan.FromSeconds(1),
                            TimeInterval = TimeSpan.FromSeconds(3.4),
                            Repeat = MarcoEvent.RepeatType.RepeatForever
                        },
                    };
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

        private async static void RunMarco(List<MarcoEvent> marcoEvents)
        {
            // Grab the Scheduler instance from the Factory
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = await factory.GetScheduler();

            // and start it off
            await scheduler.Start();

            foreach (MarcoEvent marcoEvent in marcoEvents)
            {
                // define the job and tie it to our HelloJob class
                IJobDetail job = JobBuilder.Create<MarcoJob>()
                    .WithIdentity(marcoEvent.GetHashCode().ToString(), "group1")
                    .UsingJobData("marcoEvent", JsonConvert.SerializeObject(marcoEvent))
                    .Build();

                // Trigger the job to run now, and then repeat every 10 seconds
                ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(marcoEvent.GetHashCode().ToString(), "group1")
                .StartAt(new DateTimeOffset(DateTime.Now + marcoEvent.StartTime, new TimeSpan(8, 0, 0)))
                .WithSimpleSchedule(x =>
                {
                    switch (marcoEvent.Repeat)
                    {
                        case MarcoEvent.RepeatType.OneTime:
                            break;

                        case MarcoEvent.RepeatType.RepeatNTimes:
                            x.WithInterval(marcoEvent.TimeInterval);
                            x.WithRepeatCount(marcoEvent.NumberOfRepetitions);
                            break;

                        case MarcoEvent.RepeatType.RepeatForever:
                            x.WithInterval(marcoEvent.TimeInterval);
                            x.RepeatForever();
                            break;

                        default:
                            break;
                    }
                })
                .Build();

                await scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}