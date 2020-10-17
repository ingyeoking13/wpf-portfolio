using LoginModule;
using System.Windows;
using Views.WPF_PortFolio;

namespace WPF_PortFolio
{
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow = new MainWindow();
            var loginWindow = new LoginWindow();
            if (loginWindow.CheckLogin().Result)
            {
                MainWindow.Show();
            }
            else
            {
                loginWindow.Show();

                loginWindow.Closed += (s, ee) =>
                {
                    if (loginWindow.LoginResult == true)
                    {
                        MainWindow.Show();
                    }
                };
            }
        }
    }
}
