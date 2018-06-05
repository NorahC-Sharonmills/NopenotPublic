using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Unsflash.Model;
using Windows.Security.Authentication.Web;
using static Unsflash.Model.MainPagePhotos;

namespace Unsflash.ViewModel
{
    class PublicAuthorization
    {
        public async Task<ObservableCollection<RootObject>> Authorization()
        {
            Uri auUri = new Uri(RequestParameters.publicAuUri);

            HttpClient httpClient = new HttpClient();

            string responseJson = await httpClient.GetStringAsync(auUri);

            ObservableCollection<RootObject> listNewImage = JsonConvert.DeserializeObject<ObservableCollection<RootObject>>(responseJson);

            return listNewImage;
        }

        public async Task<ObservableCollection<RootObject>> GetPopularImages()
        {
            Uri auUri = new Uri(RequestParameters.publicPopularUri);

            HttpClient httpClient = new HttpClient();

            string responseJson = await httpClient.GetStringAsync(auUri);

            ObservableCollection<RootObject> listPopularImage = JsonConvert.DeserializeObject<ObservableCollection<RootObject>>(responseJson);

            return listPopularImage;
        }

        public async Task<ObservableCollection<CollectionRootObject>> GetFeatured()
        {
            Uri featuredCollectionUri = new Uri(RequestParameters.featuredCollectionUri);

            HttpClient httpClient = new HttpClient();

            string responseJson = await httpClient.GetStringAsync(featuredCollectionUri);

            ObservableCollection<CollectionRootObject> listFeaturedCollection = JsonConvert.DeserializeObject<ObservableCollection<CollectionRootObject>>(responseJson);

            return listFeaturedCollection;
        }

        public async Task<ObservableCollection<CollectionRootObject>> GetCurated()
        {
            Uri featuredCollectionUri = new Uri(RequestParameters.curatedCollectionUri);

            HttpClient httpClient = new HttpClient();

            string responseJson = await httpClient.GetStringAsync(featuredCollectionUri);

            ObservableCollection<CollectionRootObject> listCuratedCollection = 
                JsonConvert.DeserializeObject<ObservableCollection<CollectionRootObject>>(responseJson);

            return listCuratedCollection;
        }
    }
}
