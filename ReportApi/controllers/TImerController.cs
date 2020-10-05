using ModelClass.area.response;
using REPORT.service.sms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ModelClass.model.sms;

namespace REPORT.controllers
{
    [Authorize]
    public class TimerController : ApiController
    {
        private ITimer m_timer;
        public TimerController(ITimer timer)
        {
            m_timer = timer;
        }

        [Route("api/Timer/getAllTimer")]
        [HttpGet]
        public DataRespond getAllTimer()
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_timer.getAll();
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

        [Route("api/Timer/insertTimer")]
        [HttpPost]
        public DataRespond insert([FromBody]Timer timer)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "insert success";
                m_timer.insert(timer);
            }
            catch (Exception e)
            {
                data.message = e.Message;
                data.error = e;
                data.success = false;
            }

            return data;
        }
        [Route("api/Timer/getById")]
        [HttpGet]
        public DataRespond getById(int timerid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.message = "success";
                data.data = m_timer.getById(timerid);
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
        [Route("api/Timer/updateTimer")]
        [HttpPost]
        public DataRespond update([FromBody]Timer timer)
        {
            DataRespond data = new DataRespond();
            try
            {
                Timer timerold = m_timer.getById(timer.timeid);
                timerold.timeid = timer.timeid;
                timerold.id_giai_doan = timer.id_giai_doan;
                timerold.totaltime = timer.totaltime;
                data.success = true;
                data.message = "Update success";
                m_timer.update(timerold);
            }
            catch (Exception e)
            {
                data.message = e.Message;
                data.error = e;
                data.success = false;
            }

            return data;
        }
    }
}
