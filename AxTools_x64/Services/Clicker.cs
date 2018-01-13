﻿using System;
using System.Linq;
using System.Reflection;
using System.Timers;
using System.Windows.Forms;
using AxTools.Forms;
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
        private static readonly Log2 logger = new Log2("Clicker");

        static Clicker()
        {
            KeyboardWatcher.HotkeyManager.KeyPressed += KeyboardListener2_KeyPressed;
            KeyboardWatcher.HotkeyManager.AddKeys(typeof(Clicker).ToString(), _settings.ClickerHotkey);
        }
        
        internal static void Start(double interval, IntPtr hwnd, IntPtr keyToPress)
        {
            lock (Lock)
            {
                if (_timer == null)
                {
                    _timer = new Timer(interval);
                    _timer.Elapsed += Timer_Elapsed;
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
                    _timer.Elapsed -= Timer_Elapsed;
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

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            NativeMethods.PostMessage(Handle, Win32Consts.WM_KEYDOWN, _key, IntPtr.Zero);
            NativeMethods.PostMessage(Handle, Win32Consts.WM_KEYUP, _key, IntPtr.Zero);
        }

        private static void KeyboardListener2_KeyPressed(KeyboardWatcher.KeyExt obj)
        {
            if (obj == _settings.ClickerHotkey)
            {
                if (_settings.ClickerKey == Keys.None)
                {
                    Notify.TrayPopup("Incorrect input!", "Clicker don't know what key it should press. Click here to setup clicker", NotifyUserType.Error, true, null, 10, (sender, args) =>
                    {
                        ClickerSettings clickerSettings = Utils.FindForm<ClickerSettings>();
                        if (clickerSettings == null)
                        {
                            new ClickerSettings().Show(MainForm.Instance);
                        }
                        else
                        {
                            clickerSettings.ActivateBrutal();
                        }
                    });
                    return;
                }
                if (Enabled)
                {
                    Stop();
                    WowProcess cProcess = WoWProcessManager.List.FirstOrDefault(i => i.MainWindowHandle == Handle);
                    logger.Info(cProcess != null
                        ? string.Format("{0} Disabled", cProcess)
                        : "UNKNOWN:null :: Disabled");
                }
                else
                {
                    WowProcess cProcess = WoWProcessManager.List.FirstOrDefault(i => i.MainWindowHandle == NativeMethods.GetForegroundWindow());
                    if (cProcess != null)
                    {
                        Start(_settings.ClickerInterval, cProcess.MainWindowHandle, (IntPtr)_settings.ClickerKey);
                        logger.Info(string.Format("{0} Enabled, interval {1}ms, window handle 0x{2:X}", cProcess, _settings.ClickerInterval, cProcess.MainWindowHandle.ToInt64()));
                    }
                }
            }
        }

    }
}
