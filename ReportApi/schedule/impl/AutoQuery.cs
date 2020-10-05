
using ModelClass.model.sms;
using ModelClassLibrary.connection;
using Quartz;
using REPORT.service.process;
using REPORT.service.process.impl;
using REPORT.service.sms;
using REPORT.service.sms.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REPORT.schedule.impl
{
    public class AutoQuery : IJob
    {
        public void Execute(IJobExecutionContext context)
        {

            IProcessReport m_process = new ProcessImpl(new DataContext());


            ModelClass.WS_800126.AuthHeader auth = new ModelClass.WS_800126.AuthHeader();
            auth.Username = "hampn";
            auth.Password = "vnpt@20190723";
            ModelClass.WS_800126.Service1 smsend = new ModelClass.WS_800126.Service1();

            smsend.AuthHeaderValue = auth;
            List<object> listobj = new List<object>();
            string note = "Vui lòng kiểm tra hóa đơn số 01102 đã được gửi bởi TTKD";
            foreach (var i in getPhone())
            {
                string result = smsend.sendsms(i.phone, note);
                listobj.Add(new {
                    phone= i.phone,
                    result
                });
            }
        }
        public List<Sms> getPhone()
        {
            ISms m_sms = new SmsImpl(new DataContext());
            return m_sms.getAll().ToList();
        }
        
        public Boolean checkSendSms(int prid)
        {
            IProcessReport m_process = new ProcessImpl(new DataContext());

            var process = m_process.getById(prid);
            return false;
        }
    }
}