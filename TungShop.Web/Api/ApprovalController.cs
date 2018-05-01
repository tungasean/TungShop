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
    [RoutePrefix("api/approval")]
    [Authorize]
    public class ApprovalController : ApiControllerBase
    {
        #region Initialize
        private IApprovalService _approvalService;

        public ApprovalController(IErrorService errorService, IApprovalService approvalService)
            : base(errorService)
        {
            this._approvalService = approvalService;
        }

        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _approvalService.GetAll();

                var responseData = Mapper.Map<IEnumerable<Approval>, IEnumerable<ApprovalViewModel>>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
        [Route("getbyid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _approvalService.GetById(id);

                var responseData = Mapper.Map<Approval, ApprovalViewModel>(model);

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
                var model = _approvalService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.ApprovalId).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Approval>, IEnumerable<ApprovalViewModel>>(query);

                var paginationSet = new PaginationSet<ApprovalViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, ApprovalViewModel approvalVm)
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

                    var newApproval = new Approval();
                    newApproval.UpdateApproval(approvalVm);
                    _approvalService.Add(newApproval);
                    _approvalService.Save();

                    var responseData = Mapper.Map<Approval, ApprovalViewModel>(newApproval);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ApprovalViewModel approvalVm)
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
                    var dbApproval = _approvalService.GetById(approvalVm.ApprovalId);

                    dbApproval.UpdateApproval(approvalVm);

                    _approvalService.Update(dbApproval);
                    _approvalService.Save();

                    var responseData = Mapper.Map<Approval, ApprovalViewModel>(dbApproval);
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
                    var dbApproval = _approvalService.GetById(id);
                    var oldApproval = _approvalService.Delete(dbApproval);
                    _approvalService.Save();

                    var responseData = Mapper.Map<Approval, ApprovalViewModel>(oldApproval);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}