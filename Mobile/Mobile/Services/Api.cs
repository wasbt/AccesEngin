﻿using Mobile.Helpers;
using Mobile.HttpREST;
using Mobile.Model;
using Newtonsoft.Json;
using Shared.API.IN;
using Shared.API.OUT;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mobile.Services
{
    public static class Api
    {
        #region Login
        public static async Task<LoginResultModel> LoginAction(string username, string password)
        {
            return await RESTHelper.GetLoginResultModel(username, password);
        }
        #endregion

        #region Get List Demande
        public static async Task<RESTServiceResponse<List<ControleModel>>> GetDemandeAccesListAsync(FilterListDemande filterListDemande)
        {
            var postData = new PostDataModel
            {
                PostData = JsonConvert.SerializeObject(filterListDemande),
            };

            return await RESTHelper.GetRequest<List<ControleModel>>(Settings.AccessToken, AppUrls.BaseUrl + "api/DemandeAccesList", HttpVerbs.POST, postObject: postData);
        }
        #endregion

        #region Get TypeCheckList
        public static async Task<RESTServiceResponse<TypeCheckList>> GetCheckListByIdAsync(long Id)
        {
            var model = new GetCheckListByIdModel();
            model.Id = Id;
            var postData = new PostDataModel
            {
                PostData = JsonConvert.SerializeObject(model),
            };
            return await RESTHelper.GetRequest<TypeCheckList>(Settings.AccessToken, AppUrls.BaseUrl + "api/GetCheckList", HttpVerbs.POST, postObject: postData);
        }
        #endregion 
        
        #region Get List TypeCheckList
        public static async Task<RESTServiceResponse<List<TypeCheckList>>> GetTypeCheckListAsync()
        {
            return await RESTHelper.GetRequest<List<TypeCheckList>>(Settings.AccessToken, AppUrls.BaseUrl + "api/GetTypeCheckList", HttpVerbs.POST);
        }
        #endregion

        #region Get  Details Demande By Id
        public static async Task<RESTServiceResponse<ControleModel>> GetDetailsDemandeByIdAsync(long Id)
        {
            var model = new GetCheckListByIdModel();
            model.Id = Id;

            var postData = new PostDataModel
            {
                PostData = JsonConvert.SerializeObject(model),
            };
            return await RESTHelper.GetRequest<ControleModel>(Settings.AccessToken, AppUrls.BaseUrl + "api/GetDetailsDemandeById", HttpVerbs.POST, postObject: postData);
        }
        #endregion

        #region Get  Details Demande By Matricule
        public static async Task<RESTServiceResponse<ObservableCollection<DemandeAcces>>> DemandeAccesByMatricule(string Matricule)
        {
            var postData = new PostDataModel
            {
                PostData = JsonConvert.SerializeObject(Matricule),
            };
            return await RESTHelper.GetRequest<ObservableCollection<DemandeAcces>>(Settings.AccessToken, AppUrls.BaseUrl + "api/DemandeAccesByMatricule", HttpVerbs.POST, postObject: postData);
        }
        #endregion

        #region Get  Resultat Exigence
        public static async Task<RESTServiceResponse<Model.ResultatExigenceModel>> GetResultatExigenceByDemandeAccesId(long Id)
        {
            var model = new GetCheckListByIdModel();
            model.Id = Id;
            var postData = new PostDataModel
            {
                PostData = JsonConvert.SerializeObject(model),
            };
            return await RESTHelper.GetRequest<Model.ResultatExigenceModel>(Settings.AccessToken, AppUrls.BaseUrl + "api/GetResultatExigence", HttpVerbs.POST, postObject: postData);
        }
        #endregion

        #region Get  Resultat Exigence
        public static async Task<RESTServiceResponse<Model.ResultatExigenceModel>> PostResultatExigencesAsync(PostResultatExigenceModel resultat)
        {
            var postData = new PostDataModel
            {
                PostData = JsonConvert.SerializeObject(resultat),
            };
            return await RESTHelper.GetRequest<Model.ResultatExigenceModel>(Settings.AccessToken, AppUrls.BaseUrl + "api/PostResultatExigences", HttpVerbs.POST, postObject: postData);
        }
        #endregion 

        #region Valider Demande
        public static async Task<RESTServiceResponse<bool>> ValiderDemandeAsync(ValiderDemande validerDemande)
        {
            var postData = new PostDataModel
            {
                PostData = JsonConvert.SerializeObject(validerDemande),
            };
            return await RESTHelper.GetRequest<bool>(Settings.AccessToken, AppUrls.BaseUrl + "api/ValiderDemande", HttpVerbs.POST, postObject: postData);
        }
        #endregion
        #region Download File
        public static async Task<RESTServiceResponse<bool>> DownloadAsync(long Id)
        {
            var model = new GetCheckListByIdModel();
            model.Id = Id;
            return await RESTHelper.GetRequest<bool>(Settings.AccessToken, AppUrls.BaseUrl + "api/File", HttpVerbs.POST, postObject: model);
        }
        #endregion



        
    }
}
