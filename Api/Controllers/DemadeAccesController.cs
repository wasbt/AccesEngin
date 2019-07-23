using BLL.Biz;
using Newtonsoft.Json;
using Shared;
using Shared.API.IN;
using Shared.API.OUT;
using Shared.DTO;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace Api.Controllers
{
    public class DemadeAccesController : BaseApiController
    {

        #region get All DemadeAcces
        // GET: DemadeAcces
        [HttpPost, Route("api/demandeAccesList")]
        public async Task<HttpResponseMessage> DemandeAccesList(PostDataModel filterList)
        {
            var filterListDemande = JsonConvert.DeserializeObject<FilterListDemande>(filterList.PostData);
            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);
            var list = biz.DemandeAccesList(filterListDemande);
            var result = new RESTServiceResponse<List<DemandeAccesDto>>(true, string.Empty, list);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        #endregion


        #region get CheckList For controller

        [HttpPost, Route("api/GetCheckList")]
        public async Task<HttpResponseMessage> GetCheckListAsync(PostDataModel model)
        {
            var checkListByIdModel = JsonConvert.DeserializeObject<GetCheckListByIdModel>(model.PostData);
            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);
            var typeCheckList = await biz.GetCheckListAsync(checkListByIdModel.Id);
            var result = new RESTServiceResponse<TypeCheckListDTO>(true, string.Empty, typeCheckList);
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        [HttpPost, Route("api/GetTypeCheckList")]
        public async Task<HttpResponseMessage> GetTypeCheckListAsync()
        {

            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);
            var ListtypeCheckList = await biz.GetTypeCheckListAsync();
            var result = new RESTServiceResponse<List<TypeCheckListDTO>>(true, string.Empty, ListtypeCheckList);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        #endregion


        #region post CheckList For control
        [HttpPost, Route("api/PostResultatExigences")]
        public async Task<HttpResponseMessage> PostResultatExigencesAsync(PostDataModel postResultat)
        {
            var exigenceModel = JsonConvert.DeserializeObject<PostResultatExigenceModel>(postResultat.PostData);
            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);
            var save = await biz.PostResultatExigencesAsync(exigenceModel);
            var result = new RESTServiceResponse<List<TypeCheckListDTO>>(true, string.Empty);
            return Request.CreateResponse(HttpStatusCode.OK, result);


        }
        #endregion

        [HttpPost, Route("api/GetResultatExigence")]
        public async Task<HttpResponseMessage> GetResultatExigenceByDemandeAccesId(PostDataModel model)
        {
            var checkListByIdModel = JsonConvert.DeserializeObject<GetCheckListByIdModel>(model.PostData);
            ResultatExigenceBiz biz = new ResultatExigenceBiz(context, WebApiApplication.log);
            var ControleResultat = await biz.GetResultatExigenceByDemandeAccesId(checkListByIdModel.Id);
            var result = new RESTServiceResponse<ResultatExigenceModel>(true, string.Empty, ControleResultat);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost, Route("api/DemandeAccesByMatricule")]
        public async Task<HttpResponseMessage> DemandeAccesByMatricule(PostDataModel model)
        {
            var checkListByIdModel = JsonConvert.DeserializeObject<string>(model.PostData);
            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);
            var DemandeAcces = await biz.DemandeAccesByMatricule(checkListByIdModel);
            var result = new RESTServiceResponse<List<DemandeAccesDto>>(true, string.Empty, DemandeAcces);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpPost, Route("api/GetDetailsDemandeById")]
        public async Task<HttpResponseMessage> GetDetailsDemandeById(PostDataModel model)
        {
            var checkListByIdModel = JsonConvert.DeserializeObject<GetCheckListByIdModel>(model.PostData);
            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);
            var detailsDemande = await biz.GetDetailsDemandeByIdAsync(checkListByIdModel.Id);
            var result = new RESTServiceResponse<DemandeDetail>(true, string.Empty, detailsDemande);
            return Request.CreateResponse(HttpStatusCode.OK, result);

        }

        [HttpPost, Route("api/ValiderDemande")]
        public async Task<HttpResponseMessage> ValiderDemandeAsync(PostDataModel resultat)
        {
            var validerDemande = JsonConvert.DeserializeObject<ValiderDemande>(resultat.PostData);
            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);
            var Isvalid = await biz.ValiderDemande(validerDemande, CurrentUserId);
            var result = new RESTServiceResponse<DemandeDetail>(true, string.Empty);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet, AllowAnonymous, Route("api/File/{id}")]
        public async Task<HttpResponseMessage> GetProjectImage(long id)
        {
            var file = await context.AppFile.FindAsync(id);
            var data = file?.SystemFileName;

            System.IO.MemoryStream memoryStream = new MemoryStream();
            if (data != null && data.Length > 10)
            {

                //string path = System.Web.Hosting.HostingEnvironment.MapPath(imgData);

                var localPhotoData = File.ReadAllBytes(data);
                memoryStream = new System.IO.MemoryStream(localPhotoData);
            }
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StreamContent(memoryStream)
            };
            var contentType = MimeMapping.GetMimeMapping(file.OriginalFileName);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
            return response;
        }

    }
}