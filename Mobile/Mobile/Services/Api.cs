using Mobile.Helpers;
using Mobile.HttpREST;
using Mobile.Model;
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
        public static async Task<LoginResultModel> LoginAction(LoginModel loginModel)
        {
            return await RESTHelper.GetLoginResultModel(loginModel, Constants.BaseApiAddress + "Token");
        }
        #endregion

        #region Get List Demande
        public static async Task<RESTServiceResponse<List<DemandeAcces>>> GetDemandeAccesListAsync(FilterListDemande filterListDemande)
        {
            return await RESTHelper.GetRequest<List<DemandeAcces>>(Settings.AccessToken, Constants.BaseApiAddress + "api/DemandeAccesList", HttpVerbs.POST, postObject: filterListDemande);
        }
        #endregion

        #region Get TypeCheckList
        public static async Task<RESTServiceResponse<TypeCheckList>> GetCheckListByIdAsync(long Id)
        {
            var model = new GetCheckListByIdModel();
            model.Id = Id;
            return await RESTHelper.GetRequest<TypeCheckList>(Settings.AccessToken, Constants.BaseApiAddress + "api/GetCheckList", HttpVerbs.POST, postObject: model);
        }
        #endregion 
        
        #region Get List TypeCheckList
        public static async Task<RESTServiceResponse<List<TypeCheckList>>> GetTypeCheckListAsync()
        {
            return await RESTHelper.GetRequest<List<TypeCheckList>>(Settings.AccessToken, Constants.BaseApiAddress + "api/GetTypeCheckList", HttpVerbs.POST);
        }
        #endregion
        #region Get  Details Demande By Id
        public static async Task<RESTServiceResponse<DemandeDetail>> GetDetailsDemandeByIdAsync(long Id)
        {
            var model = new GetCheckListByIdModel();
            model.Id = Id;
            return await RESTHelper.GetRequest<DemandeDetail>(Settings.AccessToken, Constants.BaseApiAddress + "api/GetDetailsDemandeById", HttpVerbs.POST, postObject: model);
        }
        #endregion

        #region Get  Details Demande By Matricule
        public static async Task<RESTServiceResponse<ObservableCollection<DemandeAcces>>> DemandeAccesByMatricule(string Matricule)
        {
            var model = new GetCheckListByIdModel();
            model.Matricule = Matricule;
            return await RESTHelper.GetRequest<ObservableCollection<DemandeAcces>>(Settings.AccessToken, Constants.BaseApiAddress + "api/DemandeAccesByMatricule", HttpVerbs.POST, postObject: model);
        }
        #endregion

        #region Get  Resultat Exigence
        public static async Task<RESTServiceResponse<Model.ResultatExigenceModel>> GetResultatExigenceByDemandeAccesId(long Id)
        {
            var model = new GetCheckListByIdModel();
            model.Id = Id;
            return await RESTHelper.GetRequest<Model.ResultatExigenceModel>(Settings.AccessToken, Constants.BaseApiAddress + "api/GetResultatExigence", HttpVerbs.POST, postObject: model);
        }
        #endregion

        #region Get  Resultat Exigence
        public static async Task<RESTServiceResponse<Model.ResultatExigenceModel>> PostResultatExigencesAsync(PostResultatExigenceModel resultat)
        {
            return await RESTHelper.GetRequest<Model.ResultatExigenceModel>(Settings.AccessToken, Constants.BaseApiAddress + "api/PostResultatExigences", HttpVerbs.POST, postObject: resultat);
        }
        #endregion



        //#region Toggle commission availability
        //public static async Task<RESTServiceResponse<bool>> ToggleCommissionAvailability(ToggleCommissionModel model)
        //{
        //    return await RESTHelper.GetRequest<bool>(Settings.AccessToken, AppUrls.ToggleCommissionAvailability, HttpVerbs.POST, postObject: model);
        //}
        //#endregion

        //#region Save project results
        //public static async Task<RESTServiceResponse<bool>> SaveProjectResults(ProjectResultValueModel model)
        //{
        //    return await RESTHelper.GetRequest<bool>(Settings.AccessToken, AppUrls.SaveProjectResultValues, HttpVerbs.POST, postObject: model);
        //}
        //#endregion

        //#region Get project details
        //public static async Task<RESTServiceResponse<ProjectDetailsElement>> GetProjectDetails(ProjectDetailsFilterModel model)
        //{
        //    return await RESTHelper.GetRequest<ProjectDetailsElement>(Settings.AccessToken, AppUrls.GetProjectDetails, HttpVerbs.POST, postObject: model);
        //}
        //#endregion

        //#region Get projects list
        //public static async Task<RESTServiceResponse<List<ProjectElement>>> GetProjectsList(ProjectFilterModel model)
        //{
        //    var response = await RESTHelper.GetRequest<List<ProjectElement>>(Settings.AccessToken, AppUrls.GetProjectsList, HttpVerbs.POST, postObject: model);

        //    return response;
        //}
        //#endregion

        //#region Get commissions list
        //public static async Task<RESTServiceResponse<List<CommissionElement>>> GetCommissionsList(CommissionFilterModel model)
        //{
        //    var response = await RESTHelper.GetRequest<List<CommissionElement>>(Settings.AccessToken, AppUrls.GetCommissionsList, HttpVerbs.POST, postObject: model);


        //    return response;
        //}
        //#endregion

    }
}
