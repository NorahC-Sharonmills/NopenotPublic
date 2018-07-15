using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Unsflash.Controls;
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
using Windows.UI.Core;
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
    public sealed partial class TestControl : Page
    {
        public DetailColPhotoModel.RootObject rootObject;
        GetaCollectionRootObject item;

        DownloadOperation downloadOperation;
        CancellationTokenSource cancellationToken;
        Windows.Networking.BackgroundTransfer.BackgroundDownloader backgroundDownloader = new Windows.Networking.BackgroundTransfer.BackgroundDownloader();

        public TestControl()
        {
            this.InitializeComponent();
            RegisterForShare();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (UsingGlobal.meRoot.access_token == null)
            {
                bdLikes.Visibility = Visibility.Collapsed;
                bdCollect.Visibility = Visibility.Collapsed;
            }
            else
            {
                bdLikes.Visibility = Visibility.Visible;
                bdCollect.Visibility = Visibility.Visible;
            }

            HttpClient httpClient;
            string requestUri;

                item = (GetaCollectionRootObject)e.Parameter;

                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.UriSource = new Uri(item.user.profile_image.medium);
                imbAuthor.ImageSource = bitmapImage;

                tblAuthorName.Text = item.user.name;
                tblUserName.Text = item.user.username;
                tblLike.Text = item.likes.ToString();

                BitmapImage bitmapImageShow = new BitmapImage();
                bitmapImageShow.UriSource = new Uri(item.urls.full);
                imgShow.Source = bitmapImageShow;

                httpClient = new HttpClient();
                requestUri = RequestParameters.defaulUri + item.id + "/?client_id=" + RequestParameters.client_id;

            try
            {
                string reponseData = await httpClient.GetStringAsync(requestUri);
                rootObject = JsonConvert.DeserializeObject<DetailColPhotoModel.RootObject>(reponseData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception)
            {
                Noreult.Visibility = Visibility.Visible;
                Truereult.Visibility = Visibility.Collapsed;
            }
        }

        //Hiện thông tin ảnh
        private void btInfo_Click(object sender, RoutedEventArgs e)
        {
            if (showinfo.Visibility == Visibility.Visible) showinfo.Visibility = Visibility.Collapsed;
            else showinfo.Visibility = Visibility.Visible;
            if (rootObject.user.name == null) nameuser.Text = " ";
            else nameuser.Text = " " + rootObject.user.name;
            if (rootObject.user.bio == null) biouser.Text = " ";
            else biouser.Text = " " + rootObject.user.bio;
            if (rootObject.created_at.Date.ToString() == null) createTime.Text = " ";
            else createTime.Text = rootObject.created_at.Date.ToString();
            if (rootObject.updated_at.Date.ToString() == null) updateTime.Text = " ";
            else updateTime.Text = rootObject.updated_at.Date.ToString();
            if (rootObject.exif.make == null) cameramaker.Text = " ";
            else cameramaker.Text = " " + rootObject.exif.make;
            if (rootObject.exif.model == null) cameramodel.Text = " ";
            else cameramodel.Text = " " + rootObject.exif.model;
            if (rootObject.exif.iso.ToString() == null) cameraiso.Text = " ";
            else cameraiso.Text = " " + rootObject.exif.iso.ToString();
            if (rootObject.exif.aperture == null) camerafstop.Text = " ";
            else camerafstop.Text = " " + rootObject.exif.aperture;
            if (rootObject.exif.focal_length == null) camerafocalleght.Text = " ";
            else camerafocalleght.Text = " " + rootObject.exif.focal_length;

            Infotext.Text = rootObject.views.ToString();
        }

        //tải ảnh
        private void btDownload_Click(object sender, RoutedEventArgs e)
        {
            if (showinfo.Visibility == Visibility.Visible) showinfo.Visibility = Visibility.Collapsed;
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
                StorageFile file = await folder.CreateFileAsync("Unplash-" + rootObject.user.name + ".jpg", CreationCollisionOption.GenerateUniqueName);
                Uri durl = new Uri(rootObject.links.download);
                downloadOperation = backgroundDownloader.CreateDownload(durl, file);

                Progress<DownloadOperation> progress = new Progress<DownloadOperation>(progressChanged);
                cancellationToken = new CancellationTokenSource();

                try
                {
                    //Statustext.Text = "Initial vizing...";
                    Statusring.IsActive = true;
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
            Statustext.Visibility = Visibility.Visible;
            Statusring.IsActive = false;
            int progress = (int)(100 * ((double)downloadOperation.Progress.BytesReceived / (double)downloadOperation.Progress.TotalBytesToReceive));
            //Statustext.Text = String.Format("{0} of {1} kb. downloaded - %{2} complete.", downloadOperation.Progress.BytesReceived / 1024, downloadOperation.Progress.TotalBytesToReceive / 1024, progress);
            Statustext.Value = progress;

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
                Statustext.Visibility = Visibility.Collapsed;
            }
        }

        //Cài hình ảnh làm hình nền
        private async void btsetDeskTop_Click(object sender, RoutedEventArgs e)
        {
            if (showinfo.Visibility == Visibility.Visible) showinfo.Visibility = Visibility.Collapsed;
            Statusring.IsActive = true;
            await ChangeBackground();
            Statusring.IsActive = false;
        }

        private async Task ChangeBackground()
        {
            if (UserProfilePersonalizationSettings.IsSupported())
            {
                Uri uri = new Uri(item.urls.full);
                using (Windows.Web.Http.HttpClient client = new Windows.Web.Http.HttpClient())
                {
                    try
                    {
                        Windows.Web.Http.HttpResponseMessage response = await client.GetAsync(uri);
                        if (response != null && response.StatusCode == Windows.Web.Http.HttpStatusCode.Ok)
                        {


                            string filename = "backgroundCol.jpg";
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

        //Chia sẻ ảnh
        private void btShare_Click(object sender, RoutedEventArgs e)
        {
            if (showinfo.Visibility == Visibility.Visible) showinfo.Visibility = Visibility.Collapsed;
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

            Uri uri = new Uri(item.urls.full);

            // Plain text
            request.Data.SetText("Unplash-" + rootObject.user.name);

            // Link
            request.Data.SetWebLink(uri);    

            // HTML
            request.Data.SetHtmlFormat("<b>Bold Text</b>");

            IRandomAccessStream stream;
            string filename = "shareimgCol.jpg";
            
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

        //Phóng to ảnh
        private async void ScrollViewer_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;
            var doubleTapPoint = e.GetPosition(scrollViewer);

            if (scrollViewer.ZoomFactor != 1)
            {
                scrollViewer.ZoomToFactor(1);
            }
            else if (scrollViewer.ZoomFactor == 1)
            {
                scrollViewer.ZoomToFactor(2);

                var dispatcher = Window.Current.CoreWindow.Dispatcher;
                await dispatcher.RunAsync(CoreDispatcherPriority.High, () =>
                {
                    scrollViewer.ScrollToHorizontalOffset(doubleTapPoint.X);
                    scrollViewer.ScrollToVerticalOffset(doubleTapPoint.Y);
                });
            }
        }
        //Phóng to ảnh 2
        bool isZoomed = false;

        private void imgShow_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {

        }

        double scalevalue = 0.0;
        double scaleTHreshold = 4.0;
        private void imgShow_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        {

        }
    }
}
