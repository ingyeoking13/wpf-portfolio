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
        private void CustomerList_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var lvi = getCurrentListViewItem(e);
                DataObject obj = new DataObject(typeof(ListViewItem), lvi.DataContext);
                
                _dragWindow = new DragWindow();
                var tf = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
                var pos = new Point
                {
                    Y = System.Windows.Forms.Control.MousePosition.Y,
                    X = System.Windows.Forms.Control.MousePosition.X
                };
                pos = tf.Transform(pos);
                _dragWindow.Top = pos.Y;
                _dragWindow.Width = pos.X;
                _dragWindow.Width = lvi.RenderSize.Width;
                _dragWindow.Height = lvi.RenderSize.Height;
                _dragWindow.IsHitTestVisible = true;
                _dragWindow.Background = new VisualBrush(lvi)
                {
                    Opacity = .7
                };
                _dragWindow.Show();

                DragDrop.DoDragDrop(lvi, obj, DragDropEffects.Move);
                _dragWindow.Close();
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
            var Users = (DataContext as HomeViewModel).Users;
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
            var tf = PresentationSource.FromVisual(this).CompositionTarget.TransformFromDevice;
            var pos = new Point
            {
                Y = System.Windows.Forms.Control.MousePosition.Y,
                X = System.Windows.Forms.Control.MousePosition.X
            };
            pos = tf.Transform(pos);
            _dragWindow.Top = pos.Y+5;
            _dragWindow.Left = pos.X+5;
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
}