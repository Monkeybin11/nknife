using System;
using System.IO;
using System.Windows.Forms;
using NKnife.Channels.SerialKnife.Dialogs;
using NKnife.Interface;
using NLog;
using WeifenLuo.WinFormsUI.Docking;

namespace NKnife.Channels.SerialKnife.Views
{
    public partial class Workbench : Form
    {
        private const string DOCK_PANEL_CONFIG = "dockpanel3.config";
        private static readonly ILogger _Logger = LogManager.GetCurrentClassLogger();
        private readonly IAbout _about;
        private readonly LoggerView _loggerView;
        private readonly DockPanel _dockPanel = new DockPanel();
        private readonly SerialPortView _serialPortView;

        public Workbench(WorkbenchViewModel viewModel, IAbout about, LoggerView loggerView, SerialPortView serialPortView)
        {
            _about = about;
            _loggerView = loggerView;
            _serialPortView = serialPortView;
            InitializeComponent();
            _VersionStatusLabel.Text = about.AssemblyVersion.ToString();
            _TotalStatusLabel.Text = string.Empty;
            _CurrentPortStatusLabel.Text = string.Empty;

            _Logger.Info("主窗体构建完成");
            InitializeDockPanel();
            _Logger.Info("添加Dock面板完成");
#if !DEBUG
                        WindowState= FormWindowState.Maximized;
#endif
            _LoggerMenuItem.Click += (sender, args) =>
            {
                loggerView.Show(_dockPanel, DockState.DockBottom);
            };
            viewModel.SerialChannelService.ChannelCountChanged += (sender, args) =>
            {
                //ThreadSafeInvoke(() => { _TotalStatusLabel.Text = $"Total: {_viewModel.SerialChannelService.Count}"; });
            };
            _dockPanel.ActiveDocumentChanged += (sender, args) =>
            {
                _CurrentPortStatusLabel.Text = _dockPanel.ActiveDocument != null ? ((Control) _dockPanel.ActiveDocument).Text : string.Empty;
            };
        }

        private void _NewPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SerialPortSelectorDialog();
            var ds = dialog.ShowDialog(this);
            if (ds == DialogResult.OK && dialog.SerialPort > 0)
            {
                _serialPortView.Text = $"COM{dialog.SerialPort}";
                _serialPortView.ViewModel.Port = dialog.SerialPort;
                _serialPortView.Show(_dockPanel);
                _CurrentPortStatusLabel.Text = _serialPortView.Text;
            }
        }

#region DockPanel

        private static string GetLayoutConfigFile()
        {
            var dir = Path.GetDirectoryName(Application.ExecutablePath);
            return dir != null ? Path.Combine(dir, DOCK_PANEL_CONFIG) : DOCK_PANEL_CONFIG;
        }

        private void InitializeDockPanel()
        {
            _StripContainer.ContentPanel.Controls.Add(_dockPanel);

            _dockPanel.DocumentStyle = DocumentStyle.DockingWindow;
            _dockPanel.Theme = new VS2015BlueTheme();
            _dockPanel.Dock = DockStyle.Fill;
            _dockPanel.BringToFront();

            _loggerView.Show(_dockPanel, DockState.DockBottomAutoHide);

            DockPanelLoadFromXml();
        }

        /// <summary>
        ///     控件提供了一个保存布局状态的方法，它默认是没有保存的。
        /// </summary>
        private void DockPanelSaveAsXml()
        {
            _dockPanel.SaveAsXml(GetLayoutConfigFile());
        }

        private void DockPanelLoadFromXml()
        {
            //加载布局
            var deserializeDockContent = new DeserializeDockContent(GetViewFromPersistString);
            var configFile = GetLayoutConfigFile();
            if (File.Exists(configFile))
            {
                _dockPanel.LoadFromXml(configFile, deserializeDockContent);
            }
        }

        private IDockContent GetViewFromPersistString(string persistString)
        {
//            if (persistString == typeof(LoggerView).ToString())
//            {
//                if (_LoggerViewMenuItem.Checked)
//                    return _LoggerView;
//            }
//            if (persistString == typeof(InterfaceTreeView).ToString())
//            {
//                if (_InterfaceTreeViewMenuItem.Checked)
//                    return _InterfaceTreeView;
//            }
//            if (persistString == typeof(DataMangerView).ToString())
//            {
//                if (_DataManagerViewMenuItem.Checked)
//                    return _DataManagerView;
//            }
//            if (persistString == typeof(CommandConsoleView).ToString())
//            {
//                if (_CommandConsoleViewMenuItem.Checked)
//                    return _CommandConsoleView;
//            }
            return null;
        }

#endregion
    }
}