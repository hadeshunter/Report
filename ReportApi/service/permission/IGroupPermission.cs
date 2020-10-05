using ModelClassLibrary.area.auth.roles;
using REPORT.reponsitory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Permission
{
    public interface IGroupPermission:IReponsitory<GroupPermissions>
    {
        void deleteGroupPermission(GroupPermissions gr);
    }
}
