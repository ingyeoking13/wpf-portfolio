using System.Windows;
using WPF_PortFolio.ViewModels;

namespace Views.WPF_PortFolio
{
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel(this);
        }

        private void CloseWindow_Exec(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeWindow(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeWindow(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;
            else
                WindowState = WindowState.Maximized;
        }
    }
}
