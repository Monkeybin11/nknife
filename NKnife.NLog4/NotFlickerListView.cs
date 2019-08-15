using System.Windows.Forms;

namespace NKnife.NLog4
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
            timeHeader.Text = "LogPanel_Time_Header";// UtilityResource.GetString(StringResource.ResourceManager, "LogPanel_Time_Header");
            timeHeader.Width = 80;

            var logMessageHeader = new ColumnHeader();
            logMessageHeader.Text = "LogPanel_Info_Header";//UtilityResource.GetString(StringResource.ResourceManager, "LogPanel_Info_Header");
            logMessageHeader.Width = 380;

            var loggerNameHeader = new ColumnHeader();
            loggerNameHeader.Text = "LogPanel_Source_Header";//UtilityResource.GetString(StringResource.ResourceManager, "LogPanel_Source_Header");
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
