using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using WPF_PortFolio.Utils;

namespace WPF_PortFolio.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        FakeRepository repo { get; set; }
        public ObservableCollection<Portfolio> Portfolios { get; set; }
        public ObservableCollection<User> Users { get; set; }

        public ICommand onClickURL { get; set; }

        public HomeViewModel(FakeRepository repo)
        {
            this.repo = repo;
            IntializeData();
            InitializeCommand();
        }

        private void IntializeData()
        {
            Portfolios = new ObservableCollection<Portfolio>();
            foreach(Portfolio p in repo.GetAllPortfolios())
                Portfolios.Add(p);

            Users = new ObservableCollection<User>();
            foreach (User u in repo.GetAllUser())
                Users.Add(u);
        }

        private void InitializeCommand()
        {
            onClickURL = new RelayCommand((arg) =>
            {
                var url = (string)arg;
                if (string.IsNullOrEmpty(url)) 
                    return;
                Process.Start(url);
            });
        }
    }
}
