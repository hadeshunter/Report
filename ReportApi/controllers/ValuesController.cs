
using REPORT.service;
using System.Web.Http;
using System.Collections.Generic;
using REPORT.service.auth;
using ModelClass.auth;
using REPORT.schedule.impl;
using REPORT.service.report;
using VNPTPM.Model.Commons;

namespace REPORT.Controllers
{
    public class ValuesController : ApiController
    {
        private IReport m_report;
        private IAuth m_auth;
        public  ValuesController(IReport report, IAuth auth)
        {
            m_report = report;
            m_auth = auth;
        }

        [Route("api/values")]
        [HttpGet]
        public dynamic Get()
        {
            //Users us = new Users();
            //return  m_auth.login(us);
            //QueryTime qr = new QueryTime();
            var SSO = CASP.Authenticate("https://id.vnpt.com.vn/cas/", "http://localhost:8080/api/VNPTLogin/Login");
            return SSO;
        }
       
    }
}
