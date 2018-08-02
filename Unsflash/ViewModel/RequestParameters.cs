using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unsflash.Controls;
using Unsflash.Model;
using Unsflash.View;

namespace Unsflash.ViewModel
{
    class RequestParameters
    {
        //Thầy
        //public static string redirect_uri = "https://www.facebook.com/nasa1992";
        //public static string client_id = "3d2984268b6ddb60817f538fd89760421fd522048de87620e456c00cf867a260";
        //public static string client_secret = "dd616a0006659294d84d723dfcaa6d0fd8f3dbced6e4e23da7a2290b4cb5c774";

        //Me
        public static string redirect_uri = "https://www.facebook.com/HihiDeoCoThangNaoCoTenFaceGiongTao";
        public static string client_id = "a9915884c0c516e6a40ce75859c96a2d5e6e1e3f18401faf3ad4c99772ca6744";
        public static string client_secret = "325a343141fc5529205801fb477bc283ed4cbc63a35ffbc9edb6e39d6930a344";

        public static string code = "";
        public static string api_url = "https://unsplash.com/oauth/authorize?"
            + "client_id=" + client_id
            + "&redirect_uri=" + redirect_uri
            + "&response_type=code"
            //+ "&scope=public+read_user"; // cmt nó lại luôn.
            + "&scope=public+read_user+write_user+read_photos+write_photos+write_likes+write_followers+read_collections+write_collections";

        public static string token_uri = "https://unsplash.com/oauth/token?"
                + "client_id=" + client_id
                + "&client_secret=" + client_secret
                + "&redirect_uri=" + redirect_uri
                + "&" + code
                + "grant_type=authorization_code";
        public static string defaulUri = "https://api.unsplash.com/photos/";
        public static string publicAuUri = defaulUri + "?client_id=" + client_id + "&page=" + HomePage.page + "&per_page=30";
        public static string publicPopularUri = "https://api.unsplash.com/photos/?client_id=" + client_id + "&page=" + HomePage.pagePopular + "&per_page=30&order_by=popular";

        //Collection
        public static string featuredCollectionUri = "https://api.unsplash.com/collections/featured?client_id=" + client_id + "&page=1&per_page=30";
        public static string curatedCollectionUri = "https://api.unsplash.com/collections/curated?client_id=" + client_id + "&page=1&per_page=30";

        //Search
        public static string photoSearchUri = "https://api.unsplash.com/search/photos/?client_id=" + client_id + "&page=" + showSearchPage.pageSearch + "&per_page=30&query=";
        public static string collectionSearchUri = "https://api.unsplash.com/search/collections/?client_id=" + client_id + "&per_page=30&query=";

        //Get a collection
        public static string feCollectionIDUri = "https://api.unsplash.com/collections/";
        public static string curCollectionIDUri = "https://api.unsplash.com/collections/curated/";

        //Get me
        public static string AuthorizationUri = "https://api.unsplash.com/me?access_token=";

        //Get liked
        public static string LikedUser = "https://api.unsplash.com/users/";
        //https://api.unsplash.com/users/lismover/likes?access_token=5aba92a6a8980713c7cc944f7cbf8859739323800bcbea379c62ff08ad2499e2

        //Get me collection
        public static string MeCollection = "https://api.unsplash.com/users/";
        //https://api.unsplash.com/users/lismover/collections?access_token=75b14fa3aa7d08af1aae5d3a7fc90831e2f6548ef92759237390985d8b1fbaf8



    }
}
