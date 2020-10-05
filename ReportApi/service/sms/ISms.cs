using ModelClass.model.sms;
using REPORT.reponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REPORT.service.sms
{
    public interface ISms:IReponsitory<Sms>
    {
         IEnumerable<Sms> getAllSms(int smsid, int pagesize, int page);
    }
}