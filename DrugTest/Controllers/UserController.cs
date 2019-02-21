using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Dap;

namespace DrugTest.Controllers
{
    public class UserController : ApiController
    {
        /// <summary>
        /// 检查登录是否成功
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public RESULT CheckLogin()
        {
                
            RESULT result = new RESULT();
            try
            {
                string phoneNumber = HttpContext.Current.Request.Form["phoneNumber"];
                string key = HttpContext.Current.Request.Form["key"];

                result.result = Dap.user.checkLogin(phoneNumber, key);
               
            }
            catch (Exception e)
            {
                result.state = false;
                result.msg = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public RESULT Register()
        {

            RESULT result = new RESULT();
            try
            {
                string phoneNumber = HttpContext.Current.Request.Form["phoneNumber"];
                string key = HttpContext.Current.Request.Form["key"];

                result.result = Dap.user.registerUser(phoneNumber, key);

            }
            catch (Exception e)
            {
                result.state = false;
                result.msg = e.Message;
            }
            return result;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public RESULT ChangePsd()
        {

            RESULT result = new RESULT();
            try
            {
                string id = HttpContext.Current.Request.Form["id"];
                string key = HttpContext.Current.Request.Form["key"];
                string newKey = HttpContext.Current.Request.Form["newKey"];

                result.result = Dap.user.changePsd(id, key,newKey);

            }
            catch (Exception e)
            {
                result.state = false;
                result.msg = e.Message;
            }
            return result;
        }
    }
}
