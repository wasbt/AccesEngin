using BLL.Biz;
using Newtonsoft.Json;
using Shared;
using Shared.Models;
using System;
using System.Collections.Generic;
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
        [HttpGet]
        [Route("api/demandeAccesList")]
        public async Task<HttpResponseMessage> DemandeAccesList(int pageIndex, int pageSize)
        {

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);


            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);

            var result = biz.DemandeAccesList(pageIndex, pageSize);

            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        #endregion


        #region get CheckList For controller

        [HttpGet]
        [Route("api/GetCheckList/{Id}")]
        public async Task<HttpResponseMessage> GetCheckListAsync(int Id)
        {

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);


            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);

            var result = await biz.GetCheckListAsync(Id);

            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);

        }


        #endregion


        #region post CheckList For control

        [HttpPost]
        [Route("api/PostResultatExigences")]
        public async Task<HttpResponseMessage> PostResultatExigencesAsync()
        {
            HttpPostedFileBase postedFile = null;

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);

            var httpRequest = HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var cc = httpRequest.Files[file];
                    var fileName = cc.FileName.Split('\\').LastOrDefault().Split('/').LastOrDefault();
                    postedFile = new HttpPostedFileWrapper(cc);
                }
            }

            var content = httpRequest.Form.GetValues("JsonDetails").FirstOrDefault();

            var resultat = JsonConvert.DeserializeObject<ResultatCheckList>(content);

            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);

            var result = await biz.PostResultatExigencesAsync(resultat, postedFile, ConstsAccesEngin.ContainerName, ConstsAccesEngin.ContainerName);

            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);

        }


        #endregion

        [HttpGet]
        [Route("api/GetResultatExigence/{Id}")]
        public async Task<HttpResponseMessage> GetResultatExigenceByDemandeAccesId(int Id)
        {

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);


            ResultatExigenceBiz biz = new ResultatExigenceBiz(context, WebApiApplication.log);

            var result = await biz.GetResultatExigenceByDemandeAccesId(Id);

            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        [HttpGet]
        [Route("api/DemandeAccesByMatricule/{Matricule}")]
        public async Task<HttpResponseMessage> DemandeAccesByMatricule(string Matricule)
        {

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);


            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);

            var result = await biz.DemandeAccesByMatricule(Matricule);

            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);

        }


        [HttpGet]
        [Route("api/GetDetailsDemandeById/{Id}")]
        public async Task<HttpResponseMessage> GetDetailsDemandeById(int Id)
        {

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);


            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);

            var result = await biz.GetDetailsDemandeByIdAsync(Id);

            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

        [ResponseType(typeof(ValiderDemande))]
        [Route("api/ValiderDemande")]
        public async Task<HttpResponseMessage> ValiderDemandeAsync(ValiderDemande resultat)
        {

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);


            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);

            var result = await biz.ValiderDemande(resultat, CurrentUserId);

            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

    }
}