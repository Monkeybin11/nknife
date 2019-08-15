﻿using System.Windows;
using System.Windows.Controls;
using NLog;

namespace NKnife.NLog4.WPF
{
    /// <summary>
    /// NLogWpfListView.xaml 的交互逻辑
    /// </summary>
    public partial class NLogWpfGrid : UserControl
    {
        private static readonly LogMessageObservableCollection _LogMessages = LogMessageObservableCollection.Instance;

        public NLogWpfGrid()
        {
            InitializeComponent();
            _LoggerGrid.ItemsSource = _LogMessages;
        }

        private void ClearMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            _LogMessages.Clear();
        }

        private void LevelMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            var filter = new LogMessageFilter();
            var menuItem = (MenuItem) sender;
            var isChecked = menuItem.IsChecked;
            menuItem.IsChecked = !isChecked;
            var header = menuItem.Header.ToString();
            switch (header)
            {
                case "Trace":
                    if (menuItem.IsChecked)
                        filter.Add(LogLevel.Trace);
                    else
                        filter.Remove(LogLevel.Trace);
                    break;
                case "Debug":
                    if (menuItem.IsChecked)
                        filter.Add(LogLevel.Debug);
                    else
                        filter.Remove(LogLevel.Debug);
                    break;
                case "Info":
                    if (menuItem.IsChecked)
                        filter.Add(LogLevel.Info);
                    else
                        filter.Remove(LogLevel.Info);
                    break;
                case "Warn":
                    if (menuItem.IsChecked)
                        filter.Add(LogLevel.Warn);
                    else
                        filter.Remove(LogLevel.Warn);
                    break;
                case "Error":
                    if (menuItem.IsChecked)
                        filter.Add(LogLevel.Error);
                    else
                        filter.Remove(LogLevel.Error);
                    break;
                case "Fatal":
                    if (menuItem.IsChecked)
                        filter.Add(LogLevel.Fatal);
                    else
                        filter.Remove(LogLevel.Fatal);
                    break;
            }
        }
    }
}
