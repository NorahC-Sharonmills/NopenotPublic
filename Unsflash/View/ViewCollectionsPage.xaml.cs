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
    public sealed partial class ViewCollectionsPage : Page
    {
        CollectionRootObject itemaaa;
        public static ObservableCollection<GetaCollectionRootObject> aCollection = new ObservableCollection<GetaCollectionRootObject>();

        public ViewCollectionsPage()
        {
            this.InitializeComponent();
        }

        public aCollectionViewModel CollectionView { get; set; }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            PublicAuthorization publicAuthorization = new PublicAuthorization();

            itemaaa = (CollectionRootObject)e.Parameter;

            if(itemaaa.curated == false)
            {
                if(itemaaa.total_photos >= 30)
                {
                    ViewModel.RequestParameters.feCollectionIDUri += itemaaa.id;
                    ViewModel.RequestParameters.feCollectionIDUri += "&per_page=30";

                    aCollection = await publicAuthorization.GetaCollectionaaa();
                }
                else
                {
                    Int32 total = itemaaa.total_photos;
                    int page = 1;
                    int per_page = 30;
                    //ViewModel.RequestParameters.feCollectionIDUri += itemaaa.id;
                    //ViewModel.RequestParameters.feCollectionIDUri += "&page=" + page +"&per_page=" + per_page;
                    do
                    {
                        ViewModel.RequestParameters.feCollectionIDUri += itemaaa.id;
                        ViewModel.RequestParameters.feCollectionIDUri += "&page=" + page + "&per_page=" + per_page;
                        aCollection = await publicAuthorization.GetaCollectionaaa();
                        page += 30;
                        per_page += 30;
                        //aCollection = aCollection + await publicAuthorization.GetaCollectionaaa();
                    } while (per_page < total);
                }

            }
            else
            {
                ViewModel.RequestParameters.curCollectionIDUri += itemaaa.id;
                ViewModel.RequestParameters.curCollectionIDUri += "&per_page=10";

                aCollection = await publicAuthorization.GetaCollectionaaa();
            }

            double totalWidth = 0;
            int start = 0;
            double Scale = 1;

            this.CollectionView = new aCollectionViewModel();

            //while (grvViewcollection.ActualWidth == 0)
            //{
            //    await Task.Delay(10);
            //}

            //for (int i = 0; i < CollectionView.aCollectionPhoto.Count; i++)
            //{
            //    var width = CollectionView.aCollectionPhoto[i].cover_photo.width * 310 / CollectionView.aCollectionPhoto[i].cover_photo.height;
            //    totalWidth += width;

            //    if (totalWidth > grvViewcollection.ActualWidth)
            //    {
            //        for (int j = start; j < i; j++)
            //        {
            //            //CollectionView.aCollectionPhoto[j] = grvViewcollection.ActualWidth / (totalWidth - width);
            //        }
            //        start = i;
            //        totalWidth = width;
            //    }
            //}

            //for (int j = start; j < CollectionView.aCollectionPhoto.Count; j++)
            //{
            //    //CollectionView.aCollectionPhoto[j].Scale = grvStart.ActualWidth / (totalWidth);
            //}


            //grvViewcollection.ItemsSource = CollectionView.aCollectionPhoto;
            //grloadacollection.Visibility = Visibility.Collapsed;

            tbNameCollection.Text = itemaaa.title;
        }
    }
}
