using System;
using System.Collections.Generic;
using System.Text;
using WindowsInput.Native;

namespace POE_Auto_Helper
{
    internal class DemoEvent
    {
        public static List<MarcoEvent> GetTestEvents()
        {
            List<MarcoEvent> marcoEvents = new List<MarcoEvent>();

            var focusEvent = new MarcoEvent()
            {
                EventType = MarcoEvent.MarcoEventType.FocusWindow,
                WindowName = "新瑪奇 mabinogi",
            };
            var mainEvent = new MarcoEvent()
            {
                EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                MouseMoveX = 1325,
                MouseMoveY = 790,
                RepeatCount = 3,
            };
            var subEvent1 = new List<MarcoEvent>()
            {
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                    MouseMoveX = 950,
                    MouseMoveY = 580,
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                    MouseKey = MarcoEvent.MouseKeyType.LeftClick,
                    KeyEvent = MarcoEvent.KeyEventType.Press
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                    MouseMoveX = 1700,
                    MouseMoveY = 580,
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                    MouseKey = MarcoEvent.MouseKeyType.LeftClick,
                    KeyEvent = MarcoEvent.KeyEventType.Press
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                    MouseMoveX = 1700,
                    MouseMoveY = 1000,
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                    MouseKey = MarcoEvent.MouseKeyType.LeftClick,
                    KeyEvent = MarcoEvent.KeyEventType.Press
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                    MouseMoveX = 950,
                    MouseMoveY = 1000,
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                    MouseKey = MarcoEvent.MouseKeyType.LeftClick,
                    KeyEvent = MarcoEvent.KeyEventType.Press
                },
            };

            mainEvent.SubEvents = subEvent1;
            marcoEvents.Add(focusEvent);
            marcoEvents.Add(mainEvent);

            return marcoEvents;
        }
        public static List<MarcoEvent> GetMabinogiSpiritWeaponEvents()
        {
            var maxMoneyInBag = 1130 * 10000;
            var stickPrice = 927 * 99;
            var stickLoopCount = 6;

            List<MarcoEvent> marcoEvents = new List<MarcoEvent>();

            var focusEvent = new MarcoEvent()
            {
                Name = "置頂瑪奇",
                ShowInLogger = true,
                EventType = MarcoEvent.MarcoEventType.FocusWindow,
                WindowName = "新瑪奇 mabinogi",
            };
            var mainEvent = new MarcoEvent()
            {
                Name = "主循環",
                ShowInLogger = true,
                RepeatCount = maxMoneyInBag / (stickPrice * stickLoopCount),
            };
            #region 買棍
            var stick = new MarcoEvent()
            {
                Name = "買棍",
                ShowInLogger = true,
                DelayBefore = 500,
                RepeatCount = stickLoopCount,
            };
            var stickContent = new List<MarcoEvent>()
            {
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                    MouseMoveX = 2322,
                    MouseMoveY = 104,
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                    MouseKey = MarcoEvent.MouseKeyType.LeftClick,
                    KeyEvent = MarcoEvent.KeyEventType.Press
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                    MouseMoveX = 2372,
                    MouseMoveY = 250,
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                    MouseKey = MarcoEvent.MouseKeyType.LeftClick,
                    KeyEvent = MarcoEvent.KeyEventType.Press,
                    DelayAfter = 10,
                    RepeatCount = 10,
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                    MouseMoveX = 2330,
                    MouseMoveY = 300,
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                    MouseKey = MarcoEvent.MouseKeyType.LeftClick,
                    KeyEvent = MarcoEvent.KeyEventType.Press
                },
            };
            stick.SubEvents = stickContent;
            #endregion
            #region 切換至其他
            var switchToOthers = new MarcoEvent()
            {
                Name = "切換至其他",
                ShowInLogger = true,
            };
            var switchToOthersContent = new List<MarcoEvent>()
            {
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                    MouseMoveX = 1130,
                    MouseMoveY = 450,
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                    MouseKey = MarcoEvent.MouseKeyType.LeftClick,
                    KeyEvent = MarcoEvent.KeyEventType.Press
                },
                new MarcoEvent()
                {
                    DelayBefore = 500,
                    EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                    MouseMoveX = 1512,
                    MouseMoveY = 600,
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                    MouseKey = MarcoEvent.MouseKeyType.LeftClick,
                    KeyEvent = MarcoEvent.KeyEventType.Press
                },
            };
            switchToOthers.SubEvents = switchToOthersContent;
            #endregion
            #region 喂精武
            var feedTheWeapon = new MarcoEvent()
            {
                DelayBefore = 250,
                Name = "喂精武",
                ShowInLogger = true,
                RepeatCount = (int)Math.Round((decimal)stick.RepeatCount * 99 / 30, MidpointRounding.ToPositiveInfinity),
                DelayAfter = 0,
            };
            var feedTheWeaponContent = new List<MarcoEvent>()
            {
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                    MouseMoveX = 1412,
                    MouseMoveY = 980,
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                    MouseKey = MarcoEvent.MouseKeyType.LeftClick,
                    KeyEvent = MarcoEvent.KeyEventType.Press,
                    RepeatCount = 3,
                    DelayAfter = 0,
                },
                new MarcoEvent()
                {
                    DelayBefore = 100,
                    EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                    MouseMoveX = 1617,
                    MouseMoveY = 980,
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                    MouseKey = MarcoEvent.MouseKeyType.LeftClick,
                    KeyEvent = MarcoEvent.KeyEventType.Press,
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseMoveEvent,
                    MouseMoveX = 1236,
                    MouseMoveY = 680,
                },
                new MarcoEvent()
                {
                    EventType = MarcoEvent.MarcoEventType.MouseKeyEvent,
                    MouseKey = MarcoEvent.MouseKeyType.LeftClick,
                    KeyEvent = MarcoEvent.KeyEventType.Press,
                },
            };
            feedTheWeapon.SubEvents = feedTheWeaponContent;
            #endregion

            mainEvent.SubEvents.Add(stick);
            mainEvent.SubEvents.Add(switchToOthers);
            mainEvent.SubEvents.Add(feedTheWeapon);
            marcoEvents.Add(focusEvent);
            marcoEvents.Add(mainEvent);

            return marcoEvents;
        }
    }
}
