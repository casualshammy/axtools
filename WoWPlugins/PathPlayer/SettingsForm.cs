﻿using AxTools.WoW.Helpers;
using System;
using System.Windows.Forms;

namespace PathPlayer
{
    public partial class SettingsForm : Form
    {
        private PathPlayer pluginInst;

        public SettingsForm()
        {
            InitializeComponent();
        }

        internal static void Open(Settings settingsInstance, PathPlayer plugin)
        {
            SettingsForm fishingConfig = new SettingsForm
            {
                textBoxPath = { Text = settingsInstance.Path },
                checkBoxRandomJumps = { Checked = settingsInstance.RandomJumps },
                pluginInst = plugin
            };
            fishingConfig.ShowDialog();
            settingsInstance.Path = fishingConfig.textBoxPath.Text;
            settingsInstance.RandomJumps = fishingConfig.checkBoxRandomJumps.Checked;
        }

        // ReSharper disable once InconsistentNaming
        private const int CP_NOCLOSE_BUTTON = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog p = new OpenFileDialog { Filter = @"Text files (*.txt, *.json)|*.txt;*.json", InitialDirectory = Utilities.GetPluginSettingsDir(pluginInst) })
            {
                if (p.ShowDialog(this) == DialogResult.OK)
                {
                    textBoxPath.Text = p.FileName;
                }
            }
        }
    }
}