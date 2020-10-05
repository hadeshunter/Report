using REPORT.reponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using ModelClass.model.sms;
using System.Web;

namespace REPORT.service.sms
{
    public interface ITimer:IReponsitory<Timer>
    {
        Timer getFirstTimer();
    }
}