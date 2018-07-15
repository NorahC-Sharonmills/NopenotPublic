using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Unsflash.Model;
using Unsflash.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Unsflash.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ExplorePage : Page
    {
        PublicAuthorization publicAuthorization;
        public static SearchPhotoObjects listPhotoSearch = new SearchPhotoObjects();
        public ExplorePage()
        {
            this.InitializeComponent();

            trendz = TrendSourchManager.GetTrends();
            showSearchPage.MySearchRes.Clear();
        }

        private List<TrendSourch> trendz;

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            showSearchPage.MySearchRes.Clear();
        }

        private async void btbusiness_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RequestParameters.photoSearchUri += "business";
            listPhotoSearch = await publicAuthorization.SearchPhotoaaa();
        }

        private async void btcomputer_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RequestParameters.photoSearchUri += "computer";
            listPhotoSearch = await publicAuthorization.SearchPhotoaaa();
        }

        private async void btnature_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RequestParameters.photoSearchUri += "nature";
            listPhotoSearch = await publicAuthorization.SearchPhotoaaa();
        }

        private async void btlove_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RequestParameters.photoSearchUri += "love";
            listPhotoSearch = await publicAuthorization.SearchPhotoaaa();
        }

        private async void bthouse_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RequestParameters.photoSearchUri += "house";
            listPhotoSearch = await publicAuthorization.SearchPhotoaaa();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Uri uri = new Uri("https://source.unsplash.com/random/1920×1080", UriKind.RelativeOrAbsolute);
            BitmapImage bmg = new BitmapImage(uri);
            //bgImg.ImageSource = bmg;
            griNewLoading.Visibility = Visibility.Collapsed;
            griSearch.Visibility = Visibility.Visible;
        }

        private void grvTrend_ItemClick(object sender, ItemClickEventArgs e)
        {
            TrendSourch item = (TrendSourch)e.ClickedItem;
            Frame.Navigate(typeof(showSearchPage), item);
        }

        private async void Autosg_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (Autosg.Text != "")
            {
                Frame.Navigate(typeof(showSearchPage), Autosg);
            }
        }
    }
}
