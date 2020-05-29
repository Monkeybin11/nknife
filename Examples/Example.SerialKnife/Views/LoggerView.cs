using System.Drawing;
using System.Windows.Forms;
using NKnife.NLog.WinForm;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Channels.SerialKnife.Views
{
    public partial class LoggerView : DockContent
    {
        public LoggerView()
        {
            InitializeComponent();
            var logPanel = new LoggerListView
            {
                Dock = DockStyle.Fill,
                Font = new Font("Tahoma", 8.25F),
                Location = new Point(0, 0),
                TabIndex = 0,
                ToolStripVisible = true
            };
            logPanel.SetDebugMode(true);
            Controls.Add(logPanel);
        }
    }
}
