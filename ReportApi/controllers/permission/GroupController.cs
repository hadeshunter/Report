using System;
using System.Linq;
using System.Web.Http;
using ModelClass.area.response;
using ModelClassLibrary.area.auth.roles;

namespace Permission.controllers
{
    [Authorize]
    public class GroupController : ApiController
    {
        private IGroup m_group;
        public GroupController(IGroup group)
        {
            m_group = group;
        }

        [Route("api/permission/getAllGroup")]
        [HttpGet]
        public DataRespond getAllGroup()
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.data = m_group.getAll().Where(m=>m.active== true);
                data.message = "success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [Route("api/permission/insertGroup")]
        [HttpPost]
        public DataRespond insert([FromBody]Groups group)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                group.active = true;
                data.message = "insert success";
                m_group.insert(group);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }

            return data;
        }


        [Route("api/permission/updateGroup")]
        [HttpPost]
        public DataRespond update([FromBody]Groups group)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                Groups gr = m_group.getById(group.groupid);
                gr.groupname = group.groupname;
                gr.note = group.note;
                gr.active = group.active;
                m_group.update(gr);
                data.message = "update success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }


        [Route("api/permission/getGroupById")]
        [HttpGet]
        public DataRespond getGroupById(int grid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "update success";
                data.data = m_group.getById(grid);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }

        [Route("api/permission/deleteGroup")]
        [HttpDelete]
        public DataRespond deleteGroup(int grid)
        {
            DataRespond data = new DataRespond();
            try
            {
                if (m_group.checkGroup(grid))
                {
                    data.success = true;
                    data.message = "update success";
                    m_group.delete(grid);
                }
                else
                {
                    data.success = false;
                    data.message = "The group also contains permissions or accounts, please empty";
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