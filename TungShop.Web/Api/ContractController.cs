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
    [RoutePrefix("api/contract")]
    [Authorize]
    public class ContractController : ApiControllerBase
    {
        #region Initialize
        private IContractService _ContractService;

        public ContractController(IErrorService errorService, IContractService ContractService)
            : base(errorService)
        {
            this._ContractService = ContractService;
        }

        #endregion

        [Route("getallcontracts")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _ContractService.GetAll();

                var responseData = Mapper.Map<IEnumerable<Contract>, IEnumerable<ContractViewModel>>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
        [Route("getbyid/{studentId}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, string studentId)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _ContractService.GetSingleByCondition(studentId);

                var responseData = Mapper.Map<Contract, ContractViewModel>(model);

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
                var model = _ContractService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.RoomID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Contract>, IEnumerable<ContractViewModel>>(query);

                var paginationSet = new PaginationSet<ContractViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, ContractViewModel ContractVm)
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
                    var newContract = new Contract();
                    newContract.UpdateContract(ContractVm);
                    _ContractService.Add(newContract);
                    _ContractService.Save();

                    var responseData = Mapper.Map<Contract, ContractViewModel>(newContract);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ContractViewModel ContractVm)
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
                    var dbContract = _ContractService.GetSingleByCondition(ContractVm.StudentID);

                    dbContract.UpdateContract(ContractVm);

                    _ContractService.Update(dbContract);
                    _ContractService.Save();

                    var responseData = Mapper.Map<Contract, ContractViewModel>(dbContract);
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
                    var dbApproval = _ContractService.GetSingleByCondition(id);
                    var oldApproval = _ContractService.Delete(dbApproval);
                    _ContractService.Save();

                    var responseData = Mapper.Map<Contract, ContractViewModel>(oldApproval);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}