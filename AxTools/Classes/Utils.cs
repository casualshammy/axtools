﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using AxTools.Classes.WoW;

namespace AxTools.Classes
{
    internal static class Utils
    {
        internal static readonly Random Rnd = new Random();
        internal static readonly Icon EmptyIcon = Icon.FromHandle(new Bitmap(1, 1).GetHicon());

        internal static T FindForm<T>() where T : Form
        {
            foreach (var i in Application.OpenForms)
            {
                if (i.GetType() == typeof (T)) return i as T;
            }
            return null;
        }

        internal static long CalcDirectorySize(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            long num2 = 0L;
            foreach (FileSystemInfo info2 in info.GetFileSystemInfos())
            {
                if (info2 is FileInfo)
                {
                    num2 += (info2 as FileInfo).Length;
                }
                else if (info2 is DirectoryInfo)
                {
                    num2 += CalcDirectorySize((info2 as DirectoryInfo).FullName);
                }
            }
            return num2;
        }

        internal static void CheckCreateDir()
        {
            if (!Directory.Exists(Globals.TempPath))
            {
                Directory.CreateDirectory(Globals.TempPath);
            }
            if (!Directory.Exists(Globals.UserfilesPath))
            {
                Directory.CreateDirectory(Globals.UserfilesPath);
            }
            if (!Directory.Exists(Globals.CfgPath))
            {
                Directory.CreateDirectory(Globals.CfgPath);
            }
        }

        internal static void Legacy()
        {
            
        }
        
        internal static string GetRandomString(int size)
        {
            StringBuilder builder = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * Rnd.NextDouble() + 65)));
                if (Rnd.Next(10) % 2 == 0)
                {
                    ch = ch.ToString().ToLower()[0];
                }
                builder.Append(ch);
            }
            return builder.ToString();
        }
        
        //internal static void GetFileInfoFromExtension(string extension, out Icon pIcon, out string type)
        //{
        //    WinAPI.SHFILEINFO psfi = new WinAPI.SHFILEINFO();
        //    WinAPI.SHGetFileInfo(extension, 0x80, ref psfi, Marshal.SizeOf(psfi), 0x111 | 0x410);
        //    pIcon = Icon.FromHandle(psfi.HIcon);
        //    type = psfi.TypeName;
        //    if (!IconHandlers.Contains(psfi.HIcon))
        //    {
        //        IconHandlers.Add(psfi.HIcon);
        //    }
        //}

        internal static bool InternetAvailable
        {
            get
            {
                try
                {
                    using (Ping ping = new Ping())
                    {
                        PingReply pingReply = ping.Send("google.com", 2000);
                        return pingReply != null && (pingReply.Status == IPStatus.Success);
                    }
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}