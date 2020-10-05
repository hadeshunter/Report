using ModelClassLibrary.area.auth.roles;
using REPORT.reponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permission
{
    public interface IPermission: IReponsitory<Permissions>
    {
        dynamic getCategoryByUser(int usid,string lang);
        dynamic getPerMissionByGroupId(int groupid);
        dynamic getPerMissionNotInGroup(int groupid);
        dynamic getAllLanguage();
    }
}
