using ModelClass.area.response;
using ModelClass.model.holiday;
using REPORT.service.holiday;
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
    public class HolidayController : ApiController
    {
        // private IConfiguration m_configuration;
        private IHoliday m_holiday;
        public HolidayController(IHoliday holiday)
        {
            m_holiday = holiday;
        }

        [Route("api/holiday/getAllHoliday")]
        [HttpGet]
        public DataRespond GetAllHoliday(int year)
        {
            DataRespond data = new DataRespond();
            try 
            {
                data.data = m_holiday.getListHoliday(year);
                data.success = true;
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

        [Route("api/holiday/insertHoliday")]
        [HttpPost]
        public DataRespond InsertHoliday(int year)  // Insert all saturday, sunday of year and holidays such as 01/01, 30/04, 01/05, 02/09 by year
        {
            DataRespond data = new DataRespond();
            try
            {
                data.data = m_holiday.insertHoliday(year);
                data.success = true;
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

        [Route("api/holiday/insertByDate")]
        [HttpPost]
        public DataRespond InsertByDate(int date)  // Insert YYYYMMDD
        {
            DataRespond data = new DataRespond();
            try
            {
                data.data = m_holiday.insertByDate(date);
                data.success = true;
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

        [Route("api/holiday/deleteDate")]
        [HttpDelete]
        public DataRespond DeleteDate(int date)  // Insert YYYYMMDD
        {
            DataRespond data = new DataRespond();
            try
            {
                data.data = m_holiday.deleteDate(date);
                data.success = true;
                data.message = "deleted";
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
