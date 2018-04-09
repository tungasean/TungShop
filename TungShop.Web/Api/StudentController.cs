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
    [RoutePrefix("api/student")]
    [Authorize]
    public class StudentController : ApiControllerBase
    {
        #region Initialize
        private IStudentService _studentService;

        public StudentController(IErrorService errorService, IStudentService studentService)
            : base(errorService)
        {
            this._studentService = studentService;
        }

        #endregion

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _studentService.GetAll();

                var responseData = Mapper.Map<IEnumerable<Student>, IEnumerable<StudentViewModel>>(model);

                var response = request.CreateResponse(HttpStatusCode.OK, responseData);
                return response;
            });
        }
        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var model = _studentService.GetById(id);

                var responseData = Mapper.Map<Student, StudentViewModel>(model);

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
                var model = _studentService.GetAll(keyword);

                totalRow = model.Count();
                var query = model.OrderByDescending(x => x.StudentID).Skip(page * pageSize).Take(pageSize);

                var responseData = Mapper.Map<IEnumerable<Student>, IEnumerable<StudentViewModel>>(query);

                var paginationSet = new PaginationSet<StudentViewModel>()
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
        public HttpResponseMessage Create(HttpRequestMessage request, StudentViewModel studentVm)
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
                    var newStudent = new Student();
                    newStudent.UpdateStudent(studentVm);
                    _studentService.Add(newStudent);
                    _studentService.Save();

                    var responseData = Mapper.Map<Student, StudentViewModel>(newStudent);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, StudentViewModel studentVm)
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
                    var dbStudent = _studentService.GetById(studentVm.StudentID);

                    dbStudent.UpdateStudent(studentVm);

                    _studentService.Update(dbStudent);
                    _studentService.Save();

                    var responseData = Mapper.Map<Student, StudentViewModel>(dbStudent);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}