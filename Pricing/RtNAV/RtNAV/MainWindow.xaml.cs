using IniParser;
using IniParser.Model;
using PriceLib.Redis;
using RtNAV.Component;
using RtNAV.Util;
using RtNAV.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RtNAV
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private static IniData s_Ini;
        private static RedisPublishLib s_PublishLib;
        private static SQLTool s_Sql;
        private Ticker m_Ticker;
        private YuantaBase m_YTBase;

        public static RedisPublishLib PublishLib { get { return s_PublishLib; } }
        public static IniData INI { get { return s_Ini; } }
        public static SQLTool SQL { get { return s_Sql; } }


        static MainWindow()
        {
            int port;

            var parser = new FileIniDataParser();
            s_Ini = parser.ReadFile("Config.ini");

            int.TryParse(INI["SYSTEM"]["PUBLISHPORT"], out port);
            s_PublishLib = new RedisPublishLib(INI["SYSTEM"]["PUBLISHIP"], port);
            s_Sql = new SQLTool(INI["SYSTEM"]["SQLIP"], INI["SYSTEM"]["SQLDB"], INI["SYSTEM"]["SQLUSER"], INI["SYSTEM"]["SQLPWD"]);
            s_PublishLib.SubscribeAllChannels();
        }
        public MainWindow()
        {
            InitializeComponent();

            _VersionInfo();
            m_YTBase = new YuantaBase();
            m_Ticker = new Ticker();

            chkCorrection.SetBinding(CheckBox.IsCheckedProperty, new Binding(nameof(m_YTBase.isCorrection)) { Source = m_YTBase, Mode = BindingMode.TwoWay });
            txtInterval.SetBinding(TextBox.TextProperty, new Binding(nameof(m_YTBase.Interval)) { Source = m_YTBase, Mode = BindingMode.TwoWay });
            tbCurrentTime.SetBinding(TextBlock.TextProperty, new Binding(nameof(m_Ticker.Current)) { Source = m_Ticker, Mode = BindingMode.OneWay, StringFormat = "{0:yyyy/MM/dd HH:mm:ss}" });

            int row = 1;
            foreach (var item in m_YTBase.List)
            {
                gdYuanta.RowDefinitions.Add(new RowDefinition());

                _CreateTextBox(item.Value, row, 0, $"txtETFCode{item.Key}", item.Key, TextAlignment.Left, true, false);
                _CreateTextBox(item.Value, row, 2, $"txtNAV{item.Key}", item.Value.NAV.ToString(), TextAlignment.Right, true, true, TextBox.TextProperty, new Binding(nameof(item.Value.NAV)) { Source = item.Value, Mode = BindingMode.TwoWay });
                _CreateCompareList(item.Value, row);
                _CreateTextBox(item.Value, row, 6, $"txtUpdateTime{item.Key}", item.Value.UpdateTime.ToString(), TextAlignment.Center, true, false, TextBox.TextProperty, new Binding(nameof(item.Value.UpdateTime)) { Source = item.Value, Mode = BindingMode.TwoWay, StringFormat = "HH:mm:ss" });
                _CreateTextBox(item.Value, row, 8, $"txtPublishChannel{item.Key}", item.Value.PublishChannel, TextAlignment.Left, false, true, TextBox.TextProperty, new Binding(nameof(item.Value.PublishChannel)) { Source = item.Value, Mode = BindingMode.TwoWay });
                _CreateTextBox(item.Value, row, 10, $"txtChannel{item.Key}", item.Value.Channel, TextAlignment.Left, true, false, TextBox.TextProperty, new Binding(nameof(item.Value.Channel)) { Source = item.Value, Mode = BindingMode.OneWay });
                _CreateCheckBox(item.Value, row, 12, $"chkisPublish{item.Key}", false, CheckBox.IsCheckedProperty, new Binding(nameof(item.Value.isPublish)) { Source = item.Value, Mode = BindingMode.TwoWay });
                row++;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            m_YTBase.Save();
        }
        private void btnAddCompare_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (popAddCompare.IsOpen)
            {
                popAddCompare.IsOpen = false;
            }
            popAddCompare.Tag = btn.Tag;
            popAddCompare.IsOpen = true;
        }
        private void btnYuanta_Click(object sender, RoutedEventArgs e)
        {
            if (m_YTBase.isStart)
            {
                m_YTBase.Stop();
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri("pack://application:,,,/RtNAV;component/off.ico");
                img.EndInit();
                imgYuantaStatus.Source = img;
                tbYuantaStatus.Text = "狀態：關閉";
            }
            else
            {
                m_YTBase.Start();
                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri("pack://application:,,,/RtNAV;component/on.ico");
                img.EndInit();
                imgYuantaStatus.Source = img;
                tbYuantaStatus.Text = "狀態：開啟";
            }
            chkCorrection.IsEnabled = txtInterval.IsEnabled = !m_YTBase.isStart;
        }
        private void btnADDOK_Click(object sender, RoutedEventArgs e)
        {
            Yuanta yt = (Yuanta)popAddCompare.Tag;
            decimal limit = (decimal)udADDDiffLimit.Value;
            string color = string.IsNullOrEmpty(cpADDAlert.SelectedColorText) ? "Blue" : cpADDAlert.SelectedColorText;
            yt.AddCompare(txtADDCompareChannel.Text, (SolidColorBrush)new BrushConverter().ConvertFromString(color), limit);
            _CreateCompareList(yt, Grid.GetRow(MainWindow.FindChild<Grid>(gdYuanta, $"gdCompare{yt.ETFCode}")));
            popAddCompare.IsOpen = false;
        }
        private void btnADDCancel_Click(object sender, RoutedEventArgs e)
        {
            popAddCompare.IsOpen = false;
        }

        #region Private
        /// <summary>
        /// 建立Cell
        /// </summary>
        /// <param name="yt">Yuanta</param>
        /// <param name="row">Row Index</param>
        /// <param name="col">Column Index</param>
        /// <param name="name">TextBox.Name</param>
        /// <param name="text">TextBox.Text</param>
        /// <param name="align">TextAlignment</param>
        /// <param name="isreadonly">Readonly</param>
        /// <param name="isEnable">Enable</param>
        /// <param name="bindingProp">Dependency Property</param>
        /// <param name="binding">Binding</param>
        private void _CreateTextBox(Yuanta yt, int row, int col, string name, string text = "", TextAlignment align = TextAlignment.Left, bool isreadonly = false, bool isEnable = true, DependencyProperty bindingProp = null, Binding binding = null)
        {
            TextBox t = new TextBox() { Name = name, Text = text, TextAlignment = align, IsReadOnly = isreadonly, IsEnabled = isEnable, VerticalContentAlignment = VerticalAlignment.Center, Tag = yt };
            Grid.SetRow(t, row);
            Grid.SetColumn(t, col);
            if (bindingProp != null && binding != null)
            {
                t.SetBinding(bindingProp, binding);
            }
            //NAV欄位綁定Background, Foreground For變色
            if (name == $"txtNAV{yt.ETFCode}")
            {
                t.SetBinding(TextBox.BackgroundProperty, new Binding(nameof(yt.Background)) { Source = yt });
                t.SetBinding(TextBox.ForegroundProperty, new Binding(nameof(yt.Foreground)) { Source = yt });
            }
            gdYuanta.Children.Add(t);
        }
        /// <summary>
        /// 建立Cell
        /// </summary>
        /// <param name="yt">Yuanta</param>
        /// <param name="row">Row Index</param>
        /// <param name="col">Column Index</param>
        /// <param name="name">CheckBox.Name</param>
        /// <param name="isreadonly">Readonly</param>
        /// <param name="bindingProp">Dependency Property</param>
        /// <param name="binding">Binding</param>
        private void _CreateCheckBox(Yuanta yt, int row, int col, string name, bool isreadonly = false, DependencyProperty bindingProp = null, Binding binding = null)
        {
            CheckBox cb = new CheckBox() { HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Name = name, Tag = yt, Background = Brushes.White };
            Grid.SetRow(cb, row);
            Grid.SetColumn(cb, col);
            if (bindingProp != null && binding != null)
            {
                cb.SetBinding(bindingProp, binding);
            }
            gdYuanta.Children.Add(cb);
        }
        /// <summary>
        /// 建立Compare Cell
        /// </summary>
        /// <param name="yt">Yuanta</param>
        /// <param name="row">Row Index</param>
        private void _CreateCompareList(Yuanta yt, int row)
        {
            #region Compare的Channel Name
            //建立Compare Channel Name欄位
            FrameworkElementFactory tbCompareChannel = new FrameworkElementFactory(typeof(TextBlock));
            tbCompareChannel.SetValue(TextBlock.NameProperty, $"tbCompareChannnel{yt.ETFCode}");
            tbCompareChannel.SetValue(TextBlock.FontFamilyProperty, new FontFamily("Courier New"));
            tbCompareChannel.SetValue(TextBlock.ForegroundProperty, new Binding($"Value.{nameof(Compare.AlertBrush)}"));
            tbCompareChannel.SetValue(TextBlock.ToolTipProperty, new Binding($"Value.{nameof(Compare.DiffLimit)}"));
            tbCompareChannel.SetBinding(TextBlock.TextProperty, new Binding($"Value.{nameof(Compare.Channel)}") { StringFormat = "[{0}]" });
            tbCompareChannel.SetBinding(TextBlock.TagProperty, new Binding($"Value.{nameof(Compare.Channel)}"));
            tbCompareChannel.AddHandler(TextBlock.MouseRightButtonDownEvent, new MouseButtonEventHandler(_DeleteCompare));
            #endregion
            #region Compare的Channel Value
            //建立Compare Channel Value
            FrameworkElementFactory tbCompareValue = new FrameworkElementFactory(typeof(TextBlock));
            tbCompareValue.SetValue(TextBlock.NameProperty, $"tbCompareValue{yt.ETFCode}");
            tbCompareValue.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Right);
            tbCompareValue.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
            tbCompareValue.SetValue(TextBlock.WidthProperty, 50D);
            tbCompareValue.SetBinding(TextBlock.TextProperty, new Binding($"Value.{nameof(Compare.Value)}") { StringFormat = "0.#0" });
            tbCompareValue.SetBinding(TextBlock.TagProperty, new Binding($"Value.{nameof(Compare.Channel)}"));
            tbCompareValue.AddHandler(TextBlock.MouseRightButtonDownEvent, new MouseButtonEventHandler(_DeleteCompare));
            #endregion
            #region 放Compare的StackPanel
            FrameworkElementFactory spCompareList = new FrameworkElementFactory(typeof(StackPanel)) { Name = $"spCompareList{yt.ETFCode}" };
            spCompareList.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
            spCompareList.AppendChild(tbCompareChannel);
            spCompareList.AppendChild(tbCompareValue);
            #endregion
            #region 放StackPanel的ListBox
            DataTemplate dt = new DataTemplate();
            dt.VisualTree = spCompareList;
            ListBox list = new ListBox()
            {
                Name = $"lstCompare{yt.ETFCode}",
                ItemsSource = yt.CompareChannel,
                SelectedValuePath = nameof(Compare.Value),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Width = double.NaN,
                ItemTemplate = dt
            };
            #endregion
            #region 新增Compare的按鈕
            Button btnAddCompare = new Button() { Name = $"btnAddCompare{yt.ETFCode}", Content = "...", Tag = yt, FontFamily = new FontFamily("Verdana") };
            btnAddCompare.Click += btnAddCompare_Click;
            #endregion
            #region 放List+按鈕 的Grid
            //移掉舊的
            var oldgrid = MainWindow.FindChild<Grid>(gdYuanta, $"gdCompare{yt.ETFCode}");
            if (oldgrid != null)
            {
                gdYuanta.Children.Remove(oldgrid);
            }
            //建新的
            Grid gdCompare = new Grid() { Name = $"gdCompare{yt.ETFCode}", Tag = row };
            gdCompare.ColumnDefinitions.Add(new ColumnDefinition());
            gdCompare.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(20) });
            Grid.SetColumn(list, 0);
            Grid.SetColumn(btnAddCompare, 1);
            gdCompare.Children.Add(list);
            gdCompare.Children.Add(btnAddCompare);
            //放上去
            Grid.SetRow(gdCompare, row);
            Grid.SetColumn(gdCompare, 4);
            gdYuanta.Children.Add(gdCompare);
            #endregion
        }
        private void _VersionInfo()
        {
            //System.Configuration.TargetFramework
            object[] attribute = typeof(MainWindow).Assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
            AssemblyDescriptionAttribute desc = (AssemblyDescriptionAttribute)(attribute[0]);
            attribute = typeof(MainWindow).Assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
            AssemblyTitleAttribute title = (AssemblyTitleAttribute)(attribute[0]);
            string ostype = Environment.Is64BitProcess ? "x64" : "x86";
#if DEBUG
            Title = $"[{title.Title} - {Process.GetCurrentProcess().Id}] { desc.Description} ({ostype}-D)  V{typeof(MainWindow).Assembly.GetName().Version}";
#else
            return $"[{title.Title} - {Process.GetCurrentProcess().Id}] {desc.Description} ({ostype}-R)  V{Application.ProductVersion}";
#endif
        }

        private void _DeleteCompare(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = (TextBlock)sender;
            string etfcode = tb.Name.Replace("tbCompareValue", "").Replace("tbCompareChannnel", "");
            m_YTBase[etfcode].RemoveCompare(tb.Tag.ToString());
            _CreateCompareList(m_YTBase[etfcode], Grid.GetRow(MainWindow.FindChild<Grid>(gdYuanta, $"gdCompare{etfcode}")));
        }
        #endregion

        #region Static
        public static T FindChild<T>(DependencyObject parent, string childName)
   where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;
            if (!string.IsNullOrEmpty(childName) && (parent as FrameworkElement).Name == childName)
            {
                T parentType = parent as T;
                if (parentType != null)
                {
                    return (T)parent;
                }
            }

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) { break; }
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;

                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }
        public static void WriteConfig()
        {
            var parser = new FileIniDataParser();
            parser.WriteFile("Config.ini", INI);
        }
        #endregion
    }
}