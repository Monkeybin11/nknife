using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NKnife.Upgrade4Github;
using NKnife.Win.UpdaterFromGitHub.Example.Properties;

namespace NKnife.Win.UpdaterFromGitHub.Example
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Icon = Resources.main;
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                var arguments = "--u xknife-erian --p nknife.serial-protocol-debugger --r NKnife.Win.UpdaterFromGitHub.Example.exe --v 1.2";
                new Thread(() => Process.Start("UpdaterFromGitHub.exe", arguments)).Start();
                Thread.Sleep(5000);
                Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void _getUpdateInfoButton_Click(object sender, EventArgs e)
        {
            _updateButton.Enabled = false;
            this.Cursor = Cursors.WaitCursor;
            if (Helper.TryGetLatestRelease("xknife-erian", "nknife.serial-protocol-debugger", out var latestRelease, out string errorMessage))
            {
                MessageBox.Show($"{latestRelease.ReleaseTitle}/{latestRelease.Version}", "获取成功", MessageBoxButtons.OK, MessageBoxIcon.Information);
                _updateButton.Enabled = true;
            }
            else
            {
                MessageBox.Show($"获取信息失败！\r\n{errorMessage}", "获取失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _updateButton.Enabled = false;
            }
            this.Cursor = Cursors.Default;
        }
    }
}
