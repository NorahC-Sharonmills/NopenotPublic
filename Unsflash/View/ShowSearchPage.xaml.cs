using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Unsflash.Controls;
using Unsflash.Model;
using Unsflash.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class showSearchPage : Page
    {
        PublicAuthorization publicAuthorization;
        public static SearchPhotoObjects listPhotoSearch = new SearchPhotoObjects();
        public static ObservableCollection<ResultModel> MySearchRes = new ObservableCollection<ResultModel>();

        public showSearchPage()
        {
            this.InitializeComponent();
        }

        public SearchViewModel ListPhotoSearchModel { get; set; }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            //AutoSuggestBox item = (AutoSuggestBox)e.Parameter;
            try
            {
                AutoSuggestBox AutosgBoxitem = (AutoSuggestBox)e.Parameter;

                ViewModel.RequestParameters.photoSearchUri += AutosgBoxitem.Text;

                //RequestParameters.collectionSearchUri += AutosgBoxitem.Text;

                tbNameSearch.Text = AutosgBoxitem.Text;
            }
            catch (Exception)
            {
                TrendSourch Trenditem = (TrendSourch)e.Parameter;

                ViewModel.RequestParameters.photoSearchUri += Trenditem.Title;

                //RequestParameters.collectionSearchUri += AutosgBoxitem.Text;

                tbNameSearch.Text = Trenditem.Title;

            }
            

            publicAuthorization = new PublicAuthorization();

            try
            {
                listPhotoSearch = await publicAuthorization.SearchPhotoaaa();
            }
            catch (Exception ex)
            {
                Noreult.Visibility = Visibility.Visible;
            }

            if(listPhotoSearch.results != null)
            {
                if (listPhotoSearch.results.Count == 0)
                {
                    Noreult.Visibility = Visibility.Visible;
                }
                else
                {
                    while (listPhotoSearch.total == 0)
                    {
                        await Task.Delay(10);
                        listPhotoSearch = await publicAuthorization.SearchPhotoaaa();
                    }

                    for (int i = 0; i < 30; i++)
                    {
                        MySearchRes.Add(new ResultModel
                        {
                            id = listPhotoSearch.results[i].id,
                            created_at = listPhotoSearch.results[i].created_at,
                            updated_at = listPhotoSearch.results[i].updated_at,
                            width = listPhotoSearch.results[i].width,
                            height = listPhotoSearch.results[i].height,
                            color = listPhotoSearch.results[i].color,
                            description = listPhotoSearch.results[i].description,
                            urlsfull = listPhotoSearch.results[i].urls.full,
                            urlsmedium = listPhotoSearch.results[i].urls.small,
                            links = listPhotoSearch.results[i].links.download,
                            categories = listPhotoSearch.results[i].categories,
                            sponsored = listPhotoSearch.results[i].sponsored,
                            likes = listPhotoSearch.results[i].likes,
                            liked_by_user = listPhotoSearch.results[i].liked_by_user,
                            current_user_collections = listPhotoSearch.results[i].current_user_collections,
                            slug = listPhotoSearch.results[i].slug,
                            username = listPhotoSearch.results[i].user.username,
                            name = listPhotoSearch.results[i].user.name,
                            ImgmediumPro5 = listPhotoSearch.results[i].user.profile_image.medium,
                            likebyuser = listPhotoSearch.results[i].urls.raw
                            
                        });
                    }

                }
            }
            else
            {
                Noreult.Visibility = Visibility.Visible;
            }


            this.ListPhotoSearchModel = new SearchViewModel();

            double totalWidth = 0;
            int start = 0;

                while (grvSearch.ActualWidth == 0)
                {
                    await Task.Delay(10);
                }

                for (int i = 0; i < ListPhotoSearchModel.ListImageRes.Count; i++)
                {
                    var width = ListPhotoSearchModel.ListImageRes[i].width * 310 / ListPhotoSearchModel.ListImageRes[i].height;
                    totalWidth += width;

                    if (totalWidth > grvSearch.ActualWidth)
                    {
                        for (int j = start; j < i; j++)
                        {
                        ListPhotoSearchModel.ListImageRes[j].Scale = grvSearch.ActualWidth / (totalWidth - width);
                        }
                        start = i;
                        totalWidth = width;
                    }
                }

                for (int j = start; j < ListPhotoSearchModel.ListImageRes.Count; j++)
                {
                    ListPhotoSearchModel.ListImageRes[j].Scale = grvSearch.ActualWidth / (totalWidth);
                }

            grvSearch.ItemsSource = ListPhotoSearchModel.ListImageRes;

            ViewModel.RequestParameters.photoSearchUri = "https://api.unsplash.com/search/photos/?client_id=" + RequestParameters.client_id + "&per_page=30&query=";

            RequestParameters.collectionSearchUri = "https://api.unsplash.com/search/collections/?client_id=" + RequestParameters.client_id + "&per_page=30&query=";
        }

        private void grvSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            ResultModel item = (ResultModel)e.ClickedItem;

            Frame.Navigate(typeof(ViewPhotoSearch), item);
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (UsingGlobal.meRoot.access_token == null && Me.TokenInFileUserDefault == "")
            {
                Grid testGrid = sender as Grid;
                Grid griBottom = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "griBottom");
                griBottom.Visibility = Visibility.Visible;
            }
            else
            {
                Grid testGrid = sender as Grid;
                Grid gridTop = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "gridTop");
                Grid griBottom = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "griBottom");
                gridTop.Visibility = Visibility.Visible;
                griBottom.Visibility = Visibility.Visible;
            }
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (UsingGlobal.meRoot.access_token == null && Me.TokenInFileUserDefault == "")
            {
                Grid testGrid = sender as Grid;
                Grid griBottom = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "griBottom");
                griBottom.Visibility = Visibility.Collapsed;
            }
            else
            {
                Grid testGrid = sender as Grid;
                Grid gridTop = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "gridTop");
                Grid griBottom = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "griBottom");
                gridTop.Visibility = Visibility.Collapsed;
                griBottom.Visibility = Visibility.Collapsed;
            }
        }

        private void btdownHome_Click(object sender, RoutedEventArgs e)
        {

        }

        private void grvSearch_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
