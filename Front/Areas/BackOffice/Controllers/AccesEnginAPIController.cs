using BLL.Common;
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

namespace Front.Areas.BackOffice.Controllers
{
    public class AccesEnginAPIController : BaseApiController
    {
        //GET INFO GENERALE BY TYPECHECKLIST

        [HttpPost]
        [Route("AccesEnginapi/GetInfoGrneraleByTypeCheckList")]
        public async Task<HttpResponseMessage> GetCompanyRequirements(GetInfoGeneraleByTypeCheckList model)
        {
            var biz = new InfoGeneraleBiz(context, MvcApplication.log);

            var list = biz.GetInfoGeneralesByyTypeCheckList(model);

            var result = new RESTServiceResponse<IList<InfoGeneraleDTO>>(true, list);

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
