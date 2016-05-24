﻿using System;
using System.Linq;
using System.Timers;
using System.Windows.Forms;
using AxTools.Helpers;
using AxTools.WinAPI;
using AxTools.WoW;
using Timer = System.Timers.Timer;

namespace AxTools.Services
{
    internal static class Clicker
    {
        private static Timer _timer;
        private static IntPtr _key;
        private static readonly object Lock = new object();
        internal static IntPtr Handle { get; private set; }
        private static readonly Settings _settings = Settings.Instance;

        static Clicker()
        {
            HotkeyManager.KeyPressed += KeyboardListener2_KeyPressed;
            HotkeyManager.AddKeys(typeof (Clicker).ToString(), _settings.ClickerHotkey);
        }

        internal static void Start(double interval, IntPtr hwnd, IntPtr keyToPress)
        {
            lock (Lock)
            {
                if (_timer == null)
                {
                    _timer = new Timer(interval);
                    _timer.Elapsed += timer_Elapsed;
                    _key = keyToPress;
                    Handle = hwnd;
                    _timer.Start();
                }
            }
        }

        internal static void Stop()
        {
            lock (Lock)
            {
                if (_timer != null)
                {
                    _timer.Elapsed -= timer_Elapsed;
                    _timer.Stop();
                    _timer.Close();
                    _timer = null;
                }
            }
        }

        internal static bool Enabled
        {
            get
            {
                return _timer != null && _timer.Enabled;
            }
        }

        private static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            NativeMethods.PostMessage(Handle, Win32Consts.WM_KEYDOWN, _key, IntPtr.Zero);
            NativeMethods.PostMessage(Handle, Win32Consts.WM_KEYUP, _key, IntPtr.Zero);
        }

        private static void KeyboardListener2_KeyPressed(Keys obj)
        {
            if (obj == _settings.ClickerHotkey)
            {
                if (_settings.ClickerKey == Keys.None)
                {
                    Notify.SmartNotify("Incorrect input!", "Please select key to be pressed", NotifyUserType.Error, true);
                    return;
                }
                if (Enabled)
                {
                    Stop();
                    WowProcess cProcess = WoWProcessManager.List.FirstOrDefault(i => i.MainWindowHandle == Handle);
                    Log.Info(cProcess != null
                        ? string.Format("{0} [Clicker] Disabled", cProcess)
                        : "UNKNOWN:null :: [Clicker] Disabled");
                }
                else
                {
                    WowProcess cProcess = WoWProcessManager.List.FirstOrDefault(i => i.MainWindowHandle == NativeMethods.GetForegroundWindow());
                    if (cProcess != null)
                    {
                        Start(_settings.ClickerInterval, cProcess.MainWindowHandle, (IntPtr)_settings.ClickerKey);
                        Log.Info(string.Format("{0} [Clicker] Enabled, interval {1}ms, window handle 0x{2:X}", cProcess, _settings.ClickerInterval, cProcess.MainWindowHandle.ToInt64()));
                    }
                }
            }
        }

    }
}
