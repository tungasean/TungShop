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
    [RoutePrefix("api/electricityWater")]
    [Authorize]
    public class ElectricityWaterController : ApiControllerBase
    {
        #region Initialize
        private IElectricityWaterService _ElectricityWaterService;

        public ElectricityWaterController(IErrorService errorService, IElectricityWaterService ElectricityWaterService)
            : base(errorService)
        {
            this._ElectricityWaterService = ElectricityWaterService;
        }

        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _ElectricityWaterService.GetAll();

                var responseData = Mapper.Map<IEnumerable<ElectricityWater>, IEnumerable<ElectricityWaterViewModel>>(model);

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
                var model = _ElectricityWaterService.GetById(id);

                var responseData = Mapper.Map<ElectricityWater, ElectricityWaterViewModel>(model);

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
                var model = _ElectricityWaterService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.RoomID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<ElectricityWater>, IEnumerable<ElectricityWaterViewModel>>(query);

                var paginationSet = new PaginationSet<ElectricityWaterViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, ElectricityWaterViewModel ElectricityWaterVm)
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
                    var newElectricityWater = new ElectricityWater();
                    newElectricityWater.UpdateElectricityWater(ElectricityWaterVm);
                    _ElectricityWaterService.Add(newElectricityWater);
                    _ElectricityWaterService.Save();

                    var responseData = Mapper.Map<ElectricityWater, ElectricityWaterViewModel>(newElectricityWater);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ElectricityWaterViewModel ElectricityWaterVm)
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
                    var dbElectricityWater = _ElectricityWaterService.GetById(ElectricityWaterVm.RoomID);

                    dbElectricityWater.UpdateElectricityWater(ElectricityWaterVm);

                    _ElectricityWaterService.Update(dbElectricityWater);
                    _ElectricityWaterService.Save();

                    var responseData = Mapper.Map<ElectricityWater, ElectricityWaterViewModel>(dbElectricityWater);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}