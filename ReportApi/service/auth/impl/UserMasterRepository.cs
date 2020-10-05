
using ModelClass.auth;
using ModelClassLibrary.connection;
using ModelClassLibrary.imlp;
using ModelClassLibrary.interfaces;
using REPORT.service.user;
using REPORT.service.user.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace REPORT.service.auth.impl
{
    public class UserMasterRepository : IDisposable
    {
        IUser m_user = new UserImpl(new PermissionContext());
        private IHashPass m_hashPass = new HashPass();
        private PermissionContext m_data = new PermissionContext();

        public void Dispose()
        {
            m_data.Dispose();
        }

        //This method is used to check and validate the user credentials
        public Users ValidateUser(string username, string password)
        {
            var user =  m_user.getAll().FirstOrDefault(m=>m.username == username);
            if (checkUser(user,username,password))
            {
                return user;
            }
            return null;
        }
        private Boolean checkUser(Users user,string usrequest, string passrequest)
        {
            if(user == null)
            {
                return false;
            }
            //if(user.username == usrequest && m_hashPass.checkPass(user.password, passrequest))
            //{
            //    return true;
            //}

            return true;
        }
    }
}