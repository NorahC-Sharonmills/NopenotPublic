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
using Windows.Storage;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Unsflash
{ 
    public  partial class MainPage : Page
    {
        private ObservableCollection<NavLink> _navLinks = new ObservableCollection<NavLink>()
        {
            new NavLink() { Label = "Search", Symbol = Symbol.Find, PageName = typeof(ExplorePage)},
            new NavLink() { Label = "Home", Symbol = Symbol.Home, PageName = typeof(HomePage)},
            new NavLink() { Label = "Collections", Symbol = Symbol.Library, PageName = typeof(CollectionsPage) },       
        };
        private ObservableCollection<NavLink> __navLinks = new ObservableCollection<NavLink>()
        {
            new NavLink() { Label = "Setting", Symbol = Symbol.Setting, PageName = typeof(MoreSeting) },
            new NavLink() { Label = "Me", Symbol = Symbol.Contact, PageName = typeof(Unsflash.View.Me)},

        };

        public ObservableCollection<NavLink> NavLinks__
        {
            get { return __navLinks; }
        }

        public ObservableCollection<NavLink> NavLinks
        {
            get { return _navLinks; }
        }

        public static int GetLinkwithInt = 1;

        public MainPage()
        {
            this.InitializeComponent();

            tblTitle.Text = "Home";

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

        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("UserDefault.txt");
            }
            catch (Exception)
            {
                try
                {
                    var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("UserDefault.txt");
                }
                catch (Exception)
                {

                }
            }
            var getfile = await ApplicationData.Current.LocalFolder.GetFileAsync("UserDefault.txt");
            Me.TokenInFileUserDefault = await FileIO.ReadTextAsync(getfile);

            try
            {
                var fileSaveTrendColor = await ApplicationData.Current.LocalFolder.GetFileAsync("UserDefaultTrendColor.txt");
            }
            catch (Exception)
            {
                try
                {
                    var fileSaveTrendColor = await ApplicationData.Current.LocalFolder.CreateFileAsync("UserDefaultTrendColor.txt");
                }
                catch (Exception)
                {

                }
            }
            var getfileSaveTrendColor = await ApplicationData.Current.LocalFolder.GetFileAsync("UserDefaultTrendColor.txt");
            MoreSeting.GetColorTrendSave = await FileIO.ReadTextAsync(getfileSaveTrendColor);

            try
            {
                var fileSaveBool = await ApplicationData.Current.LocalFolder.GetFileAsync("UserDefaultBool.txt");
            }
            catch (Exception)
            {
                try
                {
                    var fileSaveBool = await ApplicationData.Current.LocalFolder.CreateFileAsync("UserDefaultBool.txt");
                }
                catch (Exception)
                {

                }
            }
            var getfileSaveBool = await ApplicationData.Current.LocalFolder.GetFileAsync("UserDefaultBool.txt");
            MoreSeting.GetBoolSave = await FileIO.ReadTextAsync(getfileSaveBool);

            BitmapImage bitmapImage = new BitmapImage();
            if(MoreSeting.GetBoolSave == "1" && MoreSeting.GetColorTrendSave != "")
            {
                bitmapImage.UriSource = new Uri("https://source.unsplash.com/1920x1080/?" + MoreSeting.GetColorTrendSave, UriKind.RelativeOrAbsolute);
            }
            else
            {
                bitmapImage.UriSource = new Uri("https://source.unsplash.com/random/1920×1080", UriKind.RelativeOrAbsolute);
            }
            imgBack.ImageSource = bitmapImage;

            MainFrame.Navigate(typeof(HomePage));
        }

        private void NavLinksList_ItemClick(object sender, ItemClickEventArgs e)
        {
            NavLink item = (NavLink)e.ClickedItem;
            MainFrame.Navigate(item.PageName);
            tblTitle.Text = item.Label;
            NavigationPane.IsPaneOpen = false;
        }
        public void freshMe()
        {
            MainFrame.Navigate(typeof(Unsflash.View.Me));
            tblTitle.Text = "Me";
        }
    }
}
