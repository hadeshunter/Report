using ModelClass.auth;
using ModelClass.model.filter;
using REPORT.reponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REPORT.service.user
{
    public interface IUser:IReponsitory<Users>
    {
        Boolean checkUserExist(string username);
        dynamic searchUser(DataFilter dataFilter);
        Users getUserByUserName(string username);
        dynamic getUserCNTT(RequestUser rq);
    }
}