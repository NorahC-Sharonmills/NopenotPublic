using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
    public sealed partial class CollectionsPage : Page
    {
        
        public CollectionsPage()
        {
            this.InitializeComponent();
        }

        public CollectionsViewModel ViewModel { get; set; }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            PublicAuthorization publicAuthorization = new PublicAuthorization();
            CollectionsViewModel.listFeaturedCollection = await publicAuthorization.GetFeatured();

            while(CollectionsViewModel.listFeaturedCollection.Count == 0)
            {
                await Task.Delay(10);
                CollectionsViewModel.listFeaturedCollection = await publicAuthorization.GetFeatured();
            }

            this.ViewModel = new CollectionsViewModel();

            grvFeatured.ItemsSource = CollectionsViewModel.listFeaturedCollection;
            griFeaturedLoading.Visibility = Visibility.Collapsed;
        }

        private async void pvCollections_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(pvCollections.SelectedIndex == 1)
            {
                if(CollectionsViewModel.listCuratedCollection.Count == 0)
                {
                    PublicAuthorization publicAuthorization = new PublicAuthorization();
                    CollectionsViewModel.listCuratedCollection = await publicAuthorization.GetCurated();

                    while (CollectionsViewModel.listCuratedCollection.Count == 0)
                    {
                        await Task.Delay(10);
                        CollectionsViewModel.listCuratedCollection = await publicAuthorization.GetCurated();
                    }

                    this.ViewModel = new CollectionsViewModel();

                    grvCurated.ItemsSource = CollectionsViewModel.listCuratedCollection;
                }
                else
                {
                    grvCurated.ItemsSource = CollectionsViewModel.listCuratedCollection;
                }

                griCuratedLoading.Visibility = Visibility.Collapsed;
            }
        }

        private void grvFeatured_ItemClick(object sender, ItemClickEventArgs e)
        {
            CollectionRootObject itemaaa = (CollectionRootObject)e.ClickedItem;

            Frame.Navigate(typeof(ViewCollectionsPage), itemaaa);
        }

        private void grvCurated_ItemClick(object sender, ItemClickEventArgs e)
        {
            CollectionRootObject itemaaa = (CollectionRootObject)e.ClickedItem;

            Frame.Navigate(typeof(ViewCollectionsPage), itemaaa);
        }
    }
}
