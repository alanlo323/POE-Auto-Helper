using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Quartz;
using WindowsInput.Native;
using WindowsInput;
using static System.Net.Mime.MediaTypeNames;

namespace POE_Auto_Helper
{
    public class MarcoJob : IJob
    {
        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        private const UInt32 WM_KEYUP = 0x101;

        private const UInt32 WM_KEYDOWN = 0x100;

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [STAThread]
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                string json = (string)context.MergedJobDataMap["marcoEvent"];
                MarcoEvent marcoEvent = JsonConvert.DeserializeObject<MarcoEvent>(json);

                InputSimulator inputSimulator = new InputSimulator();
                inputSimulator.Keyboard.KeyPress(marcoEvent.KeyboardKey);
            }
            catch (Exception ex)
            {
            }
        }
    }
}