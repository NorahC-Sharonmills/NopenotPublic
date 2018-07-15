using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Unsflash.Model;

using Unsflash.ViewModel;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.System.UserProfile;
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
    public sealed partial class ViewCollectionsPage : Page
    {
        CollectionRootObject itemaaa;
        GetaCollectionRootObject newItem;
        public static ObservableCollection<GetaCollectionRootObject> aCollection = new ObservableCollection<GetaCollectionRootObject>();
        public static ObservableCollection<GetaCollectionRootObject> aaCollection = new ObservableCollection<GetaCollectionRootObject>();

        DownloadOperation downloadOperation;
        CancellationTokenSource cancellationToken;
        Windows.Networking.BackgroundTransfer.BackgroundDownloader backgroundDownloader = new Windows.Networking.BackgroundTransfer.BackgroundDownloader();

        public ViewCollectionsPage()
        {
            this.InitializeComponent();
            RegisterForShare();
        }

        public aCollectionViewModel CollectionView { get; set; }
        //private void onGridViewSizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    // Here I'm calculating the number of columns I want based on
        //    // the width of the page
        //    var columns = Math.Ceiling(ActualWidth / 800);
        //    ((ItemsWrapGrid)grvCol.ItemsPanelRoot).ItemWidth = e.NewSize.Width / columns;
        //    var row = Math.Ceiling(ActualHeight / 480);
        //    ((ItemsWrapGrid)grvCol.ItemsPanelRoot).ItemHeight = e.NewSize.Height / row;
        //}

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            PublicAuthorization publicAuthorization = new PublicAuthorization();

            itemaaa = (CollectionRootObject)e.Parameter;

            int temp = itemaaa.total_photos;

            //ViewModel.RequestParameters.feCollectionIDUri += itemaaa.id ;
            ViewModel.RequestParameters.feCollectionIDUri = RequestParameters.feCollectionIDUri + itemaaa.id + "/photos?client_id=" + RequestParameters.client_id + "&page=1&per_page=30";
            RequestParameters.curCollectionIDUri = RequestParameters.curCollectionIDUri + itemaaa.id + "/photos?client_id=" + RequestParameters.client_id + "&page=1&per_page=10";

            double totalWidth = 0;
            int start = 0;


                if (itemaaa.curated == false)
                {
                    this.CollectionView = new aCollectionViewModel();
                    aCollection = await publicAuthorization.GetaCollectionaaa();

                    while (grvCol.ActualWidth == 0)
                    {
                        await Task.Delay(10);
                    }

                    for (int i = 0; i < CollectionView.aCollectionPhoto.Count; i++)
                    {
                        var width = CollectionView.aCollectionPhoto[i].width * 310 / CollectionView.aCollectionPhoto[i].height;
                        totalWidth += width;

                        if (totalWidth > grvCol.ActualWidth)
                        {
                            for (int j = start; j < i; j++)
                            {
                                CollectionView.aCollectionPhoto[j].Scale = grvCol.ActualWidth / (totalWidth - width);
                            }
                            start = i;
                            totalWidth = width;
                        }
                    }

                    for (int j = start; j < CollectionView.aCollectionPhoto.Count; j++)
                    {
                        CollectionView.aCollectionPhoto[j].Scale = grvCol.ActualWidth / (totalWidth);
                    }

                    grvCol.ItemsSource = CollectionView.aCollectionPhoto;
                    ViewModel.RequestParameters.feCollectionIDUri = "https://api.unsplash.com/collections/" + itemaaa.id + "/photos?client_id=" + RequestParameters.client_id + "&page=1&per_page=30";
                }
                else
                {
                    this.CollectionView = new aCollectionViewModel();
                    aaCollection = await publicAuthorization.GetaCollectionaaaa();

                    while (aaCollection.Count == 0)
                    {
                        await Task.Delay(10);
                    }

                    while (grvCol.ActualWidth == 0)
                    {
                        await Task.Delay(10);
                    }

                    for (int i = 0; i < CollectionView.aaCollectionPhoto.Count; i++)
                    {
                        var width = CollectionView.aaCollectionPhoto[i].width * 310 / CollectionView.aaCollectionPhoto[i].height;
                        totalWidth += width;

                        if (totalWidth > grvCol.ActualWidth)
                        {
                            for (int j = start; j < i; j++)
                            {
                                CollectionView.aaCollectionPhoto[j].Scale = grvCol.ActualWidth / (totalWidth - width);
                            }
                            start = i;
                            totalWidth = width;
                        }
                    }

                    for (int j = start; j < CollectionView.aaCollectionPhoto.Count; j++)
                    {
                        CollectionView.aaCollectionPhoto[j].Scale = grvCol.ActualWidth / (totalWidth);
                    }

                    grvCol.ItemsSource = CollectionView.aaCollectionPhoto;
                    ViewModel.RequestParameters.curCollectionIDUri = "https://api.unsplash.com/collections/curated/" + itemaaa.id + "/photos?client_id=" + RequestParameters.client_id + "&page=1&per_page=30";
                }

            tbNameCollection.Text = itemaaa.title;
        }

        private void grvCol_ItemClick(object sender, ItemClickEventArgs e)
        {
            newItem = (GetaCollectionRootObject)e.ClickedItem;

            Frame.Navigate(typeof(TestControl), newItem);
        }

        private void lvcol_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void btDownload_Click(object sender, RoutedEventArgs e)
        {
            //if (showinfo.Visibility == Visibility.Visible) showinfo.Visibility = Visibility.Collapsed;
            Download();
        }

        public async void Download()
        {

            FolderPicker folderPicker = new FolderPicker();
            folderPicker.SuggestedStartLocation = PickerLocationId.Downloads;
            folderPicker.ViewMode = PickerViewMode.Thumbnail;
            folderPicker.FileTypeFilter.Add("*");
            StorageFolder folder = await folderPicker.PickSingleFolderAsync();
            if (folder != null)
            {
                Uri uridownload;
                StorageFile file;
                if (newItem == null) { uridownload = new Uri(itemaaa.cover_photo.links.download); file = await folder.CreateFileAsync("Unplash-" + itemaaa.cover_photo.user.name + ".jpg", CreationCollisionOption.GenerateUniqueName); }
                else { uridownload = new Uri(newItem.links.download); file = await folder.CreateFileAsync("Unplash-" + newItem.user.name + ".jpg", CreationCollisionOption.GenerateUniqueName); }

                //file = await folder.CreateFileAsync("Unplash-" + rootObject.user.name + ".jpg", CreationCollisionOption.GenerateUniqueName);
                //Uri durl = new Uri(rootObject.links.download);
                downloadOperation = backgroundDownloader.CreateDownload(uridownload, file);

                Progress<DownloadOperation> progress = new Progress<DownloadOperation>(progressChanged);
                cancellationToken = new CancellationTokenSource();

                try
                {
                    //Statustext.Text = "Initial vizing...";
                    //ringLoad.IsActive = true;
                    await downloadOperation.StartAsync().AsTask(cancellationToken.Token, progress);
                }
                catch (TaskCanceledException)
                {

                    downloadOperation.ResultFile.DeleteAsync();
                    downloadOperation = null;
                }
            }
        }
        private void progressChanged(DownloadOperation downloadOperation)
        {
            //sizeLoad.Visibility = Visibility.Visible;
            //ringLoad.IsActive = false;
            int progress = (int)(100 * ((double)downloadOperation.Progress.BytesReceived / (double)downloadOperation.Progress.TotalBytesToReceive));
            //sizeLoad.Text = String.Format("{0}%", progress);
            //Statustext.Text = String.Format("{0} of {1} kb. downloaded - %{2} complete.", downloadOperation.Progress.BytesReceived / 1024, downloadOperation.Progress.TotalBytesToReceive / 1024, progress);
            //Statustext.Value = progress;

            switch (downloadOperation.Progress.Status)
            {
                case BackgroundTransferStatus.Running:
                    {
                        break;
                    }
                case BackgroundTransferStatus.PausedByApplication:
                    {

                        break;
                    }
                case BackgroundTransferStatus.PausedCostedNetwork:
                    {

                        break;
                    }
                case BackgroundTransferStatus.PausedNoNetwork:
                    {

                        break;
                    }
                case BackgroundTransferStatus.Completed:
                    {
                        MessageDialog msg = new MessageDialog("Download Completed");
                        msg.ShowAsync();
                        break;
                    }
                case BackgroundTransferStatus.Error:
                    {
                        //Statustext.Text = "An error occured while downloading.";
                        MessageDialog msg = new MessageDialog("No internet connection has been found.");
                        msg.ShowAsync();
                        break;
                    }
            }
            if (progress >= 100)
            {
                downloadOperation = null;
                //sizeLoad.Visibility = Visibility.Collapsed;
            }
        }

        private async void btsetDeskTop_Click(object sender, RoutedEventArgs e)
        {
            //if (showinfo.Visibility == Visibility.Visible) showinfo.Visibility = Visibility.Collapsed;
            //ringLoad.IsActive = true;
            //await ChangeBackground();
            //ringLoad.IsActive = false;
        }

        private async Task ChangeBackground()
        {
            if (UserProfilePersonalizationSettings.IsSupported())
            {
                Uri urichangewallpaper;
                if (newItem == null) { urichangewallpaper = new Uri(itemaaa.cover_photo.urls.full); }
                else { urichangewallpaper = new Uri(newItem.urls.full); }

                using (Windows.Web.Http.HttpClient client = new Windows.Web.Http.HttpClient())
                {
                    try
                    {
                        Windows.Web.Http.HttpResponseMessage response = await client.GetAsync(urichangewallpaper);
                        if (response != null && response.StatusCode == Windows.Web.Http.HttpStatusCode.Ok)
                        {


                            string filename = "background.jpg";
                            var imageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                            using (IRandomAccessStream stream = await imageFile.OpenAsync(FileAccessMode.ReadWrite))
                            {
                                await response.Content.WriteToStreamAsync(stream);
                            }
                            StorageFile file = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                            UserProfilePersonalizationSettings settings = UserProfilePersonalizationSettings.Current;
                            if (!await settings.TrySetWallpaperImageAsync(file))
                            {
                                System.Diagnostics.Debug.WriteLine("Failed");
                            }
                            else
                            {
                                System.Diagnostics.Debug.WriteLine("Success");
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void btShare_Click(object sender, RoutedEventArgs e)
        {
            //if (showinfo.Visibility == Visibility.Visible) showinfo.Visibility = Visibility.Collapsed;
            
            DataTransferManager.ShowShareUI();
        }

        private void RegisterForShare()
        {
            DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += new TypedEventHandler<DataTransferManager, DataRequestedEventArgs>(this.ShareImageHandler);
        }

        private async void ShareImageHandler(DataTransferManager sender, DataRequestedEventArgs e)
        {
            DataRequest request = e.Request;
            request.Data.Properties.Title = "Share Picture";
            request.Data.Properties.Description = "Share picture more friend.";

            Uri uri;
            if (newItem == null) { uri = new Uri(itemaaa.cover_photo.urls.full); request.Data.SetText("Unplash-" + itemaaa.cover_photo.user.name); /*request.Data.SetWebLink(uri);*/ }
            else { uri = new Uri(newItem.urls.full); request.Data.SetText("Unplash-" + newItem.user.name); /*request.Data.SetWebLink(uri);*/ }

            // Plain text
            //request.Data.SetText("Unplash-" + rootObject.user.name);

            // Link
            /*request.Data.SetWebLink(new Uri(item.urls.full))*/;

            // HTML
            request.Data.SetHtmlFormat("<b>Bold Text</b>");

            IRandomAccessStream stream;
            string filename = "shareimg.jpg";
            //uri = new Uri(item.urls.full);
            using (Windows.Web.Http.HttpClient client = new Windows.Web.Http.HttpClient())
            {
                try
                {
                    Windows.Web.Http.HttpResponseMessage response = await client.GetAsync(uri);
                    if (response != null && response.StatusCode == Windows.Web.Http.HttpStatusCode.Ok)
                    {
                        var imageFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);
                        using (stream = await imageFile.OpenAsync(FileAccessMode.ReadWrite))
                        {
                            await response.Content.WriteToStreamAsync(stream);
                        }
                    }
                }
                catch
                {

                }

                RandomAccessStreamReference imageStreamRef = RandomAccessStreamReference.CreateFromFile(await ApplicationData.Current.LocalFolder.GetFileAsync(filename));
                request.Data.SetBitmap(imageStreamRef);

                DataRequestDeferral deferral = request.GetDeferral();

                // Make sure we always call Complete on the deferral.
                try
                {
                    StorageFile thumbnail = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);
                    request.Data.Properties.Thumbnail = RandomAccessStreamReference.CreateFromFile(thumbnail);
                    StorageFile imageshare = await ApplicationData.Current.LocalFolder.GetFileAsync(filename);

                    // Bitmaps
                    request.Data.SetBitmap(RandomAccessStreamReference.CreateFromFile(imageshare));
                }
                finally
                {
                    deferral.Complete();
                }
            }
        }

        private void btInfo_Click(object sender, RoutedEventArgs e)
        {
            //if (showinfo.Visibility == Visibility.Visible) showinfo.Visibility = Visibility.Collapsed;
            //else showinfo.Visibility = Visibility.Visible;
            

            //if (newItem == null)
            //{
            //    if (itemaaa.user.name == null) nameuser.Text = " ";
            //    else nameuser.Text = " " + itemaaa.user.name;
            //    if (itemaaa.user.bio == null) biouser.Text = " ";
            //    else biouser.Text = " " + itemaaa.user.bio;
            //    if (itemaaa.cover_photo.user.location == null) locationpic.Text = " ";
            //    else locationpic.Text = " " + itemaaa.cover_photo.user.location;
            //    if (itemaaa.cover_photo.created_at.Date.ToString() == null) createTime.Text = " ";
            //    else createTime.Text = itemaaa.cover_photo.created_at.Date.ToString();
            //    if (itemaaa.cover_photo.updated_at.Date.ToString() == null) updateTime.Text = " ";
            //    else updateTime.Text = itemaaa.cover_photo.updated_at.Date.ToString();
                //if (itemaaa.user.make == null) cameramaker.Text = " ";
                //else cameramaker.Text = " " + rootObject.exif.make;
                //if (rootObject.exif.model == null) cameramodel.Text = " ";
                //else cameramodel.Text = " " + rootObject.exif.model;
                //if (rootObject.exif.iso.ToString() == null) cameraiso.Text = " ";
                //else cameraiso.Text = " " + rootObject.exif.iso.ToString();
                //if (rootObject.exif.aperture == null) camerafstop.Text = " ";
                //else camerafstop.Text = " " + rootObject.exif.aperture;
                //if (rootObject.exif.focal_length == null) camerafocalleght.Text = " ";
                //else camerafocalleght.Text = " " + rootObject.exif.focal_length;

                //urichangewallpaper = new Uri(itemaaa.cover_photo.urls.full);
            //}
            //else
            //{
            //    //urichangewallpaper = new Uri(newItem.cover_photo.urls.full);
            //    if (newItem.user.name == null) nameuser.Text = " ";
            //    else nameuser.Text = " " + newItem.user.name;
            //    if (newItem.user.bio == null) biouser.Text = " ";
            //    else biouser.Text = " " + newItem.user.bio;
            //    if (newItem.cover_photo.user.location == null) locationpic.Text = " ";
            //    else locationpic.Text = " " + newItem.cover_photo.user.location;
            //    if (newItem.cover_photo.created_at.Date.ToString() == null) createTime.Text = " ";
            //    else createTime.Text = newItem.cover_photo.created_at.Date.ToString();
            //    if (newItem.cover_photo.updated_at.Date.ToString() == null) updateTime.Text = " ";
            //    else updateTime.Text = newItem.cover_photo.updated_at.Date.ToString();
            //}

            //Infotext.Text = rootObject.views.ToString();
        }

        private void imageDemo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //if (showinfo.Visibility == Visibility.Visible) showinfo.Visibility = Visibility.Collapsed;
        }

        private void grvCol_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void griBottom_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Grid testGrid = sender as Grid;
            Grid gridTop = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "gridTop");
            Grid griBottom = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "griBottom");
            gridTop.Visibility = Visibility.Visible;
            griBottom.Visibility = Visibility.Visible;
        }

        private void griBottom_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Grid testGrid = sender as Grid;
            Grid gridTop = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "gridTop");
            Grid griBottom = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "griBottom");
            griBottom.Visibility = Visibility.Collapsed;
            gridTop.Visibility = Visibility.Collapsed;
        }

        private void btDownColPage_Click(object sender, RoutedEventArgs e)
        {
            //CollectionRootObject aaa = (CollectionRootObject)e.OriginalSource;
            int a = 3;
        }
    }
}
