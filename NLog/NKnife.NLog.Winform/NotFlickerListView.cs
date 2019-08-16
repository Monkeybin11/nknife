using System.Windows.Forms;

namespace NKnife.NLog.WinForm
{
    public class NotFlickerListView : ListView
    {
        public NotFlickerListView()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            BuildLoggerInfoColumn();
        }

        private void BuildLoggerInfoColumn()
        {
            var timeHeader = new ColumnHeader();
            timeHeader.Text = "发生时间";
            timeHeader.Width = 80;

            var logMessageHeader = new ColumnHeader();
            logMessageHeader.Text = "日志信息";
            logMessageHeader.Width = 380;

            var loggerNameHeader = new ColumnHeader();
            loggerNameHeader.Text = "日志源";
            loggerNameHeader.Width = 200;

            Columns.AddRange(
                new[]
                {
                    timeHeader,
                    logMessageHeader,
                    loggerNameHeader
                });
            GridLines = true;
            MultiSelect = false;
            FullRowSelect = true;
            View = View.Details;
            ShowItemToolTips = true;
        }
    }
}
