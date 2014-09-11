using Boggle.Client;
using OnlinePlayer.ViewModel;
using OnlinePlayer.ViewModels;
using OnlinePlayer.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OnlinePlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        private TabContainer tabContainer;
        public ITabContainer TabContainer { get { return tabContainer; } }
        private TabBtnContainer tabBtnContainer;
        public ITabBtnContainer TabBtnContainer { get { return tabBtnContainer; } }
        Boolean ResizeInProcess = false;

        public MainWindowVM ViewModel { get { return this.DataContext as MainWindowVM; } }

        private bool IsCapched { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            MainWindowVM.Instance.MainWin = this;
            MainWindowVM.Instance.ViewFactory = new ViewFactoryV(MainWindowVM.Instance);

            this.tabContainer = new TabContainer(tcMainWindow);
            this.tabBtnContainer = new TabBtnContainer(pnlTabs);

            //MainVM.Instance.TabMenuBtnStyle = this.Resources["TabMenuItem"] as Style;

            this.DataContext = MainWindowVM.Instance;

            MainWindowVM.Instance.Start();

            MainWindowVM.Instance.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight -
                //здесь нужно так, чтобы не перекрывался скрываемый таксбар
                SystemParameters.BorderWidth * 2;

            new WindowResizer(this,
                new WindowBorder(BorderPosition.Right, right),
                new WindowBorder(BorderPosition.BottomRight, bottomRight),
                new WindowBorder(BorderPosition.Bottom, bottom));
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeWindowSize();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                FullBorder.Visibility = FullBorder.Visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
                this.ChangeWindowSize();
            }

            IsCapched = true;
        }

        private void tcMainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsCapched)
            {
                try
                {
                    this.DragMove();
                }
                catch (InvalidOperationException ex) { }
            }
        }

        private void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            IsCapched = false;
        }

        private void ChangeWindowSize()
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Singleton.Instance.Height = tcMainWindow.ActualHeight;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            GC.Collect();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.Key == Key.Escape && FullBorder.Visibility == Visibility.Collapsed)
            {
                FullBorder.Visibility = Visibility.Visible;
                this.ChangeWindowSize();
            }
        }
    }
}
