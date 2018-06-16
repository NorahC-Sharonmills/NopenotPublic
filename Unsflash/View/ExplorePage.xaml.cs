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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
        public static ObservableCollection<SearchPhotoObjects> listPhotoSearch = new ObservableCollection<SearchPhotoObjects>();
        public ExplorePage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            publicAuthorization = new PublicAuthorization();

            //ViewModel.RequestParameters.photoSearchUri += Autosg.Text;
            int a = 3;
            //RequestParameters.collectionSearchUri += "computer";
            a = 3;
            listPhotoSearch = await publicAuthorization.SearchPhotoaaa();

            //while (listPhotoSearch.Count == 0)
            //{
            //    await Task.Delay(10);
            //    listPhotoSearch = await publicAuthorization.SearchPhotoaaa();
            //}
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
    }
}
