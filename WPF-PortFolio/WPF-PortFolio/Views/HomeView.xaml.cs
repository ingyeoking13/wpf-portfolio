using System.Windows.Controls;
using WPF_PortFolio.Utils;

namespace WPF_PortFolio.Views
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void ListView_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {


        }

        private void ListView_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            var sv = (sender as ListView).FindDescendant<ScrollViewer>();
            if (sv == null)
                return;

            if (e.Delta < 0)
                sv.LineRight();
            else
                sv.LineLeft();
            e.Handled = true;
        }
    }
}
