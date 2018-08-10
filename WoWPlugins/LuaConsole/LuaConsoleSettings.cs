﻿using AxTools.Helpers;
using KeyboardWatcher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LuaConsole
{
    [DataContract]
    internal class LuaConsoleSettings
    {
        private KeyExt timerHotkey = new KeyExt(Keys.None);
        internal Action<KeyExt> TimerHotkeyChanged;
        
        [DataMember(Name = "WindowSize")]
        internal Size WindowSize = new Size(650, 354);

        [DataMember(Name = "TimerRnd")]
        internal bool TimerRnd = true;

        [DataMember(Name = "IgnoreGameState")]
        internal bool IgnoreGameState = false;

        [DataMember(Name = "TimerInterval")]
        internal int TimerInterval = 1000;

        [DataMember(Name = "TimerHotkey")]
        internal KeyExt TimerHotkey
        {
            get
            {
                return timerHotkey;
            }
            set
            {
                timerHotkey = value;
                TimerHotkeyChanged?.Invoke(timerHotkey);
            }
        }

        [DataMember(Name = "Code")]
        internal string[] Code = new string[] { };

    }
}
