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
using System.Windows.Shapes;

namespace WPF_PortFolio.Views
{
    /// <summary>
    /// DragWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DragWindow : Window
    {
        public DragWindow()
        {
            InitializeComponent();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            //this.Left = Mouse.GetPosition(null).X;
           // this.Top = Mouse.GetPosition(null).Y;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           // this.Left = Mouse.GetPosition(null).X;
           // this.Top = Mouse.GetPosition(null).Y;
        }
    }
}
