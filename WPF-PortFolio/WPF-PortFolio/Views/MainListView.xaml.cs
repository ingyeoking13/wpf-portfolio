using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_PortFolio.ViewModels;

namespace WPF_PortFolio.Views
{
    public partial class MainListView : UserControl
    {
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(MainListView), new PropertyMetadata(false));

        public MainListView()
        {
            InitializeComponent();
        }

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SearchPopup.IsOpen = false;
        }

        private void searchTB_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up ||
                e.Key == Key.Down ||
                e.Key == Key.Left ||
                e.Key == Key.Right ||
                e.Key == Key.Enter)
            {

            }
            else
            {
                (this.DataContext as MainListViewModel)?.FilterCommand.Execute(null);
            }
        }

        private void _PopupListView_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SearchPopup.IsOpen = false;
        }

        private void searchTB_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            if (e.Key == Key.Enter)
                SearchPopup.IsOpen = false;
            else if (SearchPopup.IsOpen)
            {
                if (e.Key == Key.Down)
                    _PopupListView.SelectedIndex = Math.Min(_PopupListView.SelectedIndex + 1, _PopupListView.Items.Count);
                else if (e.Key == Key.Up)
                    _PopupListView.SelectedIndex = Math.Max(_PopupListView.SelectedIndex - 1, 0);
                else e.Handled = false;
            }
            else e.Handled = false;
        }

        private void _PopupListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void EventTrigger_Changed(object sender, EventArgs e)
        {
          
        }
    }
}
