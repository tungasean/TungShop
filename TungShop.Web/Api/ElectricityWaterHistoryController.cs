using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TungShop.Model.Models;
using TungShop.Service;
using TungShop.Web.Infrastructure.Core;
using TungShop.Web.Models;
using TungShop.Web.Infrastructure.Extensions;

namespace TungShop.Web.Api
{
    [RoutePrefix("api/electricityWaterHistory")]
    [Authorize]
    public class ElectricityWaterHistoryController : ApiControllerBase
    {
        #region Initialize
        private IElectricityWaterHistoryService _ElectricityWaterHistoryService;

        public ElectricityWaterHistoryController(IErrorService errorService, IElectricityWaterHistoryService ElectricityWaterHistoryService)
            : base(errorService)
        {
            this._ElectricityWaterHistoryService = ElectricityWaterHistoryService;
        }

        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _ElectricityWaterHistoryService.GetAll();

                var responseData = Mapper.Map<IEnumerable<ElectricityWaterHistory>, IEnumerable<ElectricityWaterHistoryViewModel>>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
        [Route("getbyid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _ElectricityWaterHistoryService.GetById(id);

                var responseData = Mapper.Map<IEnumerable<ElectricityWaterHistory>, IEnumerable<ElectricityWaterHistoryViewModel>>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRow = 0;
                var model = _ElectricityWaterHistoryService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.RoomID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<ElectricityWaterHistory>, IEnumerable<ElectricityWaterHistoryViewModel>>(query);

                var paginationSet = new PaginationSet<ElectricityWaterHistoryViewModel>()
                {
                    Items = responseData,
                    Page = page,
                    TotalCount = totalRow,
                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
                return response;
            });
        }


        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, ElectricityWaterHistoryViewModel ElectricityWaterHistoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (!ModelState.IsValid)
                {
                    response = request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var newElectricityWaterHistory = new ElectricityWaterHistory();
                    newElectricityWaterHistory.UpdateElectricityWaterHistory(ElectricityWaterHistoryVm);
                    _ElectricityWaterHistoryService.Add(newElectricityWaterHistory);
                    _ElectricityWaterHistoryService.Save();

                    var responseData = Mapper.Map<ElectricityWaterHistory, ElectricityWaterHistoryViewModel>(newElectricityWaterHistory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}