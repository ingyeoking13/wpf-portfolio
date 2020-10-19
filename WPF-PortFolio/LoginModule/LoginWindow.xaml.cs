using Microsoft.Win32;
using System.Threading.Tasks;
using System.Windows;

namespace LoginModule
{
    public partial class LoginWindow
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
            LogInButton.Click += (s, e) =>
            {
                CheckLogin();
                LoginResult = true;
                this.Close();
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
