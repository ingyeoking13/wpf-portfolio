using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
using WPF_PortFolio.Utils;

namespace WPF_PortFolio.ViewModels
{
    public class ProfileViewModel  : BaseViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        public ICommand OpenLink { get; }

        FakeRepository repo;
        public ProfileViewModel(FakeRepository repo)
        {
            this.repo = repo;
            InitailizeData();
            OpenLink = new RelayCommand((arg) =>
            {
                if (string.IsNullOrEmpty((string)arg))
                    return;
                Process.Start((string)arg);
            });
        }

        private void InitailizeData()
        {
            Users = new ObservableCollection<User>();
            foreach (var u in repo.GetAllUser())
                Users.Add(u);
        }
    }
}
