using BLL.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace WebApi.Controllers
{
    [Authorize]
    public class DemadeAccesController : BaseApiController
    {
        // GET: DemadeAcces
        [HttpGet]
        [Route("api/demandeAccesList")]
        public async Task<HttpResponseMessage> DemandeAccesList()
        {

            if (!ModelState.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest);


            DemandeAccesBiz biz = new DemandeAccesBiz(context, WebApiApplication.log);

            var result = biz.DemandeAccesList();

            if (result != null)
                return Request.CreateResponse(HttpStatusCode.OK, result);
            else
                return Request.CreateResponse(HttpStatusCode.BadRequest);

        }

    }
}