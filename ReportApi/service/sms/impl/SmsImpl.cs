using ModelClass.model.sms;
using ModelClassLibrary.connection;
using REPORT.reponsitory.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REPORT.service.sms.impl
{
    public class SmsImpl : Reponsitory<Sms>, ISms
    {
        public SmsImpl(DataContext context) : base(context)
        {
        }
        public IEnumerable<Sms> getAllSms(int smsid, int pagesize, int page)
        {
            return m_context.Sms.Where(x => x.smsid == smsid).ToList().Skip(page * pagesize).Take(pagesize);
        }
    }
}