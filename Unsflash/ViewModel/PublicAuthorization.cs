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

            ObservableCollection<RootObject> listNewImage = JsonConvert.DeserializeObject<ObservableCollection<RootObject>>(responseJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return listNewImage;
        }

        public async Task<ObservableCollection<RootObject>> GetPopularImages()
        {
            Uri auUri = new Uri(RequestParameters.publicPopularUri);

            HttpClient httpClient = new HttpClient();

            string responseJson = await httpClient.GetStringAsync(auUri);

            ObservableCollection<RootObject> listPopularImage = JsonConvert.DeserializeObject<ObservableCollection<RootObject>>(responseJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return listPopularImage;
        }

        public async Task<ObservableCollection<CollectionRootObject>> GetFeatured()
        {
            Uri featuredCollectionUri = new Uri(RequestParameters.featuredCollectionUri);

            HttpClient httpClient = new HttpClient();

            string responseJson = await httpClient.GetStringAsync(featuredCollectionUri);

            ObservableCollection<CollectionRootObject> listFeaturedCollection = JsonConvert.DeserializeObject<ObservableCollection<CollectionRootObject>>(responseJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return listFeaturedCollection;
        }

        public async Task<ObservableCollection<CollectionRootObject>> GetCurated()
        {
            Uri featuredCollectionUri = new Uri(RequestParameters.curatedCollectionUri);

            HttpClient httpClient = new HttpClient();

            string responseJson = await httpClient.GetStringAsync(featuredCollectionUri);

            ObservableCollection<CollectionRootObject> listCuratedCollection = JsonConvert.DeserializeObject<ObservableCollection<CollectionRootObject>>(responseJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return listCuratedCollection;
        }

        public async Task<SearchPhotoObjects> SearchPhotoaaa()
        {
            Uri searchPhotoaaaUri = new Uri(RequestParameters.photoSearchUri);

            HttpClient httpClient = new HttpClient();

            string responseJson = await httpClient.GetStringAsync(searchPhotoaaaUri);
            
            SearchPhotoObjects searchPhotoObjects = JsonConvert.DeserializeObject<SearchPhotoObjects>(responseJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            
            return searchPhotoObjects;
        }

        public async Task<ObservableCollection<GetaCollectionRootObject>> GetaCollectionaaa()
        {
            Uri Dataaaaacollection = new Uri(RequestParameters.feCollectionIDUri);

            HttpClient httpClient = new HttpClient();

            string responseJson = await httpClient.GetStringAsync(Dataaaaacollection);

            ObservableCollection<GetaCollectionRootObject> aCollection = JsonConvert.DeserializeObject<ObservableCollection<GetaCollectionRootObject>>(responseJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return aCollection;
        }

        public async Task<ObservableCollection<GetaCollectionRootObject>> GetaCollectionaaaa()
        {
            Uri Dataacollectionaaa = new Uri(RequestParameters.curCollectionIDUri);

            HttpClient httpClient = new HttpClient();

            string responseJson = await httpClient.GetStringAsync(Dataacollectionaaa);

            ObservableCollection<GetaCollectionRootObject> aaCollection = JsonConvert.DeserializeObject<ObservableCollection<GetaCollectionRootObject>>(responseJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return aaCollection;
        }

        public async Task<MeRootObjects> GetInfoUserMe()
        {
            Uri uriData = new Uri(RequestParameters.AuthorizationUri);

            HttpClient httpClient = new HttpClient();

            string responseJson = await httpClient.GetStringAsync(uriData);

            MeRootObjects data = JsonConvert.DeserializeObject<MeRootObjects>(responseJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return data;
        }

        public async Task<ObservableCollection<LikedModelRootObjects>> GetLiked()
        {
            Uri uriData = new Uri(RequestParameters.LikedUser);

            HttpClient httpClient = new HttpClient();

            string responseJson = await httpClient.GetStringAsync(uriData);

            ObservableCollection<LikedModelRootObjects> data = JsonConvert.DeserializeObject<ObservableCollection<LikedModelRootObjects>>(responseJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return data;
        }

        public async Task<ObservableCollection<CollectionRootObject>> GetMeCollection()
        {
            Uri uriData = new Uri(RequestParameters.MeCollection);

            HttpClient httpClient = new HttpClient();

            string responseJson = await httpClient.GetStringAsync(uriData);

            ObservableCollection<CollectionRootObject> data = JsonConvert.DeserializeObject<ObservableCollection<CollectionRootObject>>(responseJson, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            return data;
        }
    }
}
