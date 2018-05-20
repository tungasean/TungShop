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
    [RoutePrefix("api/studentDiscipline")]
    [Authorize]
    public class StudentDisciplineController : ApiControllerBase
    {
        #region Initialize
        private IStudentDisciplineService _studentDisciplineService;

        public StudentDisciplineController(IErrorService errorService, IStudentDisciplineService studentDisciplineService)
            : base(errorService)
        {
            this._studentDisciplineService = studentDisciplineService;
        }

        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _studentDisciplineService.GetAll();

                var responseData = Mapper.Map<IEnumerable<StudentDiscipline>, IEnumerable<StudentDisciplineViewModel>>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
        [Route("getbystudentid/{id}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, string id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _studentDisciplineService.GetById(id);

                var responseData = Mapper.Map<IEnumerable<StudentDiscipline>, IEnumerable<StudentDisciplineViewModel>>(model);

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
                var model = _studentDisciplineService.GetById(id);

                var responseData = Mapper.Map<StudentDiscipline, StudentDisciplineViewModel>(model);

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
                var model = _studentDisciplineService.GetAll();

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.StudentID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<StudentDiscipline>, IEnumerable<StudentDisciplineViewModel>>(query);

                var paginationSet = new PaginationSet<StudentDisciplineViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, StudentDisciplineViewModel studentDisciplineVm)
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
                    var newStudentDiscipline = new StudentDiscipline();
                    newStudentDiscipline.UpdateStudentDiscipline(studentDisciplineVm);
                    _studentDisciplineService.Add(newStudentDiscipline);
                    _studentDisciplineService.Save();

                    var responseData = Mapper.Map<StudentDiscipline, StudentDisciplineViewModel>(newStudentDiscipline);
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
                    var dbStudentDiscipline = _studentDisciplineService.GetById(id);
                    var oldStudentDiscipline = _studentDisciplineService.Delete(dbStudentDiscipline);
                    _studentDisciplineService.Save();

                    var responseData = Mapper.Map<StudentDiscipline, StudentDisciplineViewModel>(oldStudentDiscipline);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}