using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Unsflash.Controls;
using Unsflash.Model;
using Unsflash.ViewModel;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Unsflash.View
{

    public sealed partial class HomePage : Page
    {
        RootObject itemNew;
        Button itemDownv2;
        public static double grvWidth;
        public static int page = 1;
        public static int pagePopular = 1;
        PublicAuthorization publicAuthorization;
        public static ObservableCollection<RootObject> aaa = new ObservableCollection<RootObject>();
        public static ObservableCollection<RootObject> listNewImage = new ObservableCollection<RootObject>();
        public static ObservableCollection<RootObject> listPopularImage = new ObservableCollection<RootObject>();

        DownloadOperation downloadOperation;
        CancellationTokenSource cancellationToken;
        Windows.Networking.BackgroundTransfer.BackgroundDownloader backgroundDownloader = new Windows.Networking.BackgroundTransfer.BackgroundDownloader();

        public HomePage()
        {
            this.InitializeComponent();
        }

        public MainPanePhotoViewModel ViewModel { get; set; }


        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {           

            publicAuthorization = new PublicAuthorization();

            page = 1;
            RequestParameters.publicAuUri = RequestParameters.defaulUri + "?client_id=" + RequestParameters.client_id + "&page=" + page + "&per_page=30";
            listNewImage = await publicAuthorization.Authorization();

            while (listNewImage.Count == 0)
            {
                await Task.Delay(10);
                listNewImage = await publicAuthorization.Authorization();
            }


            double totalWidth = 0;
            int start = 0;

            this.ViewModel = new MainPanePhotoViewModel();

            while (grvStart.ActualWidth == 0)
            {
                await Task.Delay(10);
            }

            for (int i = 0; i < ViewModel.NewImages.Count; i++)
            {
                var width = ViewModel.NewImages[i].width * 310 / ViewModel.NewImages[i].height;
                totalWidth += width;

                if (totalWidth > grvStart.ActualWidth)
                {
                    for (int j = start; j < i; j++)
                    {
                        ViewModel.NewImages[j].Scale = grvStart.ActualWidth / (totalWidth - width);
                    }
                    start = i;
                    totalWidth = width;
                }
            }

            for (int j = start; j < ViewModel.NewImages.Count; j++)
            {
                ViewModel.NewImages[j].Scale = grvStart.ActualWidth / (totalWidth);
            }


            grvStart.ItemsSource = ViewModel.NewImages;
            RequestParameters.publicAuUri = "";
            griNewLoading.Visibility = Visibility.Collapsed;
        }

        private void griItem_PointerEntered(object sender, PointerRoutedEventArgs e)
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

        private void griItem_PointerExited(object sender, PointerRoutedEventArgs e)
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

        private async void pvHome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(pvHome.SelectedIndex == 1)
            {
                if(listPopularImage.Count == 0)
                {
                    publicAuthorization = new PublicAuthorization();
                    listPopularImage = await publicAuthorization.GetPopularImages();

                    while (listNewImage.Count == 0)
                    {
                        await Task.Delay(10);
                        listPopularImage = await publicAuthorization.GetPopularImages();
                    }

                    double totalWidth = 0;
                    int start = 0;

                    this.ViewModel = new MainPanePhotoViewModel();

                    while (grvPopular.ActualWidth == 0)
                    {
                        await Task.Delay(10);
                    }

                    for (int i = 0; i < ViewModel.PopularImages.Count; i++)
                    {
                        var width = ViewModel.PopularImages[i].width * 310 / ViewModel.PopularImages[i].height;
                        totalWidth += width;

                        if (totalWidth > grvPopular.ActualWidth)
                        {
                            for (int j = start; j < i; j++)
                            {
                                ViewModel.PopularImages[j].Scale = grvPopular.ActualWidth / (totalWidth - width);
                            }
                            start = i;
                            totalWidth = width;
                        }
                    }

                    for (int j = start; j < ViewModel.PopularImages.Count; j++)
                    {
                        ViewModel.PopularImages[j].Scale = grvPopular.ActualWidth / (totalWidth);
                    }


                    grvPopular.ItemsSource = ViewModel.PopularImages;
                }
                else
                {
                    grvPopular.ItemsSource = ViewModel.PopularImages;
                }

                griPopularLoading.Visibility = Visibility.Collapsed;
                RequestParameters.publicPopularUri = "";
            }
        }

        private void grvStart_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemNew = (RootObject)e.ClickedItem;

            Frame.Navigate(typeof(ViewPhotoPage), itemNew);
        }

        private void grvPopular_ItemClick(object sender, ItemClickEventArgs e)
        {
            itemNew = (RootObject)e.ClickedItem;

            Frame.Navigate(typeof(ViewPhotoPage), itemNew);
        }

        private async void btNext_Click(object sender, RoutedEventArgs e)
        {
            page++;

            if(page > 1)
            {
                btBack.IsEnabled = true;
            }
            
            RequestParameters.publicAuUri = RequestParameters.defaulUri + "?client_id=" + RequestParameters.client_id + "&page=" + page + "&per_page=30";

            listNewImage = await publicAuthorization.Authorization();

            await Task.Delay(300);

            while (listNewImage.Count == 0)
            {
                await Task.Delay(10);
                listNewImage = await publicAuthorization.Authorization();
            }


            double totalWidth = 0;
            int start = 0;

            this.ViewModel = new MainPanePhotoViewModel();

            while (grvStart.ActualWidth == 0)
            {
                await Task.Delay(10);
            }

            for (int i = 0; i < ViewModel.NewImages.Count; i++)
            {
                var width = ViewModel.NewImages[i].width * 310 / ViewModel.NewImages[i].height;
                totalWidth += width;

                if (totalWidth > grvStart.ActualWidth)
                {
                    for (int j = start; j < i; j++)
                    {
                        ViewModel.NewImages[j].Scale = grvStart.ActualWidth / (totalWidth - width);
                    }
                    start = i;
                    totalWidth = width;
                }
            }

            for (int j = start; j < ViewModel.NewImages.Count; j++)
            {
                ViewModel.NewImages[j].Scale = grvStart.ActualWidth / (totalWidth);
            }


            grvStart.ItemsSource = ViewModel.NewImages;
            RequestParameters.publicAuUri = "";
        }

        private async void btBack_Click(object sender, RoutedEventArgs e)
        {
            if(page == 2)
            {
                btBack.IsEnabled = false;
            }

            page--;

            RequestParameters.publicAuUri = RequestParameters.defaulUri + "?client_id=" + RequestParameters.client_id + "&page=" + page + "&per_page=30";

            listNewImage = await publicAuthorization.Authorization();

            await Task.Delay(300);

            while (listNewImage.Count == 0)
            {
                await Task.Delay(10);
                listNewImage = await publicAuthorization.Authorization();
            }


            double totalWidth = 0;
            int start = 0;

            this.ViewModel = new MainPanePhotoViewModel();

            while (grvStart.ActualWidth == 0)
            {
                await Task.Delay(10);
            }

            for (int i = 0; i < ViewModel.NewImages.Count; i++)
            {
                var width = ViewModel.NewImages[i].width * 310 / ViewModel.NewImages[i].height;
                totalWidth += width;

                if (totalWidth > grvStart.ActualWidth)
                {
                    for (int j = start; j < i; j++)
                    {
                        ViewModel.NewImages[j].Scale = grvStart.ActualWidth / (totalWidth - width);
                    }
                    start = i;
                    totalWidth = width;
                }
            }

            for (int j = start; j < ViewModel.NewImages.Count; j++)
            {
                ViewModel.NewImages[j].Scale = grvStart.ActualWidth / (totalWidth);
            }


            grvStart.ItemsSource = ViewModel.NewImages;
            RequestParameters.publicAuUri = "";
        }

        private async void btBackfe_Click(object sender, RoutedEventArgs e)
        {
            pagePopular--;
            if (pagePopular == 1)
            {
                btBackfe.IsEnabled = false;
            }

            publicAuthorization = new PublicAuthorization();
            RequestParameters.publicPopularUri = "https://api.unsplash.com/photos/?client_id=" + RequestParameters.client_id + "&page=" + pagePopular + "&per_page=30&order_by=popular";
            listPopularImage = await publicAuthorization.GetPopularImages();

            await Task.Delay(300);

            while (listNewImage.Count == 0)
            {
                await Task.Delay(10);
                listPopularImage = await publicAuthorization.GetPopularImages();
            }

            double totalWidth = 0;
            int start = 0;

            this.ViewModel = new MainPanePhotoViewModel();

            while (grvPopular.ActualWidth == 0)
            {
                await Task.Delay(10);
            }

            for (int i = 0; i < ViewModel.PopularImages.Count; i++)
            {
                var width = ViewModel.PopularImages[i].width * 310 / ViewModel.PopularImages[i].height;
                totalWidth += width;

                if (totalWidth > grvPopular.ActualWidth)
                {
                    for (int j = start; j < i; j++)
                    {
                        ViewModel.PopularImages[j].Scale = grvPopular.ActualWidth / (totalWidth - width);
                    }
                    start = i;
                    totalWidth = width;
                }
            }

            for (int j = start; j < ViewModel.PopularImages.Count; j++)
            {
                ViewModel.PopularImages[j].Scale = grvPopular.ActualWidth / (totalWidth);
            }


            grvPopular.ItemsSource = ViewModel.PopularImages;
            RequestParameters.publicPopularUri = "";
        }

        private async void btNextfe_Click(object sender, RoutedEventArgs e)
        {
            pagePopular++;
            if (pagePopular > 1)
            {
                btBackfe.IsEnabled = true;
            }

            publicAuthorization = new PublicAuthorization();
            RequestParameters.publicPopularUri = "https://api.unsplash.com/photos/?client_id=" + RequestParameters.client_id + "&page=" + pagePopular + "&per_page=30&order_by=popular";
            listPopularImage = await publicAuthorization.GetPopularImages();

            await Task.Delay(300);

            while (listNewImage.Count == 0)
            {
                await Task.Delay(10);
                listPopularImage = await publicAuthorization.GetPopularImages();
            }

            double totalWidth = 0;
            int start = 0;

            this.ViewModel = new MainPanePhotoViewModel();

            while (grvPopular.ActualWidth == 0)
            {
                await Task.Delay(10);
            }

            for (int i = 0; i < ViewModel.PopularImages.Count; i++)
            {
                var width = ViewModel.PopularImages[i].width * 310 / ViewModel.PopularImages[i].height;
                totalWidth += width;

                if (totalWidth > grvPopular.ActualWidth)
                {
                    for (int j = start; j < i; j++)
                    {
                        ViewModel.PopularImages[j].Scale = grvPopular.ActualWidth / (totalWidth - width);
                    }
                    start = i;
                    totalWidth = width;
                }
            }

            for (int j = start; j < ViewModel.PopularImages.Count; j++)
            {
                ViewModel.PopularImages[j].Scale = grvPopular.ActualWidth / (totalWidth);
            }


            grvPopular.ItemsSource = ViewModel.PopularImages;
            RequestParameters.publicPopularUri = "";
        }

        private void buttonDownv2_Click(object sender, RoutedEventArgs e)
        {
            itemDownv2 = sender as Button;
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
                StorageFile file = await folder.CreateFileAsync("Unplash-v2.jpg", CreationCollisionOption.GenerateUniqueName);
                string a = itemDownv2.Content.ToString();
                Uri durl = new Uri(a);

                downloadOperation = backgroundDownloader.CreateDownload(durl, file);

                Progress<DownloadOperation> progress = new Progress<DownloadOperation>(progressChanged);
                cancellationToken = new CancellationTokenSource();

                try
                {
                    //Statusring.IsActive = true;
                    statusDownv2.Visibility = Visibility.Visible;
                    statusDownv2fe.Visibility = Visibility.Visible;
                    await downloadOperation.StartAsync().AsTask(cancellationToken.Token, progress);
                }
                catch (TaskCanceledException)
                {

                    downloadOperation.ResultFile.DeleteAsync();
                    downloadOperation = null;
                }
            }
        }
        private async void progressChanged(DownloadOperation downloadOperation)
        {
            //Statustext.Visibility = Visibility.Visible;
            //Statusring.IsActive = false;
            int progress = (int)(100 * ((double)downloadOperation.Progress.BytesReceived / (double)downloadOperation.Progress.TotalBytesToReceive));
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
                downloadingText.Text = "Đã tải";
                downloadingTextfe.Text = "Đã tải";
                downloadingBar.Visibility = Visibility.Collapsed;
                downloadingBarfe.Visibility = Visibility.Collapsed;
                await Task.Delay(1500);
                statusDownv2.Visibility = Visibility.Collapsed;
                statusDownv2fe.Visibility = Visibility.Collapsed;
                //Statustext.Visibility = Visibility.Collapsed;
            }
        }
    }
}
