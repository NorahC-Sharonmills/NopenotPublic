using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Unsflash.Model;
using Unsflash.ViewModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Unsflash.View
{

    public sealed partial class HomePage : Page
    {
        RootObject itemNew;
        public static double grvWidth;
        public static ObservableCollection<RootObject> listNewImage = new ObservableCollection<RootObject>();
        public static ObservableCollection<RootObject> listPopularImage = new ObservableCollection<RootObject>();

        public HomePage()
        {
            this.InitializeComponent();
        }

        public MainPanePhotoViewModel ViewModel { get; set; }


        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {           
            PublicAuthorization publicAuthorization = new PublicAuthorization();

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
            griNewLoading.Visibility = Visibility.Collapsed;
        }

        private void griItem_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Grid testGrid = sender as Grid;
            Grid gridTop = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "gridTop");
            Grid griBottom = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "griBottom");
            gridTop.Visibility = Visibility.Visible;
            griBottom.Visibility = Visibility.Visible;

        }

        private void griItem_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Grid testGrid = sender as Grid;
            Grid gridTop = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "gridTop");
            Grid griBottom = (Grid)GetChildControl.GetChildren(testGrid).Find(x => x.Name == "griBottom");
            griBottom.Visibility = Visibility.Collapsed;
            gridTop.Visibility = Visibility.Collapsed;
        }

        private async void pvHome_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(pvHome.SelectedIndex == 1)
            {
                if(listPopularImage.Count == 0)
                {
                    PublicAuthorization publicAuthorization = new PublicAuthorization();
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

        private void btdownHome_Click(object sender, RoutedEventArgs e)
        {
            int a = 3;
        }
    }
}
