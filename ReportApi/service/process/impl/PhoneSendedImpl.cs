using ModelClass.model.proccess;
using ModelClassLibrary.connection;
using REPORT.reponsitory;
using REPORT.reponsitory.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REPORT.service.process.impl
{
    public class PhoneSendedImpl : Reponsitory<PhoneSended>, IPhoneSended
    {
        public PhoneSendedImpl(DataContext context) : base(context)
        {

        }

        //public dynamic getUserSendedSms()
        //{
        //    var processlist = from process in getAll()
        //                   join PhoneSended in m_context.PhoneSended
        //                   on process.phoneid equals PhoneSended.phoneid
        //                   select new
        //                   {
        //                       process,
        //                       PhoneSended
        //                   };
        //    return processlist;
        //}
    }
}