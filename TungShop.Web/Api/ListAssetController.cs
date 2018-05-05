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
    [RoutePrefix("api/listAsset")]
    [Authorize]
    public class ListAssetController : ApiControllerBase
    {
        #region Initialize
        private IListAssetService _listAssetService;
        private IRoomAssetService _roomAssetService;

        public ListAssetController(IErrorService errorService, IListAssetService listAssetService, IRoomAssetService roomAssetService)
            : base(errorService)
        {
            this._listAssetService = listAssetService;
            this._roomAssetService = roomAssetService;
        }

        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _listAssetService.GetAll();

                var responseData = Mapper.Map<IEnumerable<ListAsset>, IEnumerable<ListAssetViewModel>>(model);

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
                List<ListAsset> listResult = new List<ListAsset>();
                var listAssign = _roomAssetService.GetById(id);
                foreach (var assign in listAssign)
                {
                    var model = _listAssetService.GetById(assign.AssetsID);
                    if (model != null)
                    {
                        listResult.Add(model);
                    }
                }

                var responseData = Mapper.Map<IEnumerable<ListAsset>, IEnumerable<ListAssetViewModel>>(listResult);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);

                return response;
            });
        }

        [Route("getAssetbyid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetAssetbyid(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                
                    var model = _listAssetService.GetById(id);
                var responseData = Mapper.Map<ListAsset, ListAssetViewModel>(model);

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
                var model = _listAssetService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.AssetsID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<ListAsset>, IEnumerable<ListAssetViewModel>>(query);

                var paginationSet = new PaginationSet<ListAssetViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, ListAssetViewModel listAssetVm)
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
                    var newListAsset = new ListAsset();
                    newListAsset.UpdateListAsset(listAssetVm);
                    _listAssetService.Add(newListAsset);
                    _listAssetService.Save();

                    var responseData = Mapper.Map<ListAsset, ListAssetViewModel>(newListAsset);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ListAssetViewModel listAssetVm)
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
                    var dbListAsset = _listAssetService.GetById(listAssetVm.AssetsID);

                    dbListAsset.UpdateListAsset(listAssetVm);

                    _listAssetService.Update(dbListAsset);
                    _listAssetService.Save();

                    var responseData = Mapper.Map<ListAsset, ListAssetViewModel>(dbListAsset);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
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
                    var dbListAsset = _listAssetService.GetById(id);
                    var oldListAsset = _listAssetService.Delete(dbListAsset);
                    _listAssetService.Save();

                    var responseData = Mapper.Map<ListAsset, ListAssetViewModel>(oldListAsset);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}