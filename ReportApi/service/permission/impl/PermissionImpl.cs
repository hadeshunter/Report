using ModelClassLibrary.area.auth.roles;
using ModelClassLibrary.connection;
using ModelClassLibrary.reponsitory.imlp;
using System;
using System.Linq;

namespace Permission.impl
{
    public class PermissionImpl : PermissionReponsitory<Permissions>, IPermission
    {
        public PermissionImpl(PermissionContext context) : base(context)
        {

        }
        public dynamic getAllLanguage()
        {
            var listlang = (from tran in m_context.PermissionTranslations
                           join lang in m_context.Languages
                           on tran.languages equals lang.languages
                           select lang).GroupBy(
                                m=>m.langname,
                                m=>m.languages,
                                (key, g) => new
                                {
                                    key,
                                    lan = g.FirstOrDefault()
                                }
                           );
            return listlang;
        }

        public dynamic getCategoryByUser(int usid, string lang)
        {
            if(lang== null || lang == "")
            {
                lang = "vi";
            }
            var menu =
                from userper in m_context.UserPermissions.Where(m => m.usid == usid)
                join groups in m_context.Groups
                on userper.groupid equals groups.groupid
                join groupper in m_context.GroupPermissions
                on groups.groupid equals groupper.groupid
                join perm in m_context.Permissions
                on groupper.perid equals perm.perid
                join trans in m_context.PermissionTranslations.Where(m => m.languages == lang)
                on perm.perid equals trans.perid
                select new
                {
                    perm.link,
                    trans.pername,
                    perm.policy,
                    perm.action,
                    trans.languages,
                    perm.perid
                };
            return menu;
        }

        public dynamic getPerMissionByGroupId(int groupid)
        {
            var permission = from per in m_context.Permissions.Where(m => m.active == true)
                             join grper in m_context.GroupPermissions
                             on per.perid equals grper.perid
                             join gr in m_context.Groups.Where(m => m.groupid == groupid)
                             on grper.groupid equals gr.groupid
                             select per;
            return permission;
        }
        public dynamic getPerMissionNotInGroup(int groupid)
        {
            var permission = from per in m_context.Permissions.Where(m => m.active == true)
                             where !(from gr in m_context.Groups.Where(m=>m.groupid== groupid)
                                     join grper in m_context.GroupPermissions
                                     on gr.groupid equals grper.groupid
                                     select grper.perid).Contains(per.perid)
                             select per;
            return permission;
        }
    }
}
