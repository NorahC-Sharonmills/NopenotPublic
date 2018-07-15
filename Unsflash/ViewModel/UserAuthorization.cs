using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Unsflash.Controls;
using Unsflash.Model;
using Unsflash.View;
using Windows.Security.Authentication.Web;
using Windows.UI.Popups;

namespace Unsflash.ViewModel
{
    class UserAuthorization
    {
        public string responeData;
        public bool rep = false;
        public static string token_uri = "https://unsplash.com/oauth/token?"
                + "client_id=" + RequestParameters.client_id
                + "&client_secret=" + RequestParameters.client_secret
                + "&redirect_uri=" + RequestParameters.redirect_uri
                + "&" + RequestParameters.code
                + "&grant_type=authorization_code";

        public async void Authorization()
        {
            Uri StartUri = new Uri(RequestParameters.api_url);
            Uri EndUri = new Uri(RequestParameters.redirect_uri);

            WebAuthenticationResult WebAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
                                                    WebAuthenticationOptions.None,
                                                    StartUri,
                                                    EndUri);
            if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.Success)
            {
                RequestParameters.code = WebAuthenticationResult.ResponseData.ToString();
                RequestParameters.code = RequestParameters.code.Substring(RequestParameters.code.IndexOf("code"));
            }
            else if (WebAuthenticationResult.ResponseStatus == WebAuthenticationStatus.ErrorHttp)
            {

            }
            else
            {

            }

            HttpClient httpClient = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, token_uri);
            HttpResponseMessage response = await httpClient.SendAsync(request);

            responeData = await response.Content.ReadAsStringAsync();

            try
            {
                UsingGlobal.meRoot = JsonConvert.DeserializeObject<AuthRootObjects>(responeData, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                MessageDialog ms = new MessageDialog("LOGIN FAIL!!!");
                ms.ShowAsync();
            }

        }
    }
}
