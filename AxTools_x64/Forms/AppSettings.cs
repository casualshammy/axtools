﻿using AxTools.Helpers;

using AxTools.Services;
using AxTools.Services.PingerHelpers;
using Components.Forms;
using MetroFramework;
using MetroFramework.Forms;
using Microsoft.Win32;
using Microsoft.Win32.TaskScheduler;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Action = System.Action;
using Settings2 = AxTools.Helpers.Settings2;
using Task = System.Threading.Tasks.Task;

namespace AxTools.Forms
{
    internal partial class AppSettings : BorderedMetroForm
    {
        private const string StartupTaskName = "AxTools_Startup";
        private static readonly Log2 log = new Log2("AppSettings");
        private readonly Settings2 settings = Settings2.Instance;

        internal AppSettings()
        {
            InitializeComponent();
            StyleManager.Style = Settings2.Instance.StyleColor;
            Icon = Helpers.Resources.ApplicationIcon;
            tabControl.SelectedIndex = 0;
            SetupData();
            SetupEvents();
        }

        internal AppSettings(int tabPage) : this()
        {
            tabControl.SelectedIndex = tabPage;
        }

        private void SetupData()
        {
            ComboBox_server_ip.Items.Clear();
            ComboBox_server_ip.Items.AddRange(GameServers.Entries.Select(k => k.Description).Cast<object>().ToArray());
            ComboBox_server_ip.SelectedIndex = settings.PingerServerID;
            checkBoxAddonsBackup.Checked = settings.WoWAddonsBackupIsActive;
            numericUpDownBackupCopiesToKeep.Value = settings.WoWAddonsBackupNumberOfArchives;
            numericUpDownBackupTimer.Value = settings.WoWAddonsBackupMinimumTimeBetweenBackup;
            metroComboBoxBackupCompressionLevel.SelectedIndex = settings.WoWAddonsBackupCompressionLevel;
            textBoxBackupPath.Text = settings.WoWAddonsBackupPath;
            metroComboBoxStyle.SelectedIndex = settings.StyleColor == 0 ? 0 : (int)settings.StyleColor - 1;
            checkBoxMinimizeToTray.Checked = settings.MinimizeToTray;
            checkBoxPluginsUpdate.Checked = settings.UpdatePlugins;
            checkBox_AntiAFK.Checked = settings.WoWAntiKick;
            CheckBox7.Checked = settings.WoWCustomWindowNoBorder;
            CheckBox6.Checked = settings.WoWCustomizeWindow;
            foreach (Control i in new Control[] { CheckBox7, GroupBox1, GroupBox2 })
            {
                i.Enabled = CheckBox6.Checked;
            }
            TextBox7.Text = settings.WoWCustomWindowRectangle.Width.ToString();
            TextBox6.Text = settings.WoWCustomWindowRectangle.Height.ToString();
            TextBox5.Text = settings.WoWCustomWindowRectangle.X.ToString();
            TextBox4.Text = settings.WoWCustomWindowRectangle.Y.ToString();
            using (TaskService ts = new TaskService())
            {
                CheckBoxStartAxToolsWithWindows.Checked = ts.RootFolder.AllTasks.Any(l => l.Name == StartupTaskName && l.Enabled);
            }
            toolTip.SetToolTip(checkBox_AntiAFK, "Enables anti kick function for WoW.\r\nIt will prevent your character\r\nfrom /afk status");
            checkBoxMakeBackupNotWhilePlaying.Checked = settings.WoWAddonsBackup_DoNotCreateBackupWhileWoWClientIsRunning;
            toolTip.SetToolTip(checkBoxMakeBackupNotWhilePlaying, "Backup creation is CPU-intensive operation and can cause lag.\r\nThis option will prevent AxTools from making backups while WoW client is running.");
            checkBoxSetAfkStatus.Checked = settings.WoW_AntiKick_SetAfkState;
            checkBoxSetAfkStatus.Enabled = checkBox_AntiAFK.Checked;
            toolTip.SetToolTip(checkBoxSetAfkStatus, "This feature will not work if WoW client is minimized");
            checkBoxClearWoWCache.Checked = settings.WoWClearCache;
            checkBoxSendLogOnShutdown.Checked = settings.SendLogToDeveloperOnShutdown;
        }

        private void SetupEvents()
        {
            CheckBoxStartAxToolsWithWindows.CheckedChanged += CheckBoxStartAxToolsWithWindows_CheckedChanged;
            CheckBox7.CheckedChanged += CheckBox7CheckedChanged;
            CheckBox6.CheckedChanged += CheckBox6CheckedChanged;
            TextBox7.TextChanged += TextBox7TextChanged;
            TextBox6.TextChanged += TextBox6TextChanged;
            TextBox5.TextChanged += TextBox5TextChanged;
            TextBox4.TextChanged += TextBox4TextChanged;
            ComboBox_server_ip.SelectedIndexChanged += ComboBox_server_ip_SelectedIndexChanged;
            buttonBackupPath.Click += ButtonBackupPathClick;
            metroComboBoxBackupCompressionLevel.SelectedIndexChanged += MetroComboBoxBackupCompressionLevelSelectedIndexChanged;
            checkBoxPluginsUpdate.CheckedChanged += MetroCheckBox1_CheckedChanged;
            metroComboBoxStyle.SelectedIndexChanged += MetroComboBoxStyle_SelectedIndexChanged;
            linkShowLog.Click += LinkShowLog_Click;
            linkSendLogToDev.Click += LinkSendLogToDev_Click;
            checkBoxMinimizeToTray.CheckedChanged += CheckBoxMinimizeToTray_CheckedChanged;
            checkBoxAddonsBackup.CheckedChanged += CheckBoxAddonsBackupCheckedChanged;
            numericUpDownBackupCopiesToKeep.ValueChanged += NumericUpDownBackupCopiesToKeepValueChanged;
            numericUpDownBackupTimer.ValueChanged += NumericUpDownBackupTimerValueChanged;
            checkBox_AntiAFK.CheckedChanged += CheckBox1CheckedChanged;
            buttonIngameKeyBinds.Click += ButtonIngameKeyBinds_Click;
            checkBoxMakeBackupNotWhilePlaying.CheckedChanged += CheckBoxMakeBackupNotWhilePlaying_CheckedChanged;
            checkBoxSetAfkStatus.CheckedChanged += CheckBoxSetAfkStatus_CheckedChanged;
            checkBoxClearWoWCache.CheckedChanged += CheckBoxClearWoWCache_CheckedChanged;
            checkBoxSendLogOnShutdown.CheckedChanged += CheckBoxSendLogOnShutdown_CheckedChanged;
        }

        private void CheckBoxSendLogOnShutdown_CheckedChanged(object sender, EventArgs e)
        {
            settings.SendLogToDeveloperOnShutdown = checkBoxSendLogOnShutdown.Checked;
        }

        private void CheckBoxClearWoWCache_CheckedChanged(object sender, EventArgs e)
        {
            settings.WoWClearCache = checkBoxClearWoWCache.Checked;
        }

        private void CheckBoxSetAfkStatus_CheckedChanged(object sender, EventArgs e)
        {
            settings.WoW_AntiKick_SetAfkState = checkBoxSetAfkStatus.Checked;
        }

        private void CheckBoxMakeBackupNotWhilePlaying_CheckedChanged(object sender, EventArgs e)
        {
            settings.WoWAddonsBackup_DoNotCreateBackupWhileWoWClientIsRunning = checkBoxMakeBackupNotWhilePlaying.Checked;
        }

        private void ButtonIngameKeyBinds_Click(object sender, EventArgs e)
        {
            AppSettingsWoWBinds form = Utils.FindForms<AppSettingsWoWBinds>().FirstOrDefault();
            if (form != null)
            {
                form.Show();
                form.Activate();
            }
            else
            {
                new AppSettingsWoWBinds().ShowDialog(this);
            }
        }
        
        private void CheckBoxStartAxToolsWithWindows_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBoxStartAxToolsWithWindows.Checked)
            {
                using (TaskService ts = new TaskService())
                {
                    using (TaskDefinition td = ts.NewTask())
                    {
                        td.Triggers.Add(new LogonTrigger());
                        td.Actions.Add(new ExecAction(Application.ExecutablePath, null, Application.StartupPath));
                        td.Settings.AllowDemandStart = true;
                        td.Settings.AllowHardTerminate = true;
                        td.Settings.ExecutionTimeLimit = TimeSpan.Zero;
                        td.Settings.Priority = ProcessPriorityClass.Normal;
                        td.Settings.DisallowStartIfOnBatteries = false;
                        td.Settings.Enabled = true;
                        td.Principal.LogonType = TaskLogonType.InteractiveToken;
                        td.Principal.RunLevel = TaskRunLevel.Highest;
                        ts.RootFolder.RegisterTaskDefinition(StartupTaskName, td);
                    }
                }
            }
            else
            {
                using (TaskService ts = new TaskService())
                {
                    ts.RootFolder.DeleteTask(StartupTaskName);
                }
            }
        }

        private void CheckBox7CheckedChanged(object sender, EventArgs e)
        {
            settings.WoWCustomWindowNoBorder = CheckBox7.Checked;
        }

        private void CheckBox6CheckedChanged(object sender, EventArgs e)
        {
            settings.WoWCustomizeWindow = CheckBox6.Checked;
            foreach (Control i in new Control[] { CheckBox7, GroupBox1, GroupBox2 })
            {
                i.Enabled = CheckBox6.Checked;
            }
        }

        private void TextBox7TextChanged(object sender, EventArgs e)
        {
            if (TextBox7.Text != string.Empty && Convert.ToUInt16(TextBox7.Text) >= 720)
            {
                ErrorProviderExt.ClearError(TextBox7);
                settings.WoWCustomWindowRectangle.Width = Convert.ToUInt16(TextBox7.Text);
            }
            else
            {
                ErrorProviderExt.SetError(TextBox7, "Incorrect value! It must be bigger than 720px", Color.Red);
            }
        }

        private void TextBox6TextChanged(object sender, EventArgs e)
        {
            if (TextBox6.Text != string.Empty && Convert.ToUInt16(TextBox6.Text) >= 576)
            {
                ErrorProviderExt.ClearError(TextBox6);
                settings.WoWCustomWindowRectangle.Height = Convert.ToUInt16(TextBox6.Text);
            }
            else
            {
                ErrorProviderExt.SetError(TextBox6, "Incorrect value! It must be bigger than 576px", Color.Red);
            }
        }

        private void TextBox5TextChanged(object sender, EventArgs e)
        {
            if (TextBox5.Text != string.Empty && Convert.ToInt16(TextBox5.Text) >= 0)
            {
                ErrorProviderExt.ClearError(TextBox5);
                settings.WoWCustomWindowRectangle.X = Convert.ToInt32(TextBox5.Text);
            }
            else
            {
                ErrorProviderExt.SetError(TextBox5, "Incorrect value! It must be bigger than zero", Color.Red);
            }
        }

        private void TextBox4TextChanged(object sender, EventArgs e)
        {
            if (TextBox4.Text != string.Empty && Convert.ToInt32(TextBox4.Text) >= 0)
            {
                ErrorProviderExt.ClearError(TextBox4);
                settings.WoWCustomWindowRectangle.Y = Convert.ToInt32(TextBox4.Text);
            }
            else
            {
                ErrorProviderExt.SetError(TextBox4, "Incorrect value! It must be bigger than zero", Color.Red);
            }
        }

        private void CheckBox1CheckedChanged(object sender, EventArgs e)
        {
            settings.WoWAntiKick = checkBox_AntiAFK.Checked;
            checkBoxSetAfkStatus.Enabled = checkBox_AntiAFK.Checked;
        }

        private void CheckBoxAddonsBackupCheckedChanged(object sender, EventArgs e)
        {
            settings.WoWAddonsBackupIsActive = checkBoxAddonsBackup.Checked;
        }

        private void NumericUpDownBackupCopiesToKeepValueChanged(object sender, EventArgs e)
        {
            settings.WoWAddonsBackupNumberOfArchives = (int)numericUpDownBackupCopiesToKeep.Value;
        }

        private void NumericUpDownBackupTimerValueChanged(object sender, EventArgs e)
        {
            settings.WoWAddonsBackupMinimumTimeBetweenBackup = (int)numericUpDownBackupTimer.Value;
        }

        private void ButtonBackupPathClick(object sender, EventArgs e)
        {
            using (FolderBrowserDialog p = new FolderBrowserDialog { ShowNewFolderButton = false, SelectedPath = string.Empty })
            {
                p.Description = "Select addons backup directory:";
                if (p.ShowDialog(this) == DialogResult.OK)
                {
                    textBoxBackupPath.Text = p.SelectedPath;
                    settings.WoWAddonsBackupPath = p.SelectedPath;
                }
            }
        }

        private void MetroComboBoxBackupCompressionLevelSelectedIndexChanged(object sender, EventArgs e)
        {
            settings.WoWAddonsBackupCompressionLevel = metroComboBoxBackupCompressionLevel.SelectedIndex;
        }

        private void MetroComboBoxStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            var style = metroComboBoxStyle.SelectedIndex == 0 ? 0 : metroComboBoxStyle.SelectedIndex + 1;
            settings.StyleColor = (MetroColorStyle)style;
            foreach (object i in Application.OpenForms)
            {
                if (i.GetType().ParentTypes().Any(l => l == typeof(MetroForm)) && (i as MetroForm).StyleManager != null)
                {
                    (i as MetroForm).StyleManager.Style = (MetroColorStyle)style;
                }
            }
        }

        private void LinkShowLog_Click(object sender, EventArgs e)
        {
            if (File.Exists(Globals.LogFileName))
            {
                try
                {
                    Process.Start(Globals.LogFileName);
                }
                catch (Exception ex)
                {
                    this.TaskDialog("Cannot open log file", ex.Message, NotifyUserType.Error);
                }
            }
            else
            {
                this.TaskDialog("Cannot open log file", "It doesn't exist", NotifyUserType.Error);
            }
        }

        private void LinkSendLogToDev_Click(object sender, EventArgs e)
        {
            try
            {
                string subject = InputBox.Input("Any comment? (optional)", settings.StyleColor);
                if (subject != null)
                {
                    WaitingOverlay waitingOverlay = new WaitingOverlay(this, "Please wait...", settings.StyleColor).Show();
                    Task.Factory.StartNew(() =>
                    {
                        try
                        {
                            Log2.UploadLog(subject);
                        }
                        catch (Exception ex)
                        {
                            log.Error("Can't send log: " + ex.Message);
                            this.TaskDialog("Can't send log", ex.Message, NotifyUserType.Error);
                        }
                        finally
                        {
                            BeginInvoke(new Action(waitingOverlay.Close));
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                log.Info("Can't send log file: " + ex.Message);
                this.TaskDialog("Log file sending error", ex.Message, NotifyUserType.Error);
            }
        }

        private void ComboBox_server_ip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox_server_ip.SelectedIndex != -1)
            {
                settings.PingerServerID = ComboBox_server_ip.SelectedIndex;
                if (ComboBox_server_ip.SelectedIndex == 0)
                {
                    if (Pinger.Enabled)
                    {
                        Pinger.Enabled = false;
                    }
                }
                else
                {
                    if (!Pinger.Enabled)
                    {
                        Pinger.Enabled = true;
                    }
                    else
                    {
                        Pinger.Enabled = false;
                        Pinger.Enabled = true;
                    }
                }
            }
        }

        private void CheckBoxMinimizeToTray_CheckedChanged(object sender, EventArgs e)
        {
            settings.MinimizeToTray = checkBoxMinimizeToTray.Checked;
        }

        private void MetroCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            settings.UpdatePlugins = checkBoxPluginsUpdate.Checked;
        }
    }
}