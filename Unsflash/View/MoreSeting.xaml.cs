using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class MoreSeting : Page
    {
        IReadOnlyList<StorageFile> thefiles;
        public MoreSeting()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            cbboxquatily.SelectedIndex = MainPage.GetLinkwithInt;

            long SizeFolder = 0;

            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            DirectoryInfo di = new DirectoryInfo(localFolder.Path);
            FileInfo[] fiArr = di.GetFiles();
            foreach (FileInfo f in fiArr)
            {
                SizeFolder += f.Length;
            }
            SizeFolder = SizeFolder / 1024;
            SizeFolder = SizeFolder / 1024;
            SizeFolder = SizeFolder * 8;

            sizeLocal.Text = "( Size: " + SizeFolder.ToString() + " MB)";

        }

        private void cbboxAutochange_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbboxquatily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int getIndex = cbboxquatily.SelectedIndex;
            switch (getIndex)
            {
                case 0:
                    MainPage.GetLinkwithInt = getIndex;
                    break;
                case 1:
                    MainPage.GetLinkwithInt = getIndex;
                    break;
                case 2:
                    MainPage.GetLinkwithInt = getIndex;
                    break;
            }
        }

        private async void btLogin_Click(object sender, RoutedEventArgs e)
        {
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            thefiles = await localFolder.GetFilesAsync();

            for (int i = 0; i < thefiles.Count; i++)
            {
                await thefiles[i].DeleteAsync(StorageDeleteOption.Default);
            }
            this.Frame.Navigate(typeof(MoreSeting));
        }
    }
}
