using ModelClass.area.response;
using REPORT.service.process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace REPORT.controllers
{
    public class PhoneSendedController : ApiController
    {
        private IPhoneSended m_phoneSended;

        public PhoneSendedController(IPhoneSended phoneSended)
        {
            m_phoneSended = phoneSended;
        }
        [Route("api/PhoneSended/getAllPhoneSended")]
        [HttpGet]
        public DataRespond getAllPhoneSended()
        {
            DataRespond data = new DataRespond();
            try {
                data.success = true;
                data.data= m_phoneSended.getAll();
                data.message = "Success";
            }catch(Exception e)
            {
                data.message = e.Message;
                data.error = e;
                data.success = false;
            }
            return data;
        }
    }
}
