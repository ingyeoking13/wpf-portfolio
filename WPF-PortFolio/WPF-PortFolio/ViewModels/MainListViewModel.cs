using System;
using System.Collections.ObjectModel;
using WPF_PortFolio.Utils;

namespace WPF_PortFolio.ViewModels
{
    class MainListViewModel : BaseViewModel
    {
        FakeRepository _repo;
        public ObservableCollection<Contest> ContestList { get; set; }
        private bool _LoadingAnimation;

        public bool LoadingAnimation
        {
            get { return _LoadingAnimation; }
            set 
            { 
                _LoadingAnimation = value;
                OnPropertyChange();
            }
        }

        public MainListViewModel(FakeRepository repo)
        {
            this._repo= repo;
            InitializeData();
        }

        private void InitializeData()
        {
            LoadingAnimation = true;
            ContestList = new ObservableCollection<Contest>();
            App.Current.Dispatcher.BeginInvoke((Action)(async () =>
            {
                var ContestResult = await _repo.GetAllContest();
                foreach( var contest in ContestResult )
                {
                    ContestList.Add(contest);
                }
                LoadingAnimation = false;
            }));
        }
    }
}
