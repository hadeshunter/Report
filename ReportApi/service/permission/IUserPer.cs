using ModelClassLibrary.area.auth.roles;
using REPORT.reponsitory;
using System;
using System.Collections.Generic;

namespace Permission
{
    public interface IUserPer :IReponsitory<UserPermissions>
    {
        dynamic getByUsid(int usid);
        dynamic getUserInGroup(int groupid);
        dynamic getUserNotInGroup(int groupid);
        void removeUserToGroup(List<UserPermissions> list);
    }
}
