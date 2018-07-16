using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Unsflash.Controls;
using Unsflash.Model;
using Unsflash.ViewModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
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
    public sealed partial class Me : Page
    {
        UserAuthorization UserAuth;
        PublicAuthorization publicAuthorization;
        public static MeRootObjects meRootObjects = new MeRootObjects();
        public static ObservableCollection<LikedModelRootObjects> likedRootObjects = new ObservableCollection<LikedModelRootObjects>();
        public static ObservableCollection<CollectionRootObject> meCollectionRootObjects = new ObservableCollection<CollectionRootObject>();
        public static ObservableCollection<GetaCollectionRootObject> CollectionMe = new ObservableCollection<GetaCollectionRootObject>();
        public string access_token = UsingGlobal.meRoot.access_token;
        public string token_type = UsingGlobal.meRoot.token_type;
        public string refresh_token = UsingGlobal.meRoot.refresh_token;
        public string scope = UsingGlobal.meRoot.scope;
        public int created_at = UsingGlobal.meRoot.created_at;

        public static string TokenInFileUserDefault;

        public Me()
        {
            this.InitializeComponent();
        }

        public LikedViewModel LikedPhotoModel { get; set; }
        public CollectionsViewModel ViewModel { get; set; }
        public aCollectionViewModel CollectionView { get; set; }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            publicAuthorization = new PublicAuthorization();

            UserAuth = new UserAuthorization();

            try
            {
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("UserDefault.txt");
                TokenInFileUserDefault = await FileIO.ReadTextAsync(file);
            }
            catch (Exception)
            {
                var file = await ApplicationData.Current.LocalFolder.CreateFileAsync("UserDefault.txt");
            }

            if (access_token == null && TokenInFileUserDefault == "")
            {
                Logining.Visibility = Visibility.Visible;
                Logined.Visibility = Visibility.Collapsed;
            }
            else
            {
                if(TokenInFileUserDefault != "")
                {
                    access_token = TokenInFileUserDefault;
                }
                var file = await ApplicationData.Current.LocalFolder.GetFileAsync("UserDefault.txt");
                await FileIO.WriteTextAsync(file, access_token);
                if (meRootObjects.id == null)
                {
                    RequestParameters.AuthorizationUri += access_token;
                    meRootObjects = await publicAuthorization.GetInfoUserMe();
                    //RequestParameters.AuthorizationUri = "https://api.unsplash.com/me?access_token=";

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.UriSource = new Uri(meRootObjects.profile_image.large);
                    imgMe.ImageSource = bitmapImage;

                    tblMe.Text = meRootObjects.name;
                    tblCenter.Text = "Download free, beautiful high-quality photos curated by " + meRootObjects.first_name + " .";
                    if (meRootObjects.bio == null) tblBio.Text = "";
                    else tblBio.Text = meRootObjects.bio;
                    if (meRootObjects.location == null) tblLocation.Text = "";
                    else tblLocation.Text = meRootObjects.location;
                    tblUser.Text = meRootObjects.username;
                }
                else
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.UriSource = new Uri(meRootObjects.profile_image.large);
                    imgMe.ImageSource = bitmapImage;

                    tblMe.Text = meRootObjects.name;
                    tblCenter.Text = "Download free, beautiful high-quality photos curated by " + meRootObjects.first_name + " .";
                    if (meRootObjects.bio == null) tblBio.Text = "";
                    else tblBio.Text = meRootObjects.bio;
                    if (meRootObjects.location == null) tblLocation.Text = "";
                    else tblLocation.Text = meRootObjects.location;
                    tblUser.Text = meRootObjects.username;
                }

                Logined.Visibility = Visibility.Visible;
                Logining.Visibility = Visibility.Collapsed;
            }
        }

        private async void btLogin_Click(object sender, RoutedEventArgs e)
        {
            Logining.Visibility = Visibility.Collapsed;
            UserAuth.Authorization();
            await Task.Delay(2000);
            griNewLoading.Visibility = Visibility.Visible;
            await Task.Delay(30000);
            this.Frame.Navigate(typeof(View.Me));
            griNewLoading.Visibility = Visibility.Collapsed;
        }

        public void LoadFail()
        {
            Logining.Visibility = Visibility.Collapsed;
            Noreult.Visibility = Visibility.Visible;
        }

        private async void pvMePage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(pvMePage.SelectedIndex == 1)
            {
                if(access_token != null)
                {
                    RequestParameters.LikedUser = RequestParameters.LikedUser + meRootObjects.username + "/likes?access_token=" + access_token;
                    try
                    {
                        likedRootObjects = await publicAuthorization.GetLiked();
                    }
                    catch (Exception ex)
                    {
                        LoadFail();
                    }

                    RequestParameters.LikedUser = "https://api.unsplash.com/users/";

                    while (likedRootObjects.Count == 0)
                    {
                        await Task.Delay(10);
                        likedRootObjects = await publicAuthorization.GetLiked();
                    }

                    this.LikedPhotoModel = new LikedViewModel();

                    double totalWidth = 0;
                    int start = 0;

                    while (grvLiked.ActualWidth == 0)
                    {
                        await Task.Delay(10);
                    }

                    for (int i = 0; i < LikedPhotoModel.LikedPhotos.Count; i++)
                    {
                        var width = LikedPhotoModel.LikedPhotos[i].width * 310 / LikedPhotoModel.LikedPhotos[i].height;
                        totalWidth += width;

                        if (totalWidth > grvLiked.ActualWidth)
                        {
                            for (int j = start; j < i; j++)
                            {
                                LikedPhotoModel.LikedPhotos[j].Scale = grvLiked.ActualWidth / (totalWidth - width);
                            }
                            start = i;
                            totalWidth = width;
                        }
                    }

                    for (int j = start; j < LikedPhotoModel.LikedPhotos.Count; j++)
                    {
                        LikedPhotoModel.LikedPhotos[j].Scale = grvLiked.ActualWidth / (totalWidth);
                    }

                    grvLiked.ItemsSource = LikedPhotoModel.LikedPhotos;
                }
                else
                {
                    LoginingLike.Visibility = Visibility.Visible;
                }
            }
            if(pvMePage.SelectedIndex == 2)
            {
                if(access_token != null)
                {
                    if(CollectionsViewModel.listMeCollection.Count == 0)
                    {
                        RequestParameters.MeCollection = RequestParameters.MeCollection + meRootObjects.username + "/collections?access_token=" + access_token;
                        try
                        {
                            CollectionsViewModel.listMeCollection = await publicAuthorization.GetMeCollection();
                        }
                        catch (Exception)
                        {
                            LoginingCollection.Visibility = Visibility.Visible;
                            tbloginCollection.Text = "NO RESULTS";
                        }


                        while (CollectionsViewModel.listMeCollection.Count == 0)
                        {
                            await Task.Delay(10);
                            CollectionsViewModel.listMeCollection = await publicAuthorization.GetCurated();
                        }

                        this.ViewModel = new CollectionsViewModel();

                        grvCollectionMe.ItemsSource = CollectionsViewModel.listMeCollection;
                    }
                    else
                    {
                        grvCollectionMe.ItemsSource = CollectionsViewModel.listMeCollection;
                    }
                    grvCol.Visibility = Visibility.Collapsed;
                    grvCollectionMe.Visibility = Visibility.Visible;
                }
                else
                {
                    LoginingCollection.Visibility = Visibility.Visible;
                }
            }
            if(pvMePage.SelectedIndex == 0)
            {
                
            }
        }

        private void Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Grid testGrid = sender as Grid;
            Grid gridTop = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "gridTop");
            Grid griBottom = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "griBottom");
            gridTop.Visibility = Visibility.Visible;
            griBottom.Visibility = Visibility.Visible;
        }

        private void Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Grid testGrid = sender as Grid;
            Grid gridTop = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "gridTop");
            Grid griBottom = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "griBottom");
            griBottom.Visibility = Visibility.Collapsed;
            gridTop.Visibility = Visibility.Collapsed;
        }

        private async void btLoginLike_Click(object sender, RoutedEventArgs e)
        {
            //LoginingLike.Visibility = Visibility.Collapsed;
            //UserAuth.Authorization();
            //await Task.Delay(2000);
            //griNewLoadingLike.Visibility = Visibility.Visible;
            //await Task.Delay(35000);
            //this.Frame.Navigate(typeof(View.Me));
            //pvMePage.SelectedIndex = 1;
            //griNewLoadingLike.Visibility = Visibility.Collapsed;
        }

        private async void grvCollectionMe_ItemClick(object sender, ItemClickEventArgs e)
        {
            await Task.Delay(300);

            grvCol.Visibility = Visibility.Visible;
            grvCollectionMe.Visibility = Visibility.Collapsed;

            CollectionRootObject item = (CollectionRootObject)e.ClickedItem;

            double totalWidth = 0;
            int start = 0;

            RequestParameters.feCollectionIDUri = RequestParameters.feCollectionIDUri + item.id + "/photos?client_id=" + RequestParameters.client_id + "&page=1&per_page=30";

            this.CollectionView = new aCollectionViewModel();

            try
            {
                CollectionMe = await publicAuthorization.GetaCollectionaaa();
            }
            catch (Exception ex)
            {
                MessageDialog ms = new MessageDialog(ex.ToString());
                ms.ShowAsync();
            }

            while (grvCol.ActualWidth == 0)
            {
                await Task.Delay(10);
            }

            for (int i = 0; i < CollectionView.CollectionPhotoMe.Count; i++)
            {
                var width = CollectionView.CollectionPhotoMe[i].width * 310 / CollectionView.CollectionPhotoMe[i].height;
                totalWidth += width;

                if (totalWidth > grvCol.ActualWidth)
                {
                    for (int j = start; j < i; j++)
                    {
                        CollectionView.CollectionPhotoMe[j].Scale = grvCol.ActualWidth / (totalWidth - width);
                    }
                    start = i;
                    totalWidth = width;
                }
            }

            for (int j = start; j < CollectionView.CollectionPhotoMe.Count; j++)
            {
                CollectionView.CollectionPhotoMe[j].Scale = grvCol.ActualWidth / (totalWidth);
            }

            grvCol.ItemsSource = CollectionView.CollectionPhotoMe;
            RequestParameters.feCollectionIDUri = "https://api.unsplash.com/collections/";
        }
    }
}
