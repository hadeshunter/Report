using MailChimp.Net.Models;
using ModelClass.area.response;
using ModelClass.auth;
using ModelClass.model.filter;
using ModelClass.model.lang;
using ModelClassLibrary.area.auth.roles;
using ModelClassLibrary.interfaces;
using Permission;
using REPORT.service.user;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
//using System.Web.Http.Cors;


namespace REPORT.controllers
{
   
    public class UserController : ApiController
    {
        private IUser m_user;
        private IUserPer m_userPer;
        private IHashPass m_hashPass;
        public UserController(IUser _user, IHashPass hashPass, IUserPer userPermission)
        {
            m_user = _user;
            m_hashPass = hashPass;
            m_userPer = userPermission;
        }
        [Authorize]
        [Route("api/user/createFirstUser")]
        [HttpPost]
        public DataRespond createFirstUser([FromBody]Users users)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                users.password = m_hashPass.hashPass(users.password);
                m_user.insert(users);
                data.message = "Success";
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
        [Route("api/user/getById")]
        [HttpGet]
        [Authorize]
        public DataRespond getById(int usid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.message = "success";
                data.data = m_user.getById(usid);
                data.success = true;
            }
            catch(Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }
            return data;
        }
        [Route("api/user/getAllUser")]
        [HttpGet]
        [Authorize]
        public DataRespond getAllUser()
        {
            DataRespond data = new DataRespond();
            try
            {
                data.message = "success";
                data.data = m_user.getAll();
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

        [Route("api/user/insert")]
        [HttpPost]
        [Authorize]
        public DataRespond insert([FromBody]Users user)
        {
            DataRespond data = new DataRespond();
            try
            {
                if (!m_user.checkUserExist(user.username))
                {
                    data.success = false;
                    data.message = "User is exist";
                    return data;
                }
                data.success = true;
                data.message = "success";
                user.password = m_hashPass.hashPass(user.password);
                m_user.insert(user);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }

            return data;
        }
        [Route("api/user/update")]
        [HttpPut]
        [Authorize]
        public DataRespond updateUser([FromBody]Users rq)
        {
            DataRespond data = new DataRespond();
            try
            {
                if (!m_user.checkUserExist(rq.username))
                {
                    data.success = false;
                    data.message = "User is exist";
                    return data;
                }
                data.success = true;
                data.message = "success";
                Users users = m_user.getById(rq.usid);

                if (rq.role != users.role)
                {
                    users.role = rq.role;
                }

                if (rq.active != users.active)
                {
                    users.active = rq.active;
                }

                if (rq.password != null && rq.password != "" && rq.password != users.password)
                {
                    users.password = m_hashPass.hashPass(rq.password);
                }
                if (rq.phone != users.phone)
                {
                    users.phone = rq.phone;
                }
                m_user.update(users);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }

            return data;
        }

        [Route("api/user/delete")]
        [HttpDelete]
        [Authorize]
        public DataRespond delete(int usid)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "success";
                m_user.delete(usid);
            }
            catch (Exception e)
            {
                data.success = false;
                data.message = e.Message;
                data.error = e;
            }

            return data;
        }

        [Route("api/user/deleteListUser")]
        [HttpPost]
        [Authorize]
        public DataRespond deleteListUser([FromBody] List<Users> listUser)
        {
            DataRespond data = new DataRespond();
            List<UserPermissions> listUserPermissions = new List<UserPermissions>();
            try
            {
                foreach (var us in listUser)
                {
                    //var usper = m_userPer.getByUsid(us.usid);
                    //m_userPer.removeUserToGroup(usper);
                    m_user.delete(us.usid);
                }
                
                data.success = true;
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

        [Route("api/user/search")]
        [HttpPost]
        [Authorize]
        public DataRespond search([FromBody] DataFilter dataFilter)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "success";
                data.data = m_user.searchUser(dataFilter);
            }
            catch (Exception e)
            {
                data.message = e.Message;
                data.error = e;
                data.success = false;
            }
            return data;
        }


        [Route("api/user/getUserByUserName")]
        [HttpPost]
        public DataRespond getUserByUserName(string username)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.success = true;
                data.message = "success";
                var user = m_user.getUserByUserName(username);
                if (user == null)
                {
                    Users new_user = new Users();
                    new_user.username = username;
                    new_user.password = m_hashPass.hashPass("123456");
                    new_user.role = 2;
                    new_user.active = true;
                    m_user.insert(new_user);
                    data.data = m_user.getUserByUserName(username);
                } else
                {
                    data.data = user;
                }
            }
            catch (Exception e)
            {
                data.message = e.Message;
                data.error = e;
                data.success = false;
            }
            return data;
        }

        [Route("api/user/getUserCNTT")]
        [HttpPost]
        [Authorize]
        public DataRespond GetUserCNTT(RequestUser rq)
        {
            DataRespond data = new DataRespond();
            try
            {
                data.message = "success";
                data.data = m_user.getUserCNTT(rq);
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
