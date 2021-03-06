﻿using AxTools.Helpers;
using AxTools.WinAPI;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace AxTools.WoW
{
    internal class AutoLogin
    {
        private static readonly Log2 log = new Log2("AutoLogin");
        private readonly WoWAccount2 wowAccount;
        private readonly Process process;

        internal AutoLogin(WoWAccount2 wowAccount, Process process)
        {
            this.wowAccount = wowAccount;
            this.process = process;
        }

        internal void EnterCredentialsASAPAsync()
        {
            Task.Factory.StartNew(EnterCredentialsASAP);
        }

        private void EnterCredentialsASAP()
        {
            var counter = 300;
            while (counter > 0)
            {
                try
                {
                    process.Refresh();
                    if (process.MainWindowHandle != (IntPtr)0)
                    {
                        if (WoWProcessManager.Processes.TryGetValue(process.Id, out WowProcess wowProcess) && wowProcess.Memory != null)
                        {
                            bool okay;
                            if (wowProcess.IsValidBuild)
                            {
                                GlueState glueState = wowProcess.Memory.Read<GlueState>(wowProcess.Memory.ImageBase + WowBuildInfoX64.GlueState);
                                var focusedWidget = wowProcess.Memory.Read<IntPtr>(wowProcess.Memory.ImageBase + WowBuildInfoX64.FocusedWidget);
                                okay = glueState == GlueState.Disconnected && focusedWidget != (IntPtr)0;
                            }
                            else
                            {
                                Thread.Sleep(3000);
                                okay = true;
                            }
                            if (okay)
                            {
                                foreach (char ch in wowAccount.GetLogin())
                                {
                                    NativeMethods.PostMessage(wowProcess.MainWindowHandle, Win32Consts.WM_CHAR, (IntPtr)ch, IntPtr.Zero);
                                    Thread.Sleep(5);
                                }
                                var tabCode = new IntPtr(0x09);
                                NativeMethods.PostMessage(wowProcess.MainWindowHandle, Win32Consts.WM_KEYDOWN, tabCode, IntPtr.Zero);
                                NativeMethods.PostMessage(wowProcess.MainWindowHandle, Win32Consts.WM_KEYUP, tabCode, IntPtr.Zero);
                                Thread.Sleep(5);
                                foreach (char ch in wowAccount.GetPassword())
                                {
                                    NativeMethods.PostMessage(wowProcess.MainWindowHandle, Win32Consts.WM_CHAR, (IntPtr)ch, IntPtr.Zero);
                                    Thread.Sleep(5);
                                }
                                var enterCode = new IntPtr(0x0D);
                                NativeMethods.PostMessage(wowProcess.MainWindowHandle, Win32Consts.WM_KEYDOWN, enterCode, IntPtr.Zero);
                                NativeMethods.PostMessage(wowProcess.MainWindowHandle, Win32Consts.WM_KEYUP, enterCode, IntPtr.Zero);
                                log.Info($"{wowProcess} Credendials have been entered [{Utils.SecureString(wowAccount.GetLogin())}]");
                                break;
                            }
                        }
                    }
                    Thread.Sleep(100);
                    counter--;
                }
                catch (Exception ex)
                {
                    log.Error($"[{process.ProcessName}:{process.Id}] Internal error: {ex.Message}");
                }
            }
        }

        // ReSharper disable UnusedMember.Local
        private enum GlueState
        {
            None = -1,
            Disconnected = 0,
            Updater,
            CharacterSelection = 2,
            CharacterCreation = 3,
            ServerSelection = 6,
            Credits = 7,
            RegionalSelection = 8
        }

        // ReSharper restore UnusedMember.Local
    }
}