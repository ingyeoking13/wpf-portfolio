using System;
using System.Collections.ObjectModel;
using WPF_PortFolio.Utils;

namespace WPF_PortFolio.ViewModels
{
    public class AdminViewModel  : BaseViewModel
    {
        public ObservableCollection<User> Users { get; set; }
        FakeRepository repo;
        public AdminViewModel(FakeRepository repo)
        {
            this.repo = repo;
            InitailizeData();
        }

        private void InitailizeData()
        {
            Users = new ObservableCollection<User>();
            foreach (var u in repo.GetAllUser())
                Users.Add(u);
        }
    }
}
