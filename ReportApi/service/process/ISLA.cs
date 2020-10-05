using ModelClass.model.proccess;
using REPORT.reponsitory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace REPORT.service.process
{
    public interface ISLA:IReponsitory<SLAData>
    {
        dynamic getAllSLAFromOracle();
        dynamic updateSla(SLAData data);
        dynamic createSLA(SLAData data);
        dynamic execureQuery(string query);
        dynamic GetByIdSla(int id);
        dynamic deleteSla(int id);


    }
}