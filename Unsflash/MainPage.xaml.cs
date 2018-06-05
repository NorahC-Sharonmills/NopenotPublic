using Microsoft.Toolkit.Uwp.UI.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Unsflash.Model;
using Unsflash.View;
using Unsflash.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Unsflash
{ 
    public  partial class MainPage : Page
    {
        private ObservableCollection<NavLink> _navLinks = new ObservableCollection<NavLink>()
        {
            new NavLink() { Label = "Home", Symbol = Symbol.Home, PageName = typeof(HomePage)},
            new NavLink() { Label = "Collections", Symbol = Symbol.Library, PageName = typeof(CollectionsPage) },
            new NavLink() { Label = "Explore", Symbol = Symbol.View, PageName = typeof(ExplorePage) },
            //new NavLink() { Label = "Search", Symbol = Symbol.Find, PageName = typeof(SearchPage)},

        };

        public ObservableCollection<NavLink> NavLinks
        {
            get { return _navLinks; }
        }

        public MainPage()
        {
            this.InitializeComponent();

            var currentView = SystemNavigationManager.GetForCurrentView();
            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            currentView.BackRequested += CurrentView_BackRequested;
            currentView.BackRequested += backButton_Tapped;
        }

        private void backButton_Tapped(object sender, BackRequestedEventArgs e)

        {
            if (MainFrame.CanGoBack) MainFrame.GoBack();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            var currentView = SystemNavigationManager.GetForCurrentView();

            currentView.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;

            currentView.BackRequested -= backButton_Tapped;
        }

        private void CurrentView_BackRequested(object sender, BackRequestedEventArgs e)
        {
            //if (this.Frame.CanGoBack) this.Frame.GoBack();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            MainFrame.Navigate(typeof(HomePage));
        }

        private void NavLinksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            NavLink item = (NavLink)e.ClickedItem;
            MainFrame.Navigate(item.PageName);
            tblTitle.Text = item.Label;
            NavigationPane.IsPaneOpen = false;
        }
    }
}
