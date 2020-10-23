using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using WPF_PortFolio.Utils;

namespace WPF_PortFolio.ViewModels
{
    class MainListViewModel : BaseViewModel
    {
        public CancellationTokenSource CancelSource { get; set; } 
            = new CancellationTokenSource();

        FakeRepository _repo;
        public ObservableCollection<Contest> ContestList { get; set; }
        public ObservableCollection<Contest> SearchResult { get; set; }
        private bool _LoadingAnimation;

        private bool _IsOpen;
        public bool IsOpen
        {
            get { return _IsOpen; }
            set 
            { 
                _IsOpen = value;
                OnPropertyChange();
            }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set 
            { 
                searchText = value;
                OnPropertyChange();
            }
        }

        public bool LoadingAnimation
        {
            get { return _LoadingAnimation; }
            set 
            { 
                _LoadingAnimation = value;
                OnPropertyChange();
            }
        }


        public ICommand FilterCommand { get; set; }
        public ICommand ChangeSearchTBCommand { get; set; }
        public ICommand ScrollMoveCommand { get; set; }

        public MainListViewModel(FakeRepository repo)
        {
            this._repo= repo;
            InitializeData();
            InitializeCommand();
        }

        private void InitializeData()
        {
            LoadingAnimation = true;
            ContestList = new ObservableCollection<Contest>();
            SearchResult = new ObservableCollection<Contest>();

            App.Current.Dispatcher.BeginInvoke((Action)(async () =>
            {
                var ContestResult = await _repo.GetAllContest(this.CancelSource.Token);
                if (ContestResult != null)
                    foreach ( var contest in ContestResult )
                    {
                        ContestList.Add(contest);
                        SearchResult.Add(contest);
                    }
                LoadingAnimation = false;
            }));

        }


        private void InitializeCommand()
        {
            FilterCommand = new RelayCommand(filter);
            ScrollMoveCommand = new RelayCommand((listView) =>
            {
                var lv = (listView as ListView);
                if (lv == null)
                    return;
                if (lv.SelectedItem == null)
                    return;
                lv.ScrollIntoView(lv.SelectedItem);
            });

            ChangeSearchTBCommand = new RelayCommand((item) =>{
                if (item is Contest contest)
                {
                    SearchText =  contest.name;
                }
            });
        }

        private void filter(object arg)
        {
            var lv = CollectionViewSource.GetDefaultView(SearchResult);
            if (lv == null)
                return;
            lv.Filter = null;
            App.Current.Dispatcher.BeginInvoke((Action)(() => {
                lv.Filter = (obj) =>
                {
                    if (string.IsNullOrEmpty(searchText))
                        return true;

                    if (obj is Contest contest)
                    {
                        return contest.name.ToLower().Contains(SearchText.ToLower());
                    }
                    return false;
                };

                var cnt = (lv as ListCollectionView)?.Count;
                IsOpen = !(cnt == null || cnt == 0);
            }));

        }
    }
}
