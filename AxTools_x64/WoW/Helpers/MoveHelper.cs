﻿using AxTools.WinAPI;
using AxTools.WoW.Internals;
using System;
using System.Threading;
using System.Windows.Forms;

namespace AxTools.WoW.Helpers
{
    internal static class MoveHelper
    {
        internal static float AngleHorizontal(WowProcess process, WowPoint point)
        {
            WoWPlayerMe me = ObjectMgr.Pulse(process);
            var angle = Convert.ToSingle(Math.Atan2(Convert.ToDouble(point.Y) - Convert.ToDouble(me.Location.Y), Convert.ToDouble(point.X) - Convert.ToDouble(me.Location.X)));
            angle = NegativeAngle(angle);
            return angle;
        }

        internal static float NegativeAngle(float angle)
        {
            //if the turning angle is negative
            if (angle < 0)
                //add the maximum possible angle (PI x 2) to normalize the negative angle
                angle += (float)(Math.PI * 2);
            return angle;
        }

        internal static void FaceHorizontalWithTimer(WowProcess process, float radius, Keys key, bool moving)
        {
            if (radius < 0.1f)
                return;
            var turnTime = moving ? 1328 : 980;
            NativeMethods.SendMessage(process.MainWindowHandle, Win32Consts.WM_KEYDOWN, (IntPtr)key, IntPtr.Zero);
            Thread.Sleep((int)(radius * turnTime * Math.PI / 10));
            NativeMethods.SendMessage(process.MainWindowHandle, Win32Consts.WM_KEYUP, (IntPtr)key, IntPtr.Zero);
        }

        internal static float AngleVertical(WoWPlayerMe me, WowPoint point)
        {
            return -(float)Math.Round(Math.Atan2(me.Location.Distance2D(point), point.Z - me.Location.Z) - Math.PI / 2, 2);
        }
    }
}