using ModelClassLibrary.area.auth.roles;
using System.Linq;
using System.Threading.Tasks;
using ModelClassLibrary.reponsitory.imlp;
using ModelClassLibrary.connection;
namespace Permission.impl
{
    public class GroupImpl : PermissionReponsitory<Groups>, IGroup
    {
        public GroupImpl(PermissionContext context) : base(context)
        {
        }

        public bool checkGroup(int groupid)
        {
            var group = m_context.UserPermissions.FirstOrDefault(m => m.groupid == groupid);
            var usergr = m_context.GroupPermissions.FirstOrDefault(m => m.groupid == groupid);
            if(group == null && usergr == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
