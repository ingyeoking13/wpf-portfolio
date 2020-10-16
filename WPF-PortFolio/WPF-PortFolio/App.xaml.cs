using LoginModule;
using System.Windows;
using Views.WPF_PortFolio;

namespace WPF_PortFolio
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var loginWindow = new LoginWindow();
            if (loginWindow.CheckLogin())
            {
                var mainWindow = new MainWindow();
                mainWindow.Show();

            }
            else
            {
                loginWindow.Show();
            }
            //base.OnStartup(e);
        }
    }
}
