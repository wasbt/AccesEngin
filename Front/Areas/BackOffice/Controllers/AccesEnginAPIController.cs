using BLL.Common.Biz;
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

namespace Front.Areas.BackOffice.Controllers
{
    public class AccesEnginAPIController : ApiController
    {
        public EnginDbContext context = new EnginDbContext();
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
    }
}
