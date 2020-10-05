using ModelClass.area.response;
using ModelClass.model.report.request;
using REPORT.service;
using REPORT.service.report;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace REPORT.controllers
{
    [Authorize]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ReportController : ApiController
    {
        // private IConfiguration m_configuration;
        private IReport m_report;
        public ReportController(IReport report)
        {
            m_report = report;
        }
        public dynamic Get(int test)
        {
            return m_report.execureStoreProcedure(test);
        }

        [Route("api/report/getReportDasboard")]
        [HttpPost]
        public DataRespond GetReportDasboard(ReportRequest rq)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_report.getReportDasboard(rq);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [Route("api/report/getReportTongYeuCau")]
        [HttpPost]
        public DataRespond GetReportTongYeuCau(ReportRequest rq)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_report.getReportTongYeuCau(rq);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }


        [Route("api/report/insertSLADetail1st")]
        [HttpPost]
        public DataRespond InsertSLADetail1st(ReportRequest rq)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_report.insertSLADetail1st(rq);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [Route("api/report/insertSLADetail2nd")]
        [HttpPost]
        public DataRespond InsertSLADetail2nd(ReportRequest rq)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_report.insertSLADetail2nd(rq);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [Route("api/report/insertSLADetail3rd")]
        [HttpPost]
        public DataRespond InsertSLADetail3rd(ReportRequest rq)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_report.insertSLADetail3rd(rq);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [Route("api/report/getListReportSLA")]
        [HttpPost]
        public DataRespond GetListReportSLA(ReportRequest rq)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_report.getListReportSLA(rq);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [Route("api/report/filterByGD_DG")]
        [HttpPost]
        public DataRespond FilterByGD_DG(ReportRequest rq)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_report.filterByGD_DG(rq);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [Route("api/report/getThongTinYeuCau/{v_ID_YEU_CAU:int}")]
        [HttpGet]
        public DataRespond GetThongTinYeuCau(int v_ID_YEU_CAU)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_report.getThongTinYeuCau(v_ID_YEU_CAU);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [Route("api/report/getFilterByTKH")]
        [HttpPost]
        public DataRespond GetFilterByTKH(ReportRequest rq)
        {
            DataRespond data = new DataRespond();
            ReportRequest input1 = new ReportRequest();
            input1.v_ID_NHOM_GIAI_DOAN = rq.v_ID_NHOM_GIAI_DOAN;
            input1.v_TEN_KHACH_HANG = rq.v_TEN_KHACH_HANG;
            input1.v_ID_GIAI_DOAN = 6;

            ReportRequest input2 = new ReportRequest();
            input2.v_ID_NHOM_GIAI_DOAN = rq.v_ID_NHOM_GIAI_DOAN;
            input2.v_TEN_KHACH_HANG = rq.v_TEN_KHACH_HANG;
            input2.v_ID_GIAI_DOAN = 7;

            ReportRequest input3 = new ReportRequest();
            input3.v_ID_NHOM_GIAI_DOAN = rq.v_ID_NHOM_GIAI_DOAN;
            input3.v_TEN_KHACH_HANG = rq.v_TEN_KHACH_HANG;
            input3.v_ID_GIAI_DOAN = 8;


            try
            {
                data.success = true;
                m_report.insertFilterByTKH1(input1);
                m_report.insertFilterByTKH2(input2);
                m_report.insertFilterByTKH3(input3);
                data.data = m_report.getFilterByTKH(rq);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [Route("api/report/getFilterByID")]
        [HttpPost]
        public DataRespond GetFilterByID(ReportRequest rq)
        {
            DataRespond data = new DataRespond();
            ReportRequest input1 = new ReportRequest();
            input1.v_ID_NHOM_GIAI_DOAN = rq.v_ID_NHOM_GIAI_DOAN;
            input1.v_ID_YEU_CAU = rq.v_ID_YEU_CAU;
            input1.v_ID_GIAI_DOAN = 6;

            ReportRequest input2 = new ReportRequest();
            input2.v_ID_NHOM_GIAI_DOAN = rq.v_ID_NHOM_GIAI_DOAN;
            input2.v_ID_YEU_CAU = rq.v_ID_YEU_CAU;
            input2.v_ID_GIAI_DOAN = 7;

            ReportRequest input3 = new ReportRequest();
            input3.v_ID_NHOM_GIAI_DOAN = rq.v_ID_NHOM_GIAI_DOAN;
            input3.v_ID_YEU_CAU = rq.v_ID_YEU_CAU;
            input3.v_ID_GIAI_DOAN = 8;


            try
            {
                data.success = true;
                m_report.insertFilterByID1(input1);
                m_report.insertFilterByID2(input2);
                m_report.insertFilterByID3(input3);
                data.data = m_report.getFilterByID(rq);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [Route("api/report/getWarningList")]
        [HttpPost]
        public DataRespond GetWarningList([FromBody] ReportRequest rq)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_report.getWarningList(rq);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
    }
}