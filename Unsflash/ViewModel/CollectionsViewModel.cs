using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unsflash.Model;

namespace Unsflash.ViewModel
{
    public class CollectionsViewModel : INotifyPropertyChanged
    {
        public static ObservableCollection<CollectionRootObject> listFeaturedCollection = new ObservableCollection<CollectionRootObject>();
        public static ObservableCollection<CollectionRootObject> listCuratedCollection = new ObservableCollection<CollectionRootObject>();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CollectionRootObject> FeaturedCollections
        {
            get { return listFeaturedCollection; }
            set
            {
                listFeaturedCollection = value;
                if (listFeaturedCollection != null)
                {

                    OnPropertyChanged("FeaturedCollections");
                }

            }
        }

        public ObservableCollection<CollectionRootObject> CuratedCollections
        {
            get { return listCuratedCollection; }
            set
            {
                listCuratedCollection = value;
                if (listCuratedCollection != null)
                {

                    OnPropertyChanged("CuratedCollections");
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
