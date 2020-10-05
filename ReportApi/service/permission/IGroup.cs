
using ModelClassLibrary.area.auth.roles;
using REPORT.reponsitory;
using System;

namespace Permission
{
    public interface IGroup: IReponsitory<Groups>
    {
        Boolean checkGroup(int groupid);
    }
}
