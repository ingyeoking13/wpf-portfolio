using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows;

namespace LoginModule
{
    public partial class LoginWindow : Window
    {
        private readonly string registryPath = "Software\\aworks\\Studio";
        private string USERID = "";
        private string PASSWD = "";

        public bool LoginResult { get; set; }

        public LoginWindow()
        {
            InitializeComponent();
            InitailizeState();
        }

        private void InitailizeState()
        {
            SignInButton.IsChecked = true;
            SignUpButton.IsChecked = false;

            //Toggle Signin/SignUp
             SignInButton.Click += (s, e) =>
            {
                if (SignUpButton.IsChecked == true)
                    SignUpButton.IsChecked = false;
                else
                    SignInButton.IsChecked = true;
            };

            SignUpButton.Click += (s, e) =>
            {
                if (SignInButton.IsChecked == true)
                    SignInButton.IsChecked = false;
                else
                    SignUpButton.IsChecked = true;
            };

            // Click시 
            CloseButton.Click += (s, e) =>
            {
                LoginResult = false;
                this.Close();
            };

            LogInButton.Click += (s, e) =>
            {
                CheckLogin();
                LoginResult = true;
                this.Close();
            };

            ForgotPassword.PreviewMouseDown += (s, e) =>
            {

            };
        }

        public async Task<bool> CheckLogin()
        {
            var load = Registry.CurrentUser.OpenSubKey(registryPath);
            if (load == null) 
                return false;

            USERID = (string)load.GetValue(nameof(USERID));
            if ( string.IsNullOrEmpty(USERID) )
                return false;

            PASSWD = (string)load.GetValue(nameof(PASSWD));
            if (string.IsNullOrEmpty(PASSWD))
                return false;

            return  LoginResult = await CheckLoginAsync();
        }

        private async Task<bool> CheckLoginAsync()
        {
            return true;
        }
    }
}
