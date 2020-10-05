using System;
using System.Web.Http;
using ModelClass.area.response;
using Permission;

namespace TestService.controllers
{
   // [Authorize]
    public class PermissionController : ApiController
    {
        private IPermission m_permistion;
        private IGroup m_group;
        string language = "vi";
        public PermissionController(IPermission permistion, IGroup group)
        {
            m_permistion = permistion;
            m_group = group;
        }
        [Route("api/permission/getPermistionById")]
        [HttpGet]
        public DataRespond getPermistionById(int usid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                // string lang = HttpContext.Request.Headers["lang"].ToString();
                //data.data = m_permistion.getCategoryByUser(usid, lang);
                data.data = m_permistion.getCategoryByUser(usid, language);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }

            return data;
        }


        [Route("api/permission/getPerMissionByGroupId")]
        [HttpGet]
        public DataRespond getPerMissionByGroupId(int groupid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                var permission = m_permistion.getPerMissionByGroupId(groupid);
                data.data = permission;
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

        [Route("api/permission/getPerMissionNotInGroup")]
        [HttpGet]
        public DataRespond getPerMissionNotInGroup(int groupid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                var permission = m_permistion.getPerMissionNotInGroup(groupid);
                data.data = permission;
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

        [Route("api/permission/getAllPermissionByUser")]
        [HttpGet]
        public DataRespond getPermissionByUser(int usid, string lang)
        {
            DataRespond data = new DataRespond();
            if (lang != null && lang.Length > 0)
            {
                language = lang;
            }
            try
            {
                data.success = true;
               // string lang = HttpContext.Request.Headers["lang"].ToString();
                data.data = m_permistion.getCategoryByUser(usid, language);
            }
            catch(Exception e)
            {
                data.message = e.Message;
                data.success = false;
                data.error = e;
            }
            return data;
        }

        [Route("api/permission/getAllLanguage")]
        [HttpGet]
        public DataRespond getAllLanguage()
        {
            DataRespond data = new DataRespond();
            try
            {
                
                data.success = true;
                //string lang = HttpContext.Request.Headers["lang"].ToString();
                data.message = "success";
                data.data = m_permistion.getAllLanguage();
            }
            catch (Exception e)
            {
                data.message = e.Message;
                data.success = false;
                data.error = e;
            }
            return data;
        }
    }
}