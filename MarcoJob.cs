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
                switch (marcoEvent.EventType)
                {
                    case MarcoEvent.MarcoEventType.MouseEvent:
                        switch (marcoEvent.MouseKey)
                        {
                            case MarcoEvent.MouseKeyType.LeftClick:
                                switch (marcoEvent.KeyEvent)
                                {
                                    case MarcoEvent.KeyEventType.Down:
                                        inputSimulator.Mouse.LeftButtonDown();
                                        break;

                                    case MarcoEvent.KeyEventType.Up:
                                        inputSimulator.Mouse.LeftButtonUp();
                                        break;

                                    case MarcoEvent.KeyEventType.Press:
                                        inputSimulator.Mouse.LeftButtonClick();
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case MarcoEvent.MouseKeyType.MiddleClick:
                                switch (marcoEvent.KeyEvent)
                                {
                                    case MarcoEvent.KeyEventType.Down:
                                        break;

                                    case MarcoEvent.KeyEventType.Up:
                                        break;

                                    case MarcoEvent.KeyEventType.Press:
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            case MarcoEvent.MouseKeyType.RightClick:
                                switch (marcoEvent.KeyEvent)
                                {
                                    case MarcoEvent.KeyEventType.Down:
                                        inputSimulator.Mouse.RightButtonDown();
                                        break;

                                    case MarcoEvent.KeyEventType.Up:
                                        inputSimulator.Mouse.RightButtonUp();
                                        break;

                                    case MarcoEvent.KeyEventType.Press:
                                        inputSimulator.Mouse.RightButtonClick();
                                        break;

                                    default:
                                        break;
                                }
                                break;

                            default:
                                break;
                        }
                        break;

                    case MarcoEvent.MarcoEventType.KeyboardEvent:
                        switch (marcoEvent.KeyEvent)
                        {
                            case MarcoEvent.KeyEventType.Down:
                                inputSimulator.Keyboard.KeyDown(marcoEvent.KeyboardKey);
                                break;

                            case MarcoEvent.KeyEventType.Up:
                                inputSimulator.Keyboard.KeyUp(marcoEvent.KeyboardKey);
                                break;

                            case MarcoEvent.KeyEventType.Press:
                                inputSimulator.Keyboard.KeyPress(marcoEvent.KeyboardKey);
                                break;

                            default:
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}