using System;
using System.ComponentModel;
using System.Windows.Forms;
using NKnife.NLog4.Properties;
using NKnife.NLog4.WinForm;

namespace NKnife.NLog4
{
    public partial class NLogForm : Form
    {
        public NLogForm()
        {
            InitializeComponent();
            Icon = Resources.NLogForm;
            Padding = new Padding(3);
            LoggerListView.AppendLogPanelToContainer(this);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            int h = Screen.PrimaryScreen.Bounds.Height;
            Top = h - Height - 40;

            int w = Screen.PrimaryScreen.Bounds.Width;
            Left = w - Width - 2;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }
    }
}