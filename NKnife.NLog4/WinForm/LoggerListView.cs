﻿using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using NLog;

namespace NKnife.NLog4.WinForm
{
    /// <summary>
    ///     日志显示面板控件，日志等级通过左上角的日志等级列表进行筛选
    /// </summary>
    public partial class LoggerListView : UserControl
    {
        private Tuple<Color, Color> _trackColor = new Tuple<Color, Color>(Color.CornflowerBlue, Color.White);
        private Tuple<Color, Color> _debugColor = new Tuple<Color, Color>(Color.DarkSlateBlue, Color.White);
        private Tuple<Color, Color> _infoColor = new Tuple<Color, Color>(Color.Black, Color.White);
        private Tuple<Color, Color> _warnColor = new Tuple<Color, Color>(Color.Black, Color.Khaki);
        private Tuple<Color, Color> _errorColor = new Tuple<Color, Color>(Color.Black, Color.Orange);
        private Tuple<Color, Color> _fatalColor = new Tuple<Color, Color>(Color.White, Color.OrangeRed);

        private LoggerListView()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitializeComponent();
            AutoScaleMode = AutoScaleMode.Font;
            Font = new Font("Tahoma", 8.25F);

            #region Menu Checked

#if DEBUG
            _TraceMenuItem.Checked = true;
            _DebugMenuItem.Checked = true;
#else
            _TraceMenuItem.Checked = false;
            _DebugMenuItem.Checked = false;
#endif
            _InfoMenuItem.Checked = true;
            _WarnMenuItem.Checked = true;
            _ErrorMenuItem.Checked = true;
            _FatalMenuItem.Checked = true;

            #endregion

            ViewModel.LogInfos.CollectionChanged += LogInfos_CollectionChanged;
            SizeChanged += (s, e) => { SetViewColumnSize(); };
            _ListView.MouseDoubleClick += LoggerListViewDoubleClick;
        }

        public LoggerCollectionViewModel ViewModel { get; } = new LoggerCollectionViewModel();

        /// <summary>
        ///     是否显示工具栏
        /// </summary>
        public bool ToolStripVisible
        {
            get { return _ToolStrip.Visible; }
            set { _ToolStrip.Visible = value; }
        }

        /// <summary>
        ///     设置Log显示的ListView中各列的宽度
        /// </summary>
        private void SetViewColumnSize()
        {
            if (_ListView.Columns.Count >= 2)
            {
                if (_ListView.Columns[0].Width != 0)
                    _ListView.Columns[0].Width = 80;
                if (_ListView.Columns[2].Width != 0)
                    _ListView.Columns[2].Width = 200;
                _ListView.Columns[1].Width = Width - _ListView.Columns[0].Width - _ListView.Columns[2].Width - 22;
            }
        }

        public void SetColors(
            Tuple<Color, Color> trace, 
            Tuple<Color, Color> debug, 
            Tuple<Color, Color> info, 
            Tuple<Color, Color> warn, 
            Tuple<Color, Color> error,
            Tuple<Color, Color> fatal)
        {
            _trackColor = trace;
            _debugColor = debug;
            _infoColor = info;
            _warnColor = warn;
            _errorColor = error;
            _fatalColor = fatal;
        }

        private void LogInfos_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                {
                    var info = ViewModel.LogInfos[e.NewStartingIndex];
                    var viewItem = new ListViewItem();
                    viewItem.Tag = info.LogInfo;
                    viewItem.Text = info.DateTime.ToString("HH:mm:ss.fff");
                    viewItem.SubItems.Add(new ListViewItem.ListViewSubItem(viewItem, info.LogInfo.FormattedMessage));
                    viewItem.SubItems.Add(new ListViewItem.ListViewSubItem(viewItem,
                        LogUtil.ParseLoggerName(info.Source)));
                    switch (info.LogLevel.Name)
                    {
                        case "Trace":
                            viewItem.ForeColor = _trackColor.Item1;
                            viewItem.BackColor = _trackColor.Item2;
                            break;
                        case "Debug":
                            viewItem.ForeColor = _debugColor.Item1;
                            viewItem.BackColor = _debugColor.Item2;
                            break;
                        case "Info":
                            viewItem.ForeColor = _infoColor.Item1;
                            viewItem.BackColor = _infoColor.Item2;
                            break;
                        case "Warn":
                            viewItem.ForeColor = _warnColor.Item1;
                            viewItem.BackColor = _warnColor.Item2;
                            break;
                        case "Error":
                            viewItem.ForeColor = _errorColor.Item1;
                            viewItem.BackColor = _errorColor.Item2;
                            break;
                        case "Fatal":
                            viewItem.ForeColor = _fatalColor.Item1;
                            viewItem.BackColor = _fatalColor.Item2;
                            break;
                    }
                    _ListView.Items.Insert(0, viewItem);
                    break;
                }
                case NotifyCollectionChangedAction.Remove:
                {
                    _ListView.Items.RemoveAt(e.OldStartingIndex);
                    break;
                }
                case NotifyCollectionChangedAction.Reset:
                {
                    if (ViewModel.LogInfos.Count <= 0)
                        _ListView.Items.Clear();
                    break;
                }
            }
        }

        /// <summary>
        ///     双击一条日志弹出详细窗口
        /// </summary>
        private void LoggerListViewDoubleClick(object sender, MouseEventArgs e)
        {
            var si = _ListView.HitTest(e.X, e.Y);
            var info = (LogEventInfo) si.Item?.Tag;
            if (info != null)
            {
                LoggerInfoDetailForm.Show(info);
            }
        }

        public void SetDebugMode(bool isDebug)
        {
            if (isDebug)
            {
                ViewModel.CurrentLevel = Level.Trace | Level.Debug | Level.Info | Level.Warn | Level.Error | Level.Fatal;
                _TraceMenuItem.Checked = true;
                _DebugMenuItem.Checked = true;
                _InfoMenuItem.Checked = true;
                _WarnMenuItem.Checked = true;
                _ErrorMenuItem.Checked = true;
                _FatalMenuItem.Checked = true;
            }
            else
            {
                ViewModel.CurrentLevel = Level.Info | Level.Warn | Level.Error | Level.Fatal;
                _TraceMenuItem.Checked = false;
                _DebugMenuItem.Checked = false;
                _InfoMenuItem.Checked = true;
                _WarnMenuItem.Checked = true;
                _ErrorMenuItem.Checked = true;
                _FatalMenuItem.Checked = true;
            }
        }

        private void SetLevelGroupToolButton_Click(object sender, EventArgs e)
        {
            var item = (ToolStripMenuItem) sender;
            item.Checked = !item.Checked;
            ViewModel.CurrentLevel = Level.None;
            if (_TraceMenuItem.Checked)
                ViewModel.CurrentLevel = Level.Trace;
            if (_DebugMenuItem.Checked)
                ViewModel.CurrentLevel = ViewModel.CurrentLevel | Level.Debug;
            if (_InfoMenuItem.Checked)
                ViewModel.CurrentLevel = ViewModel.CurrentLevel | Level.Info;
            if (_WarnMenuItem.Checked)
                ViewModel.CurrentLevel = ViewModel.CurrentLevel | Level.Warn;
            if (_ErrorMenuItem.Checked)
                ViewModel.CurrentLevel = ViewModel.CurrentLevel | Level.Error;
            if (_FatalMenuItem.Checked)
                ViewModel.CurrentLevel = ViewModel.CurrentLevel | Level.Fatal;
        }

        private void ClearToolButton_Click(object sender, EventArgs e)
        {
            lock (ViewModel.LogInfos)
            {
                ViewModel.LogInfos.Clear();
            }
        }

        #region 静态辅助方法-创建实例

        /// <summary>
        ///     初始化LogPanel到指定容器
        /// </summary>
        /// <param name="container"></param>
        public static LoggerListView AppendLogPanelToContainer(Panel container)
        {
            var logPanel = Instance;
            logPanel.Dock = DockStyle.Fill;
            logPanel.Font = new Font("Tahoma", 8.25F);
            logPanel.Location = new Point(0, 0);
            logPanel.Name = "_LogPanel";
            logPanel.Size = new Size(673, 227);
            logPanel.TabIndex = 0;
            logPanel.ToolStripVisible = true;
            container.Controls.Add(logPanel);
            return logPanel;
        }

        public static LoggerListView AppendLogPanelToContainer(Form container)
        {
            var logPanel = Instance;
            logPanel.Dock = DockStyle.Fill;
            logPanel.Font = new Font("Tahoma", 8.25F);
            logPanel.Location = new Point(0, 0);
            logPanel.Name = "_LogPanel";
            logPanel.Size = new Size(673, 227);
            logPanel.TabIndex = 0;
            logPanel.ToolStripVisible = true;
            container.Controls.Add(logPanel);
            return logPanel;
        }

        #endregion

        #region 单件实例

        /// <summary>
        ///     获得一个本类型的单件实例.
        /// </summary>
        /// <value>The instance.</value>
        public static LoggerListView Instance => _MyInstance.Value;

        private static readonly Lazy<LoggerListView> _MyInstance = new Lazy<LoggerListView>(() => new LoggerListView());

        #endregion
    }
}