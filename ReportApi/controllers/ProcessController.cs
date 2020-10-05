using ModelClass.area.response;
using ModelClass.model.proccess;
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
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class ProcessController : ApiController
    {
        private IProcessReport m_processReport;

        public ProcessController(IProcessReport processReport)
        {
            m_processReport = processReport;
        }
        [Route("api/Process/getAllProcess")]
        [HttpPost]
         public DataRespond getAllProcess([FromBody] PageProcess pageProcess)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                // data.data = m_processReport.getAllProcess(pageProcess.page, pageProcess.pagesize);
                data.data = m_processReport.getAll();
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
