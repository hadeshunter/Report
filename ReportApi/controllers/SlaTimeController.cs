using ModelClass.area.response;
using REPORT.service.process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ModelClass.model.proccess;
using System.IdentityModel.Tokens.Jwt;
using System.Globalization;

namespace REPORT.controllers
{
    [Authorize]
    public class SlaTimeController : ApiController
    {
        private ISLA m_sla;
        public SlaTimeController(ISLA sLA)
        {
            m_sla = sLA;
        }


        [Route("api/sla/getAllSla")]
        [HttpGet]
        public DataRespond getAll()
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "success";
                data.data = m_sla.getAllSLAFromOracle();
            }
            catch (Exception e)
            {
                data.message = e.Message;
                data.error = e;
                data.success = false;
            }

            return data;
        }
        [Route("api/sla/createSLA")]
        [HttpPost]
        public DataRespond createSLA(SLAData sLAData)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                var query = "insert into HDDT_BK.cau_hinh_sla_ttcntt(" +
                  "ID_NHOM_GIAI_DOAN,ID_GIAI_DOAN,ID_DO_UU_TIEN,THOI_GIAN ,NGAY_TAO,NGUOI_TAO,NGAY_CAP_NHAT" +
                  ",NGUOI_CAP_NHAT,TTHD,NGAY_KT,THEO_VB) values (" +
                  sLAData.ID_NHOM_GIAI_DOAN + "," +
                  sLAData.ID_GIAI_DOAN + "," +
                  sLAData.ID_DO_UU_TIEN + "," +
                  sLAData.THOI_GIAN + "," +
                  (sLAData.NGAY_TAO != null ? "" + "to_date('" + sLAData.NGAY_TAO + "','dd-MM-yyyy')" : "null") + "," +
                  sLAData.NGUOI_TAO + "," +
                  (sLAData.NGAY_CAP_NHAT != null ? "" + "to_date('" + sLAData.NGAY_CAP_NHAT + "','dd-MM-yyyy ')" : "null") + "," +
                  sLAData.NGUOI_CAP_NHAT + "," +
                  sLAData.TTHD + "," +
                  (sLAData.NGAY_KT != null ? "" + "to_date('" + sLAData.NGAY_KT + "','dd-MM-yyyy ')" : "null") + "," +
                  "'" + (sLAData.THEO_VB != null ? "" + sLAData.THEO_VB : "null") + "'"
                 + ")";
                data.data = m_sla.execureQuery(query);
                data.message = "Success";
            }
            catch (Exception e)
            {
                data.message = e.Message;
                data.error = e;
                data.success = false;
            }

            return data;
        }

        [Route("api/sla/GetById")]
        [HttpGet]
        public DataRespond GetById(int id)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_sla.GetByIdSla(id);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.message = e.Message;
                data.error = e;
                data.success = false;
            }

            return data;
        }
        [Route("api/sla/updateSla")]
        [HttpPost]
        public DataRespond updateSla([FromBody]SLAData sLAData)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_sla.updateSla(sLAData);
                data.message = "Cập nhật dữ liệu thành công";
            }
            catch (Exception e)
            {
                data.message = e.Message;
                data.error = e;
                data.success = false;
            }

            return data;
        }
        [Route("api/sla/deleteSlatime")]
        [HttpDelete]
        public DataRespond deleteSla(int id)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_sla.deleteSla(id);
                data.message = "xóa dữ liệu thành công";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }


        /*
            parse jwt to object data
         */
        public dynamic parseJWTT(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
            return tokenS.Claims.First(claim => claim.Type == "sub").Value;
        }
    }
}
