using AutoMapper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using TungShop.Common;
using TungShop.Common;
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
                var model = _ElectricityWaterHistoryService.GetAll(id);

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
        [HttpGet]
        [Route("ExportPdf")]
        public async Task<HttpResponseMessage> ExportPdf(HttpRequestMessage request, int id)
        {
            string fileName = string.Concat("Invoice_" + DateTime.Now.ToString("yyyyMMddhhmmssfff") + ".pdf");
            var folderReport = ConfigHelper.GetByKey("ReportFolder");
            string filePath = HttpContext.Current.Server.MapPath(folderReport);
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }
            string fullPath = Path.Combine(filePath, fileName);
            try
            {
                var template = File.ReadAllText(HttpContext.Current.Server.MapPath("/Assets/admin/templates/product-detail.html"));
                var replaces = new Dictionary<string, string>();
                var electricityWater = _ElectricityWaterHistoryService.GetById(id);
                //Cap nhat lai lich su la da in hoa don
                if (electricityWater != null)
                {
                    electricityWater.IsPrint = 1;
                    _ElectricityWaterHistoryService.Update(electricityWater);
                    _ElectricityWaterHistoryService.Save();
                }
                
                //tao invoice
                if (electricityWater != null)
                {
                    replaces.Add("{{Month}}", electricityWater.Month);
                    replaces.Add("{{Room}}", electricityWater.RoomID);
                    replaces.Add("{{SoDienCu}}", electricityWater.EletricityOld.ToString("N0"));
                    replaces.Add("{{SoDienMoi}}", electricityWater.EletricityNew.ToString("N0"));
                    replaces.Add("{{GiaDien}}", electricityWater.PriceElectricity.ToString() );
                    replaces.Add("{{SoNuocCu}}", electricityWater.WaterOld.ToString("N0"));
                    replaces.Add("{{SoNuocMoi}}", electricityWater.WaterNew.ToString("N0"));
                    replaces.Add("{{GiaNuoc}}", electricityWater.PriceWater.ToString() );
                    replaces.Add("{{ThanhTien}}", electricityWater.Money.ToString() );
                }
                
                template = template.Parse(replaces);

                await ReportHelper.GeneratePdf(template, fullPath);
                return request.CreateErrorResponse(HttpStatusCode.OK, Path.Combine(folderReport, fileName));
            }
            catch (Exception ex)
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage request, ElectricityWaterHistoryViewModel ElectricityWaterVm)
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
                    var dbElectricityWaterHistory = _ElectricityWaterHistoryService.GetById(ElectricityWaterVm.ID);

                    dbElectricityWaterHistory.UpdateElectricityWaterHistory(ElectricityWaterVm);

                    _ElectricityWaterHistoryService.Update(dbElectricityWaterHistory);
                    _ElectricityWaterHistoryService.Save();

                    var responseData = Mapper.Map<ElectricityWaterHistory, ElectricityWaterHistoryViewModel>(dbElectricityWaterHistory);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }

                return response;
            });
        }
    }
}