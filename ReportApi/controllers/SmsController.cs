using ModelClass.area.response;
using ModelClass.model.sms;
using REPORT.service.sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace REPORT.controllers
{

    [Authorize]
    public class SmsController : ApiController
    {
        private ISms m_sms;
        public SmsController(ISms sms)
        {
            m_sms = sms;
        }
        [Route("api/sms/getListSms")]
        [HttpGet]
        public DataRespond getListSms()
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_sms.getAll();
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

        [Route("api/sms/insertSms")]
        [HttpPost]
        public DataRespond insert([FromBody]Sms sms)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "insert success";
                m_sms.insert(sms);
            }
            catch(Exception e)
            {
                data.message = e.Message;
                data.error = e;
                data.success = false;
            }

            return data;
        }
        [Route("api/sms/updateSms")]
        [HttpPost]
        public DataRespond updateSms([FromBody]Sms sms)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                Sms smsold = m_sms.getById(sms.smsid);
                smsold.name = sms.name;
                smsold.phone = sms.phone;
                smsold.active = sms.active;
                data.message = "update success";
                m_sms.update(smsold);
            }
            catch (Exception e)
            {
                data.message = e.Message;
                data.error = e;
                data.success = false;
            }

            return data;
        }
        [Route("api/sms/getById")]
        [HttpGet]
        public DataRespond getById(int smsid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.message = "success";
                data.data = m_sms.getById(smsid);
                data.success = true;
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
        [Route("api/sms/deleteItemSms")]
        [HttpDelete]

        public DataRespond deleteItemSms(int smsid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                m_sms.delete(smsid);
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
