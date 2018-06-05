using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
    public sealed partial class ViewPhotoPage : Page
    {
        public DetailPhotoModel.RootObject rootObject;
        RootObject item;

        DownloadOperation downloadOperation;
        CancellationTokenSource cancellationToken;
        Windows.Networking.BackgroundTransfer.BackgroundDownloader backgroundDownloader = new Windows.Networking.BackgroundTransfer.BackgroundDownloader();

        DataTransferManager dataTransferManager = DataTransferManager.GetForCurrentView();
        public ViewPhotoPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            item = (RootObject)e.Parameter;

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.UriSource = new Uri(item.user.profile_image.medium);
            imbAuthor.ImageSource = bitmapImage;

            tblAuthorName.Text = item.user.name;
            tblUserName.Text = item.user.username;
            tblLike.Text = item.likes.ToString();

            BitmapImage bitmapImageShow = new BitmapImage();
            bitmapImageShow.UriSource = new Uri(item.urls.regular);
            imgShow.Source = bitmapImageShow;

            HttpClient httpClient = new HttpClient();
            string requestUri = RequestParameters.defaulUri + item.id + "/?client_id=" + RequestParameters.client_id;
            string reponseData = await httpClient.GetStringAsync(requestUri);

            rootObject = JsonConvert.DeserializeObject<DetailPhotoModel.RootObject>(reponseData);
            int a = 3;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            dataTransferManager = DataTransferManager.GetForCurrentView();
            dataTransferManager.DataRequested += DataTransferManager_DataRequested;
        }

        private void DataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {

            DataRequest request = args.Request;
            request.Data.Properties.Title = "Share Contract Lesson by Duy Hihi";
            //request.Data.SetBitmap();
            request.Data.SetText("Unplash-" + rootObject.user.name);
            request.Data.SetWebLink(new Uri("www.google.com"));
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            dataTransferManager.DataRequested -= DataTransferManager_DataRequested;
        }

        private void btInfo_Click(object sender, RoutedEventArgs e)
        {
            if(listInfo.Visibility == Visibility.Visible) listInfo.Visibility = Visibility.Collapsed;
            else listInfo.Visibility = Visibility.Visible;
            if(rootObject.user.name == null) nameuser.Text = " ";
            else nameuser.Text = " " + rootObject.user.name;
            if (rootObject.user.bio == null) biouser.Text = " ";
            else biouser.Text = " " + rootObject.user.bio;
            if (rootObject.location == null) locationpic.Text = " ";
            else locationpic.Text = " " + rootObject.location.title;
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

        }

        private void btShare_Click(object sender, RoutedEventArgs e)
        {
            if (listInfo.Visibility == Visibility.Visible) listInfo.Visibility = Visibility.Collapsed;
            DataTransferManager.ShowShareUI();
        }

        private async void btDownload_Click(object sender, RoutedEventArgs e)
        {
            if (listInfo.Visibility == Visibility.Visible) listInfo.Visibility = Visibility.Collapsed;
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
                    Statustext.Text = "Initializing...";
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
            int progress = (int)(100 * ((double)downloadOperation.Progress.BytesReceived / (double)downloadOperation.Progress.TotalBytesToReceive));
            Statustext.Text = String.Format("{0} of {1} kb. downloaded - %{2} complete.", downloadOperation.Progress.BytesReceived / 1024, downloadOperation.Progress.TotalBytesToReceive / 1024, progress);
            

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
                case BackgroundTransferStatus.Error:
                    {
                        Statustext.Text = "An error occured while downloading.";
                        break;
                    }
            }
            if (progress >= 100)
            {
                downloadOperation = null;
                Statustext.Text = "Download Complete";
                //Task.Delay(TimeSpan.FromSeconds(5));
                Statustext.Visibility = Visibility.Collapsed;
            }
        }

    }
}
