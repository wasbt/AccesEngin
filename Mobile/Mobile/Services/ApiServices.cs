using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Mobile.Helpers;
using Mobile.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Shared.DTO;

namespace Mobile.Services
{
    internal class ApiServices
    {
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> LoginAsync(string userName, string password)
        {
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", userName),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password")
            };

            var request = new HttpRequestMessage(
                HttpMethod.Post, Constants.BaseApiAddress + "Token");

            request.Content = new FormUrlEncodedContent(keyValues);

            var client = new HttpClient();
            var response = await client.SendAsync(request);

            var content = await response.Content.ReadAsStringAsync();

            JObject jwtDynamic = JsonConvert.DeserializeObject<dynamic>(content);

            var accessTokenExpiration = jwtDynamic.Value<DateTime>(".expires");
            var accessToken = jwtDynamic.Value<string>("access_token");

            Settings.AccessTokenExpirationDate = accessTokenExpiration;

            Debug.WriteLine(accessTokenExpiration);

            Debug.WriteLine(content);

            return accessToken;
        }


        /// <summary>
        /// Get List Demande
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns>DemandeAcces</returns>
        public async Task<List<DemandeAcces>> GetDemandeAccesListAsync(string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(Constants.BaseApiAddress + "api/DemandeAccesList");

            var demandeAcces = JsonConvert.DeserializeObject<List<DemandeAcces>>(json);

            return demandeAcces;
        }

        /// <summary>
        /// Get Check List By Id 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="accessToken"></param>
        /// <returns>TypeCheckListDTO</returns>
        public async Task<TypeCheckList> GetCheckListByIdAsync(string Id, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = await client.GetStringAsync(
                Constants.BaseApiAddress + "api/GetCheckList/" + Id);

            var typeCheckList = JsonConvert.DeserializeObject<TypeCheckList>(json);

            return typeCheckList;
        }
    }
}
