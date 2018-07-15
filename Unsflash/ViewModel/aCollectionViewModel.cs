using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unsflash.Model;
using Unsflash.View;

namespace Unsflash.ViewModel
{
    public class aCollectionViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<GetaCollectionRootObject> aCollectionPhoto
        {
            get { return ViewCollectionsPage.aCollection; }
            set
            {
                ViewCollectionsPage.aCollection = value;
                if (ViewCollectionsPage.aCollection != null)
                {

                    OnPropertyChanged("aCollectionPhoto");
                }

            }
        }
        
        public ObservableCollection<GetaCollectionRootObject> aaCollectionPhoto
        {
            get { return ViewCollectionsPage.aaCollection; }
            set
            {
                ViewCollectionsPage.aaCollection = value;
                if(ViewCollectionsPage.aaCollection != null)
                {
                    OnPropertyChanged("aaCollectionPhoto");
                }
            }
        }

        public ObservableCollection<GetaCollectionRootObject> CollectionPhotoMe
        {
            get { return Me.CollectionMe; }
            set
            {
                Me.CollectionMe = value;
                {
                    if(Me.CollectionMe != null)
                    {
                        OnPropertyChanged("CollectionMe");
                    }
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
