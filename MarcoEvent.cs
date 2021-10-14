using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WindowsInput.Native;

namespace POE_Auto_Helper
{
    internal class MarcoEvent
    {
        public enum MarcoEventType
        {
            MouseEvent,
            KeyboardEvent
        }

        public enum KeyboardKeyType
        {
            K_0,
            K_1,
            K_2,
            K_3,
            K_4,
            K_5,
            K_6,
            K_7,
            K_8,
            K_9,
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            H,
            I,
            J,
            K,
            L,
            M,
            N,
            O,
            P,
            Q,
            R,
            S,
            T,
            U,
            V,
            W,
            X,
            Y,
            Z
        }

        public enum MouseKeyType
        {
            LeftClick,
            MiddleClick,
            RightClick
        }

        public enum KeyEventType
        {
            Down,
            Up,
            Press
        }

        public enum RepeatType
        {
            OneTime,
            RepeatNTimes,
            RepeatForever
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public MarcoEventType EventType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public VirtualKeyCode KeyboardKey { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public MouseKeyType MouseKey { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public KeyEventType KeyEvent { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan TimeInterval { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public RepeatType Repeat { get; set; }

        public int NumberOfRepetitions { get; set; }
    }
}