using ModelClassLibrary.connection;
using REPORT.reponsitory.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using ModelClass.model.sms;
using System.Web;

namespace REPORT.service.sms.impl
{
    public class TimerImpl : Reponsitory<Timer>, ITimer
    {
        public TimerImpl(DataContext context) : base(context)
        {
        }
        public Timer getFirstTimer()
        {
            return getAll().FirstOrDefault();
        }

    }
}