﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using Views.WPF_PortFolio;
using WPF_PortFolio.Utils;
using WPF_PortFolio.Views;

namespace WPF_PortFolio.ViewModels
{
    public class NavigationItem 
    {
        private string _ImageURL;
        public string ImageURL
        {
            get { return _ImageURL; }
            set { _ImageURL = value; }
        }

        private string _header;
        public string Header
        {
            get { return _header; }
            set { _header = value; }
        }

        private ICommand _command;
        public ICommand Command
        {
            get { return _command; }
            set { _command = value; }
        }


        public NavigationItem(string imageURL, string Header, ICommand command)
        {
            this.ImageURL = imageURL;
            this.Header = Header;
            this.Command = command;
        }
    }

    public class MainViewModel 
    {
        public ObservableCollection<NavigationItem> NavigationItemSource { get; set; }
        private MainWindow view; 
        public ICommand NavigationCommand { get; set; }

        public MainViewModel(MainWindow _mainWindow)
        {
            this.view = _mainWindow;
            InitializeNavigation();
        }

        private void InitializeNavigation()
        {
            NavigationCommand = new RelayCommand(
                (arg) => {
                    if (arg is NavigationItem nav)
                        nav.Command.Execute(null);
                }
            );

            NavigationItemSource = new ObservableCollection<NavigationItem>();

            // 홈 페이지 -- 대시보드 UI
            NavigationItemSource.Add(
                new NavigationItem(
                    "pack://application:,,,/WPF-PortFolio;Component/Resources/ic_Home.png",
                    "HOME",  
                    new RelayCommand(GoHomePage)));

            // 검색 페이지 -- 리스트 뷰 
            NavigationItemSource.Add(
                new NavigationItem(
                    "pack://application:,,,/WPF-PortFolio;Component/Resources/ic_Search.png",
                    "SEARCH", 
                    new RelayCommand(GoListPage)));

            // 관리 페이지 -- 사용자 프로필 리스트, 디테일 페이지 
            NavigationItemSource.Add(
                new NavigationItem(
                    "pack://application:,,,/WPF-PortFolio;Component/Resources/ic_Profile.png",
                    "PROFILE", 
                    new RelayCommand(GoAdminPage)));

            // Home 에서 시작
            NavigationCommand.Execute(NavigationItemSource[0]);
        }

        private void GoHomePage(object arg)
        {
            var instance = FakeRepository.Instance;
            var contentWindow = new HomeView();
            contentWindow.DataContext = new HomeViewModel(instance);
            view.ContentArea.Content = contentWindow;
        }
        
        private void GoListPage(object arg)
        { 
            var contentWindow = new MainListView();
            contentWindow.DataContext = new MainListViewModel(FakeRepository.Instance);
            view.ContentArea.Content = contentWindow;
        }

        private void GoAdminPage(object arg)
        {
            var contentWindow = new ProfileView();
            contentWindow.DataContext = new ProfileViewModel(FakeRepository.Instance);
            view.ContentArea.Content = contentWindow;
        }
    }
}
