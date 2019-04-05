using BLL.Biz;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Results;

namespace WebApi.Controllers
{
    [Authorize]
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

        [HttpGet]
        [Route("api/DemandeAccesById/{Id}")]
        public async Task<HttpResponseMessage> DemandeAccesById(int Id)
        {

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);


            ResultatExigenceBiz biz = new ResultatExigenceBiz(context, WebApiApplication.log);

            var result = await biz.DemandeAccesById(Id);

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

        #region get CheckList For controller

        [ResponseType(typeof(ResultatCheckList))]
        [Route("api/PostResultatExigences")]
        public async Task<HttpResponseMessage> PostResultatExigencesAsync(ResultatCheckList resultat)
        {

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);


            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);

            var result = await biz.PostResultatExigencesAsync(resultat);

            if (result)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);

        }


        #endregion
    }
}