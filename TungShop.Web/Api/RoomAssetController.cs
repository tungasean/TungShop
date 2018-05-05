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
    [RoutePrefix("api/roomAsset")]
    [Authorize]
    public class RoomAssetController : ApiControllerBase
    {
        #region Initialize
        private IRoomAssetService _roomAssetService;

        public RoomAssetController(IErrorService errorService, IRoomAssetService roomAssetService)
            : base(errorService)
        {
            this._roomAssetService = roomAssetService;
        }

        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _roomAssetService.GetAll();

                var responseData = Mapper.Map<IEnumerable<RoomAsset>, IEnumerable<RoomAssetViewModel>>(model);

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
                var model = _roomAssetService.GetById(id);

                var responseData = Mapper.Map<IEnumerable<RoomAsset>, IEnumerable<RoomAssetViewModel>>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

//        [Route("getall")]
//        [HttpGet]
//        public HttpResponseMessage GetAll(HttpRequestMessage request, string keyword, int page, int pageSize = 20)
//        {
//            return CreateHttpResponse(request, () =>
//            {
//                int totalRow = 0;
//                var model = _roomAssetService.GetAll(keyword);
//
//                totalRow = model.Count();
//                var query = model.OrderByDescending(x => x.RoomAssetID).Skip(page * pageSize).Take(pageSize);
//
//                var responseData = Mapper.Map<IEnumerable<RoomAsset>, IEnumerable<RoomAssetViewModel>>(query);
//
//                var paginationSet = new PaginationSet<RoomAssetViewModel>()
//                {
//                    Items = responseData,
//                    Page = page,
//                    TotalCount = totalRow,
//                    TotalPages = (int)Math.Ceiling((decimal)totalRow / pageSize)
//                };
//                var response = request.CreateResponse(HttpStatusCode.OK, paginationSet);
//                return response;
//            });
//        }


        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage request, RoomAssetViewModel roomAssetVm)
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
                    var newRoomAsset = new RoomAsset();
                    newRoomAsset.UpdateRoomAsset(roomAssetVm);
                    _roomAssetService.Add(newRoomAsset);
                    _roomAssetService.Save();

                    var responseData = Mapper.Map<RoomAsset, RoomAssetViewModel>(newRoomAsset);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, RoomAssetViewModel roomAssetVm)
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
                    var dbRoomAsset = _roomAssetService.GetById(roomAssetVm.RoomID);
                    foreach (var value in dbRoomAsset)
                    {
                        value.UpdateRoomAsset(roomAssetVm);

                        _roomAssetService.Update(value);
                        _roomAssetService.Save();
                    }
                    

                    var responseData = Mapper.Map<IEnumerable<RoomAsset>, IEnumerable<RoomAssetViewModel>>(dbRoomAsset);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, string id)
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
                    var dbRoomAsset = _roomAssetService.GetById(id);
                    foreach (var value in dbRoomAsset)
                    {
                        var oldRoomAsset = _roomAssetService.Delete(value);
                    }
                    _roomAssetService.Save();

                    var responseData = Mapper.Map<IEnumerable<RoomAsset>, IEnumerable<RoomAssetViewModel>>(dbRoomAsset);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}