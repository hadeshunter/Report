using ModelClassLibrary.area.auth.roles;
using ModelClassLibrary.connection;
using ModelClassLibrary.reponsitory.imlp;
using System.Data.Entity;
using System.Linq;

namespace Permission.impl
{
    public class GroupPermissionImpl : PermissionReponsitory<GroupPermissions>, IGroupPermission
    {
        public GroupPermissionImpl(PermissionContext context) : base(context)
        {
        }

        public void deleteGroupPermission(GroupPermissions gr)
        {
            var grpr = m_context.GroupPermissions.FirstOrDefault(m => m.groupid == gr.groupid && m.perid == gr.perid);
            m_context.Entry(grpr).State = EntityState.Deleted;
            save();
        }
    }
}
