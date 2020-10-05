using ModelClass.model.proccess;
using REPORT.reponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REPORT.service.process
{
    public interface IProcessReport:IReponsitory<Process>
    {
        IEnumerable<Process> getAllProcess( int page, int pagesize);
    }
}