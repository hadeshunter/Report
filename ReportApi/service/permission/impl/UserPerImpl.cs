using ModelClassLibrary.area.auth.roles;
using ModelClassLibrary.connection;
using ModelClassLibrary.reponsitory.imlp;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Permission.impl
{

    public class UserPerImpl : PermissionReponsitory<UserPermissions>, IUserPer
    {
        public UserPerImpl(PermissionContext context) : base(context)
        {
        }

        public dynamic getByUsid(int usid)
        {
            var userper = from user in m_context.Users.Where(m => m.usid == usid)
                          join userpermission in m_context.UserPermissions
                          on user.usid equals userpermission.usid
                          join groups in m_context.Groups
                          on userpermission.groupid equals groups.groupid
                          select userpermission;
            return userper;
        }

        public dynamic getUserInGroup(int groupid)
        {
            var userper = from user in m_context.Users
                          join userpermission in m_context.UserPermissions
                          on user.usid equals userpermission.usid
                          join groups in m_context.Groups.Where(m => m.groupid == groupid)
                          on userpermission.groupid equals groups.groupid
                          select new
                          {
                              user,
                              groups,
                              userpermission
                          };
            return userper;
        }

        public dynamic getUserNotInGroup(int groupid)
        {
            var userper = from user in m_context.Users
                          where !(from gr in m_context.Groups.Where(m=>m.groupid == groupid)
                                  join userpermission in m_context.UserPermissions
                                  on gr.groupid equals userpermission.groupid
                                  select userpermission.usid).Contains(user.usid)
                          select new
                          {
                              user
                          };
            return userper;
        }

        public void removeUserToGroup(List<UserPermissions> list)
        {
            foreach (var i in list)
            {
                var userper = m_context.UserPermissions.Where(m => m.usid == i.usid && m.groupid == i.groupid).FirstOrDefault();
                m_context.Entry(userper).State = EntityState.Deleted;
                save();
            }
        }
    }
}
