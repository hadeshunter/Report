using ModelClass.model.proccess;
using ModelClassLibrary.connection;
using REPORT.reponsitory.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REPORT.service.process.impl
{
    public class ProcessImpl : Reponsitory<Process>, IProcessReport
    {
        public ProcessImpl(DataContext context) : base(context)
        {
        }

        public IEnumerable<Process> getAllProcess( int page, int pagesize)
        {
            return m_context.Process.ToList();
        }
    }
}