using AutoMapper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using OfficeOpenXml;
using TungShop.Common;
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

        [Route("import")]
        [HttpPost]
        public async Task<HttpResponseMessage> Import()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "Định dạng không được server hỗ trợ");
            }

            var root = HttpContext.Current.Server.MapPath("~/UploadedFiles/Excels");
            if (!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }

            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            

            //Upload files
            int addedCount = 0;
            foreach (MultipartFileData fileData in result.FileData)
            {
                if (string.IsNullOrEmpty(fileData.Headers.ContentDisposition.FileName))
                {
                    return Request.CreateResponse(HttpStatusCode.NotAcceptable, "Yêu cầu không đúng định dạng");
                }
                string fileName = fileData.Headers.ContentDisposition.FileName;
                if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                {
                    fileName = fileName.Trim('"');
                }
                if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                {
                    fileName = Path.GetFileName(fileName);
                }

                var fullPath = Path.Combine(root, fileName);
                File.Copy(fileData.LocalFileName, fullPath, true);

                //insert to DB
                var listApproval = this.ReadApprovalFromExcel(fullPath,0);
                if (listApproval.Count > 0)
                {
                    foreach (var approval in listApproval)
                    {
                        _approvalService.Add(approval);
                        addedCount++;
                    }
                    _approvalService.Save();
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "Đã nhập thành công " + addedCount + " sản phẩm thành công.");
        }

        private List<Approval> ReadApprovalFromExcel(string fullPath, int categoryId)
        {
            using (var package = new ExcelPackage(new FileInfo(fullPath)))
            {
                ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                List<Approval> listApproval = new List<Approval>();
                ApprovalViewModel approvalViewModel;
                Approval approval;
                
                int warranty;

                for (int i = workSheet.Dimension.Start.Row + 1; i <= workSheet.Dimension.End.Row; i++)
                {
                    approvalViewModel = new ApprovalViewModel();
                    approval = new Approval();

                    approvalViewModel.StudentId = workSheet.Cells[i, 1].Value.ToString();
                    approvalViewModel.Name = workSheet.Cells[i, 2].Value.ToString();
                    approvalViewModel.CardNo = workSheet.Cells[i, 3].Value.ToString();
                    approvalViewModel.Address = workSheet.Cells[i, 6].Value.ToString();
                    string date = workSheet.Cells[i, 5].Value.ToString();
                    DateTime date2 =  DateTime.Parse(date);
                    approvalViewModel.BirthDay = date2;
                    approvalViewModel.Status = 1;

                    if (int.TryParse(workSheet.Cells[i, 4].Value.ToString(), out warranty))
                    {
                        approvalViewModel.Sex = warranty;

                    }
                    approval.UpdateApproval(approvalViewModel);
                    listApproval.Add(approval);
                }
                return listApproval;
            }
        }
    }
}