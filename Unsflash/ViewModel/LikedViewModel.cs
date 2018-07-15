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
    public class LikedViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<LikedModelRootObjects> LikedPhotos
        {
            get { return Me.likedRootObjects; }
            set
            {
                Me.likedRootObjects = value;
                if (Me.likedRootObjects != null)
                {

                    OnPropertyChanged("LikedPhotos");
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
