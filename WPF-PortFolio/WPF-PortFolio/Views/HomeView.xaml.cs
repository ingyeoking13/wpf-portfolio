using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using WPF_PortFolio.Utils;
using WPF_PortFolio.ViewModels;

namespace WPF_PortFolio.Views
{
    public partial class HomeView : UserControl
    {
        public HomeView()
        {
            InitializeComponent();
        }

        DragWindow _dragWindow;
        DragAdorner dragAdorner;
        private void CustomerList_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var lvi = getCurrentListViewItem(e);
                DataObject obj = new DataObject(typeof(ListViewItem), lvi.DataContext);
                var adLayer = AdornerLayer.GetAdornerLayer(CustomerList);
                
                //dragAdorner = new DragAdorner(lvi, new Point(0, 0));
                _dragWindow = new DragWindow();
                _dragWindow.RenderSize = lvi.RenderSize;
                var p =
                App.Current.MainWindow.PointToScreen(Mouse.GetPosition(App.Current.MainWindow));
                _dragWindow.Top = p.Y;
                _dragWindow.Width = p.X;
                _dragWindow.Width = lvi.RenderSize.Width;
                _dragWindow.Height = lvi.RenderSize.Height;
                _dragWindow.Background = new VisualBrush(lvi)
                {
                    Opacity = .7
                };
                _dragWindow.Show();

                //_dragWindow.PointFromScreen(Mouse.GetPosition(null));
                //adLayer.Add(dragAdorner);
                DragDrop.DoDragDrop(lvi, obj, DragDropEffects.Move);
                _dragWindow.Close();
                //adLayer.Remove(dragAdorner);
            }
        }

        private ListViewItem getCurrentListViewItem(object e)
        {
            Point pos;
            if (e is MouseEventArgs mea)
                pos = mea.GetPosition(CustomerList);
            else if (e is DragEventArgs dea)
                pos = dea.GetPosition(CustomerList);
            else 
                return null;

            var obj = VisualTreeHelper.HitTest(CustomerList, pos).VisualHit;
            while (VisualTreeHelper.GetParent(obj) != null && !(obj is ListViewItem))
            {
                obj = VisualTreeHelper.GetParent(obj);
            }

            if (obj is ListViewItem lvi)
                return lvi;
            return null;
        }

        private void CustomerList_Drop(object sender, DragEventArgs e)
        {
            var lvi = getCurrentListViewItem(e);
            if (lvi == null)
                return;
            var Users = (this.DataContext as HomeViewModel).Users;
            int targetIndex = Users.IndexOf(lvi.DataContext as User);
            var sourceUser = e.Data.GetData(typeof(ListViewItem)) as User;
            if (sourceUser == lvi.DataContext)
                return;

            int sourceIndex = Users.IndexOf(sourceUser);
            int removingIndex = sourceIndex;
            if (sourceIndex < targetIndex)
                targetIndex++;
            else
                removingIndex++;
            if (targetIndex >= Users.Count)
                Users.Add(sourceUser);
            else
                Users.Insert(targetIndex, sourceUser);

            Users.RemoveAt(removingIndex);
        }

        private void CustomerList_DragOver(object sender, DragEventArgs e)
        {
            DependencyObject obj = CustomerList;
            obj = FindChild<ScrollViewer>(obj);
            
            if (obj is ScrollViewer sv)
            {
                double tolerance = 10;
                double mouseY = e.GetPosition(CustomerList).Y;
                double offset = 3;

                if (mouseY < tolerance)
                {
                    sv.ScrollToVerticalOffset(sv.VerticalOffset - offset);
                }
                else if (mouseY > CustomerList.ActualHeight - tolerance)
                {
                    sv.ScrollToVerticalOffset(sv.VerticalOffset + offset);
                }
            }
        }

        private void lbl_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            if (dragAdorner != null)
            {
                var pos = Mouse.GetPosition(null);
                dragAdorner.UpdatePosition(pos);
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        public static Point GetMousePosition()
        {
            Win32Point w32Mouse = new Win32Point();
            GetCursorPos(ref w32Mouse);
            return new Point(w32Mouse.X, w32Mouse.Y);
        }

        private static TChild FindChild<TChild>(DependencyObject dependencyObject) where TChild : DependencyObject
        {
            int childCount = VisualTreeHelper.GetChildrenCount(dependencyObject);
            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(dependencyObject, i);
                if (child != null && child is TChild)
                {
                    return (TChild)child;
                }
                else
                {
                    TChild grandChild = FindChild<TChild>(child);
                    if (grandChild != null)
                    {
                        return grandChild;
                    }
                }
            }
            return null;
        }

        private void CustomerList_PreviewDragLeave(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void CustomerList_PreviewDragEnter(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }
    }
    public class DragAdorner : Adorner
    {
        public DragAdorner(UIElement adornedElement, Point offset)
            : base(adornedElement)
        {
            this.offset = offset;
            vbrush = new VisualBrush(AdornedElement);
            vbrush.Opacity = .7;
        }



        public void UpdatePosition(Point location)
        {
            this.location = location;
            this.InvalidateVisual();
        }

        protected override void OnRender(DrawingContext dc)
        {
            var p = location;
            p.Offset(-offset.X, -offset.Y);
            dc.DrawRectangle(vbrush, null, new Rect(p, this.RenderSize));
        }
        private Brush vbrush;
        private Point location;
        private Point offset;
    }
}