using System;
using System.Collections.Generic;
using System.Web.Http;
using ModelClass.area.response;
using ModelClassLibrary.area.auth.roles;

namespace Permission.controllers
{
    [Authorize]
    public class UserPermissionController : ApiController
    {
        private IUserPer m_userPer;
        public UserPermissionController(IUserPer userPermission)
        {
            m_userPer = userPermission;
        }

        [Route("api/permission/getUserInGroup")]
        [HttpGet]
        public DataRespond getUserInGroup(int groupid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "success";
                data.data = m_userPer.getUserInGroup(groupid);
            }
            catch(Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
        [Route("api/permission/getUserNotInGroup")]
        [HttpGet]
        public DataRespond getUserNotInGroup(int groupid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "success";
                data.data = m_userPer.getUserNotInGroup(groupid);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [Route("api/permission/addUserToGroup")]
        [HttpPost]
        public DataRespond addUserToGroup([FromBody] List<UserPermissions> listUserPermissions)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "success";
                foreach (var i in listUserPermissions)
                {
                    i.createday = DateTime.Now;
                    m_userPer.insert(i);
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

        [Route("api/permission/removeUserToGroup")]
        [HttpPost]
        public DataRespond removeUserToGroup([FromBody] List<UserPermissions>listUserPermissions)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "success";
                m_userPer.removeUserToGroup(listUserPermissions);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }

            return data;
        }

        [Route("api/permission/getByUsid")]
        [HttpGet]
        public DataRespond getById(int usid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.message = "success";
                data.data = m_userPer.getByUsid(usid);
                data.success = true;
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