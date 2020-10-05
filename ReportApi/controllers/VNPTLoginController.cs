using ModelClass.area.response;
using ModelClassLibrary.connection;
using ModelClassLibrary.interfaces;
using REPORT.service.auth;
using REPORT.service.user;
using System;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using VNPTPM.Model.Commons;

namespace VNPTPM.Web.Api.Ad
{
    //[RoutePrefix("api/VNPTLogin")]
    public class VNPTLoginController : ApiController
    {
        private IUser m_user;
        //private IHashPass m_hashPass;
        //private IAuth m_auth;
        public VNPTLoginController(IUser _user)
        {
            m_user = _user;
        }
        [AllowAnonymous]
        [Route("api/VNPTLogin/Login")]
        [HttpGet]
        public void Login()
        {
            string trans_google = "https://translate.google.com";
            string cashostURL = "https://id.vnpt.com.vn/cas/";
            string redirectURL = "http://localhost:8080/api/VNPTLogin/Login";
            string redirectURLSuccess = "http://localhost:8080/api/auth/login";
            DataRespond data = new DataRespond();
            try
            {
                //HttpContext.Current.Server.ClearError();
                //HttpContext.Current.Response.Headers.Clear();
                //HttpContext.Current.Response.Buffer = true;
                string username = CASP.Authenticate(cashostURL, redirectURL);
                
                if (!HttpContext.Current.Response.IsRequestBeingRedirected)
                {
                    HttpContext.Current.Response.Redirect(trans_google, true);
                }

                //if (username != null)
                //{
                //    //    // kiểm tra username đã được khai báo trong CSDL hay chưa?
                //    var user = m_user.getUserByUserName(username);
                //    //    //if(user is null)
                //    //    //{
                //    //    //    //nếu tài khoản email chưa có trong hệ thống thì hiển thị thông báo không có quyền truy cập
                //    //    //    //HttpContext.Current.Response.Redirect(redirectURLPermision, true);

                //    //    //} else
                //    //    //{
                //    //    //    // nếu tài khoản đã được khai báo rồi thì tạo mới 1 mật khẩu động
                //    //    //    var password = Guid.NewGuid().ToString();
                //    //    //    // ma hóa mật khẩu, luu xuống CSDL
                //    //    //    user.password = m_hashPass.hashPass(password);
                //    //    //    // lưu mật khẩu xuống CSDL. sau đó dùng tài khoản và mật khẩu mới tạo gọi api để lấy token
                //    //    //    m_user.update(user);
                //    //    //    //call function login of system
                //    //    //    m_auth.login(user);
                //    //    //    //redirect to main page
                //    HttpContext.Current.Response.RedirectPermanent(redirectURLSuccess, true);
                //    //    //}
                //}
                //else
                //{
                //    HttpContext.Current.Response.Redirect(redirectURL, true);
                //}
            }
            catch (Exception e)
            {
                HttpContext.Current.Response.Redirect(redirectURL, true);
                Console.WriteLine("go to this");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("api/VNPTLogin/Logout")]
        public void Logout()
        {
            string cashostURLLogout = "https://id.vnpt.com.vn/cas/logout?service=";
            string redirectURL = "http://localhost:8080"; // set url khi đăng xuất thành công sẽ chuyển đến url này.

            HttpContext.Current.Response.Redirect(cashostURLLogout + redirectURL, true);
        }
        
    }
}
