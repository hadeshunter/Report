using System;
using System.Collections.Generic;
using System.Web.Http;
using ModelClass.area.response;
using ModelClassLibrary.area.auth.roles;

namespace Permission.controllers
{
    [Authorize]
    public class GroupPermissionController : ApiController
    {
        private IGroupPermission m_groupPermission;
        public GroupPermissionController(IGroupPermission groupPermission)
        {
            m_groupPermission = groupPermission;
        }

        [Route("api/permission/insertGroupPermission")]
        [HttpPost]
        public DataRespond insert([FromBody]List<GroupPermissions> listgroup)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "success";
                foreach(var i in listgroup)
                {
                    i.createday = DateTime.Now;
                    m_groupPermission.insert(i);
                }
                
            }
            catch(Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [Route("api/permission/removeGroupPermission")]
        [HttpPost]
        public DataRespond remove([FromBody]List<GroupPermissions> listgroup)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "success";
                foreach (var i in listgroup)
                {
                    m_groupPermission.deleteGroupPermission(i);
                }

            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
    }
}