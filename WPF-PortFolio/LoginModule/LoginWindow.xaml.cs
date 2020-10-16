using Microsoft.Win32;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using System.Windows;

namespace LoginModule
{
    public partial class LoginWindow : Window
    {
        private readonly string registryPath = "Software\\aworks\\Studio";
        private string USERID = "";
        private string PASSWD = "";
        private string DUMMY = ""; 

        public LoginWindow()
        {
            InitializeComponent();
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

            return await CheckLoginAsync();
        }

        private async Task<bool> CheckLoginAsync()
        {
            
        }
    }
}
