using Newtonsoft.Json;
using Mobile.HttpREST;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Mobile.Services;
using Mobile.Model;

namespace Mobile.HttpREST
{
    public class RESTHelper
    {
        static readonly JsonSerializer Serializer = new JsonSerializer();
        public static bool IsConnected()
        {
            return Xamarin.Essentials.Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet;
        }


        #region Login Method
        public static async Task<LoginResultModel> GetLoginResultModel(LoginModel loginModel, string loginUrl, string acceptMediaType = "application/json")
        {
            try
            {
                #region Is valid model
                if (loginModel == null || string.IsNullOrWhiteSpace(loginModel?.Username) || string.IsNullOrWhiteSpace(loginModel?.Password))
                    return new LoginResultModel() { Error = "Invalid input", ErrorDescription = "Invalid username/ password" };
                #endregion

                #region IsConnected
                if (!IsConnected())
                    return new LoginResultModel() { Error = "Connection problem", ErrorDescription = "No connection available!" };
                #endregion

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(loginUrl);
                    client.DefaultRequestHeaders.Accept.Clear();

                    if (!string.IsNullOrWhiteSpace(acceptMediaType))
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptMediaType));

                    //if (!string.IsNullOrWhiteSpace(fromKey))
                    //    client.DefaultRequestHeaders.TryAddWithoutValidation("From", fromKey);

                    var formEncodedContent = new FormUrlEncodedContent(new[]
                    {
                         new KeyValuePair<string, string>("grant_type", "password"),
                         new KeyValuePair<string, string>("username", loginModel.Username),
                         new KeyValuePair<string, string>("password", loginModel.Password),
                    });

                    var response = await client.PostAsync(string.Empty, formEncodedContent).ConfigureAwait(false);


#if DEBUG
                    // Memory Heavy in production
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        var text = await StreamToStringAsync(stream);
                        return JsonConvert.DeserializeObject<LoginResultModel>(text);
                    }
#else
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(stream))
                    using (var json = new JsonTextReader(reader))
                    {
                        return Serializer.Deserialize<LoginResultModel>(json);
                    }
#endif
                }
            }
            catch (Exception ex)
            {
                return new LoginResultModel() { Error = "Error", ErrorDescription = ex.Message };
            }
        }
        #endregion

        #region GetRequest GetParams as NameValueCollection
        public static async Task<RESTServiceResponse<T>> GetRequest<T>(string token, string url, HttpVerbs method = HttpVerbs.GET, NameValueCollection getParams = null, object postObject = null, string contentType = "application/json",string fromKey = "")
        {
            try
            {
                #region IsConnected
                if (!IsConnected())
                {
                    return new RESTServiceResponse<T>(false, "Vous n'êtes pas connéctés !");
                }
                #endregion


                using (var client = new HttpClient())
                {
                    //setup client
                    Uri uri = new Uri(url);
                    #region Setting Attachements

                    if (method == HttpVerbs.GET && getParams != null)
                    {
                        uri = uri.AttachParameters(getParams);
                    }

                    #endregion
                    client.BaseAddress = uri;
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                    if (!string.IsNullOrWhiteSpace(fromKey))
                        client.DefaultRequestHeaders.TryAddWithoutValidation("From", fromKey);

                    HttpResponseMessage response = new HttpResponseMessage();
                    switch (method)
                    {
                        case HttpVerbs.GET:
                            response = await client.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
                            break;
                        case HttpVerbs.POST:
                            var json = JsonConvert.SerializeObject(postObject).ToString();
                            var content = new StringContent(json, Encoding.UTF8, "application/json");
                            response = await client.PostAsync(uri, content).ConfigureAwait(false);
                            break;
                        default:
                            break;
                    }

#if DEBUG
                    // Memory Heavy in production
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    {
                        var text = await StreamToStringAsync(stream);
                        return JsonConvert.DeserializeObject<RESTServiceResponse<T>>(text);
                    }
#else
                    using (var stream = await response.Content.ReadAsStreamAsync())
                    using (var reader = new StreamReader(stream))
                    using (var json = new JsonTextReader(reader))
                    {
                        return Serializer.Deserialize<RESTServiceResponse<T>>(json);
                    }
#endif
                }
            }
            catch (Exception ex)
            {
                return new RESTServiceResponse<T>(false, ex.Message);
            }
        }
        #endregion

        private static async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
                using (var sr = new StreamReader(stream))
                    content = await sr.ReadToEndAsync();

            return content;
        }
    }


    #region Helpers
    

    public static class RESTExtensions
    {
        #region Attach "NameValueCollection" Parameters
        public static Uri AttachParameters(this Uri uri, NameValueCollection parameters)
        {
            var stringBuilder = new StringBuilder();
            string str = "?";
            for (int index = 0; index < parameters.Count; ++index)
            {
                stringBuilder.Append(str + parameters.AllKeys[index] + "=" + parameters[index]);
                str = "&";
            }
            return new Uri(uri + stringBuilder.ToString());
        }
        #endregion
    }

    #endregion
}
