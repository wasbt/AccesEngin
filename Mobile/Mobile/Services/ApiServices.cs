using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Mobile.Helpers;
using Mobile.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Plugin.Media.Abstractions;
using Shared.API.OUT;
using Shared.DTO;
using Shared.Models;
using System.Linq;
using Shared.API.IN;

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
        public async Task LoginAsync(string userName, string password)
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

            SetSettings(jwtDynamic);
        }

        private static void SetSettings(JObject jwtDynamic)
        {
            Settings.AccessTokenExpirationDate = jwtDynamic.Value<DateTime>(".expires");
            Settings.AccessToken = jwtDynamic.Value<string>("access_token");
            Settings.UserId = jwtDynamic.Value<string>("UserId");
            Settings.UserRoles = jwtDynamic.Value<string>("UserRoles");
        }

        /// <summary>
        /// Get List Demande
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns>DemandeAcces</returns>
        public async Task<List<DemandeAcces>> GetDemandeAccesListAsync(string accessToken, FilterListDemande filterListDemande)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Bearer", accessToken);

            var json = JsonConvert.SerializeObject(filterListDemande);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var data = await client.PostAsync(Constants.BaseApiAddress + "api/DemandeAccesList", content);


            var demandeAcces = JsonConvert.DeserializeObject<List<DemandeAcces>>(await data.Content.ReadAsStringAsync());

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
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer", accessToken);

                var json = await client.GetStringAsync(
                    Constants.BaseApiAddress + "api/GetCheckList/" + Id);

                var typeCheckList = JsonConvert.DeserializeObject<TypeCheckList>(json);
                return typeCheckList;

            }
            catch (Exception e)
            {

                throw;
            }

        }

        /// <summary>
        /// Get TypeCheck List
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns>TypeCheckListDTO</returns>
        public async Task<List<TypeCheckList>> GetTypeCheckListAsync(string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer", accessToken);

                var json = await client.GetStringAsync(
                    Constants.BaseApiAddress + "api/GetTypeCheckList");

                var typeCheckList = JsonConvert.DeserializeObject<List<TypeCheckList>>(json);
                return typeCheckList;


            }
            catch (Exception e)
            {

                throw;
            }

        }

        /// <summary>
        /// Get Detail Demande By Id 
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="accessToken"></param>
        /// <returns>TypeCheckListDTO</returns>
        public async Task<DemandeDetail> GetDetailsDemandeByIdAsync(long Id, string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer", accessToken);

                var json = await client.GetStringAsync(
                    Constants.BaseApiAddress + "api/GetDetailsDemandeById/" + Id);

                var demandeDetail = JsonConvert.DeserializeObject<DemandeDetail>(json);
                return demandeDetail;


            }
            catch (Exception e)
            {

                throw;
            }

        }

        /// <summary>
        /// Get Demande By Matricule
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="accessToken"></param>
        /// <returns>TypeCheckListDTO</returns>
        public async Task<ObservableCollection<DemandeAcces>> DemandeAccesByMatricule(string Matricule, string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer", accessToken);

                var json = await client.GetStringAsync(
                    Constants.BaseApiAddress + "api/DemandeAccesByMatricule/" + Matricule);

                var demandeAcces = JsonConvert.DeserializeObject<ObservableCollection<DemandeAcces>>(json);
                return demandeAcces;


            }
            catch (Exception e)
            {

                throw;
            }

        }

        public async Task<Model.ResultatExigenceModel> GetResultatExigenceByDemandeAccesId(long DemandeAccesId, string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer", accessToken);

                var json = await client.GetStringAsync(
                    Constants.BaseApiAddress + "api/GetResultatExigence/" + DemandeAccesId);

                var demandeAcces = JsonConvert.DeserializeObject<Model.ResultatExigenceModel>(json);
                return demandeAcces;


            }
            catch (Exception e)
            {

                throw;
            }

        }

        /// <summary>
        /// Post Check List 
        /// </summary>
        /// <param name="resultat"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task PostResultatExigencesAsync(PostResultatExigenceModel resultat, string accessToken)
        {


            var jsonToSend = JsonConvert.SerializeObject(resultat.ResultatCheckList);
            var multipart = new MultipartFormDataContent();
            var body = new StringContent(jsonToSend);
            multipart.Add(body, "JsonDetails");
            if (resultat.StreamFile != null)
            {
                multipart.Add(new StreamContent(resultat.StreamFile), "\"file\"",
                     $"\"{resultat.NameFile}\"");
            }

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = httpClient.PostAsync(Constants.BaseApiAddress + "api/PostResultatExigences", multipart).Result;

        }

        public async Task UploadFileAsync(MultipartFormDataContent contents, string accessToken)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.PostAsync(Constants.BaseApiAddress + "api/Files/Upload", contents);
            var Text = await response.Content.ReadAsStringAsync();

        }

        /// <summary>
        /// Refuser/Accepter Demande 
        /// </summary>
        /// <param name="validerDemande"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task ValiderDemandeAsync(ValiderDemande validerDemande, string accessToken = "")
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var json = JsonConvert.SerializeObject(validerDemande);
            HttpContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(Constants.BaseApiAddress + "api/ValiderDemande", content);
        }

        /// <summary>
        /// get File
        /// </summary>
        /// <param name="validerDemande"></param>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task DownloadAsync(long? id, string accessToken = "")
        {

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetStringAsync(Constants.BaseApiAddress + "api/File/" + id);
        }
    }
}
