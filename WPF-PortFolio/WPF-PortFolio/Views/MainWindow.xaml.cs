namespace Views.WPF_PortFolio
{
    public partial class MainWindow 
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void CloseWindow_Exec(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            this.Close();
        }
    }
}
