using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unsflash.Model;
using Unsflash.View;
using static Unsflash.Model.MainPagePhotos;

namespace Unsflash.ViewModel
{
    public class MainPanePhotoViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<RootObject> NewImages
        {
            get { return HomePage.listNewImage; }
            set
            {
                HomePage.listNewImage = value;
                if (HomePage.listNewImage != null)
                {

                    OnPropertyChanged("NewImages");
                }

            }
        }

        public ObservableCollection<RootObject> PopularImages
        {
            get { return HomePage.listPopularImage; }
            set
            {
                HomePage.listPopularImage = value;
                if (HomePage.listPopularImage != null)
                {

                    OnPropertyChanged("PopularImages");
                }

            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}
