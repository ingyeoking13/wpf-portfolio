using System.Windows.Controls;
using WPF_PortFolio.Utils;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;

namespace WPF_PortFolio.Views
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        private void ListView_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
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

        //mouse Point 
        Point mousePoint;
        bool isListViewClicked = false;
        ListViewItem currentClicked = null;
        DispatcherTimer dt = null;

        private void ListView_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {

            }
        }

        private void ListView_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var lv = sender as ListView;
            if (lv == null)
                return;

            if (e.ClickCount == 1)
            {
                mousePoint = e.GetPosition(this);
                isListViewClicked = true;
            }
        }

        private void ListView_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (!isListViewClicked) 
                return;

            var lv = sender as ListView;
            isListViewClicked = false;
            var curPoint = e.GetPosition(this);
            var delta = curPoint.X - mousePoint.X;
            var sv = Util.FindDescendant<ScrollViewer>(lv);
            sv.ScrollToHorizontalOffset(sv.HorizontalOffset - delta);
        }

        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            currentClicked = (sender as ListViewItem);
            dt = new DispatcherTimer();
            dt.Interval = System.TimeSpan.FromMilliseconds(1300);
            dt.Tick += (s, ee) =>
            {
                System.Console.WriteLine(ee);
                PortfolioListView.SelectedItem = null;
                currentClicked.IsSelected = true;
                if (dt != null)
                    dt.Stop();
            };
            dt.Start();
            e.Handled = true;
        }

        private void ListViewItem_MouseLeave(object sender, MouseEventArgs e)
        {
            if (dt == null)
                return;
            dt.Stop();
            dt = null;
        }
    }
}
