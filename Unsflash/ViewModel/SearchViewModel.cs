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
    public class SearchViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ResultModel> ListImageRes
        {
            get { return showSearchPage.MySearchRes; }
            set
            {
                showSearchPage.MySearchRes = value;
                if (showSearchPage.MySearchRes != null)
                {
                    OnPropertyChanged("ListImage");
                }
            }
        }

        //public SearchPhotoObjects ListCol
        //{
        //    get { return HomePage.listPopularImage; }
        //    set
        //    {
        //        HomePage.listPopularImage = value;
        //        if (HomePage.listPopularImage != null)
        //        {

        //            OnPropertyChanged("PopularImages");
        //        }

        //    }
        //}

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
