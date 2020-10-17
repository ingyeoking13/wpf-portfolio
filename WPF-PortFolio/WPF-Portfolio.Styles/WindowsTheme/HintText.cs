using System.Windows;

namespace WPF_Portfolio.Styles.WindowsTheme
{
    class HintText
    {
        public static string GetHint(DependencyObject obj)
        {
            return (string)obj.GetValue(HintProperty);
        }
        public static void SetHint(DependencyObject obj, string value)
        {
            obj.SetValue(HintProperty, value);
        }
        public static readonly DependencyProperty HintProperty =
            DependencyProperty.RegisterAttached("Hint", typeof(string), typeof(HintText), new PropertyMetadata(string.Empty));
    }
}
