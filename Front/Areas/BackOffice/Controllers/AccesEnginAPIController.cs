using Front.Controllers;
using Shared;
using Shared.API.IN;
using Shared.DTO;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using DAL;
using BLL.Biz;
using Shared.Models;

namespace Front.Areas.BackOffice.Controllers
{
    public class AccesEnginAPIController : BaseApiController
    {

        //GET INFO GENERALE BY TYPECHECKLIST
        [HttpPost]
        [Route("AccesEnginapi/GetInfoGrneralesByTypeCheckList")]
        public async Task<HttpResponseMessage> GetInfoGrneralesByTypeCheckList(GetInfoGeneraleByTypeCheckList model)
        {
            var biz = new InfoGeneraleBiz(context, MvcApplication.log);

            var list = biz.GetInfoGeneralesByTypeCheckList(model);

            var result = new RESTServiceResponse<List<InfoGeneraleDTO>>(true, list);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //GET INFO GENERALE BY TYPE ENGIN
        [HttpPost]
        [Route("AccesEnginapi/GetTypeEnginByTypeCheckList")]
        public async Task<HttpResponseMessage> GetTypeEnginByTypeCheckList(GetInfoGeneraleByTypeCheckList model)
        {
            var biz = new InfoGeneraleBiz(context, MvcApplication.log);

            var list = biz.GetTypeEnginByTypeCheckList(model);

            var result = new RESTServiceResponse<List<TypeEnginDTO>>(true, list);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //GET INFO GENERALE BY NATUTRE DE LA MATIRE
        [HttpPost]
        [Route("AccesEnginapi/GetNatureMatiereByTypeCheckList")]
        public async Task<HttpResponseMessage> GetNatureMatiereByTypeCheckList(GetInfoGeneraleByTypeCheckList model)
        {
            var biz = new InfoGeneraleBiz(context, MvcApplication.log);

            var list = biz.GetNatureMatiereByTypeCheckList(model);

            var result = new RESTServiceResponse<List<NatureMatiereDTO>>(true, list);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #region Entités && Sites

        [HttpPost]
        [Route("AccesEnginapi/GetEntityBySite")]
        public async Task<HttpResponseMessage> GetEntityBySite(GetEntityBySiteModel model)
        {
            var biz = new SitesEntitiesBiz(context, MvcApplication.log);

            var list = biz.GetEntityBySite(model);

            var result = new RESTServiceResponse<List<EntityDTO>>(true, list);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion

        #region Users / Roles

        [HttpPost]
        [Route("AccesEnginapi/SaveUserRoles")]
        public async Task<HttpResponseMessage> SaveUserRoles(SaveUserRolesModel model)
        {
            var biz = new ProfileBiz(context, MvcApplication.log);

            var saved = biz.SaveUserRoles(model);

            var result = new RESTServiceResponse<bool>(true, saved);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpPost]
        [Route("AccesEnginapi/GetUserRoles")]
        public async Task<HttpResponseMessage> GetUserRoles(GetUserRolesModel model)
        {
            var list = new List<RoleElement>();

            List<string> roles = new List<string>();
            var biz = new ProfileBiz(context, MvcApplication.log);

            list = biz.GetUserRoles(model);


            var result = new RESTServiceResponse<List<RoleElement>>(true, list);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
        #endregion

        #region KPIS

        //FILL CHART (CONTROLLER && NONCONTROLLER)
        [HttpPost]
        [Route("AccesEnginapi/FillChartPieMyDemande")]
        public async Task<HttpResponseMessage> MesDemande()
        {
            var biz = new KpiBiz(context, MvcApplication.log);

            var Resultat = await biz.MesDemande(CurrentUserId);

            var result = new RESTServiceResponse<KpiModel>(true, Resultat);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //FILL CHART (AUTORISE && NONAUTORISE)
        [HttpPost]
        [Route("AccesEnginapi/FillChartPieMyDemandeAutorise")]
        public async Task<HttpResponseMessage> MesDemandeAutorise()
        {
            var biz = new KpiBiz(context, MvcApplication.log);
            var Resultat = new KpiModel();
            if (IsChefProjet)
                Resultat = await biz.MesDemandeAutorise(CurrentUserId);
            else if (IsControleur)
                Resultat = await biz.DemandeAutoriseByControlleur(CurrentUserId);

            var result = new RESTServiceResponse<KpiModel>(true, Resultat);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }



        #endregion

        #region Reporter
        [HttpPost]
        [Route("AccesEnginapi/reporteraction")]
        public async Task<HttpResponseMessage> ReporterAction(ReporterDemande reporterDemande)
        {

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);


            DemandeAccesBiz biz = new DemandeAccesBiz(context, MvcApplication.log);

            var ressult = await biz.ReporterAction(reporterDemande,CurrentUserId);

            if (ressult)
                return Request.CreateResponse(HttpStatusCode.OK);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);
        }

        #endregion
    }
}
